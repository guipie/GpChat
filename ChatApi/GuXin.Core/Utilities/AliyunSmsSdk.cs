using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace GuXin.Core.Utilities
{
    public class AliyunSmsSdk
    {
        private readonly string Version = "2017-05-25";
        private readonly string Action = "SendSms";
        private readonly string Format = "JSON";

        private readonly int MaxRetryNumber = 3;
        private readonly bool AutoRetry = true;
        private const string SEPARATOR = "&";
        private readonly int TimeoutInMilliSeconds = 100000;



        private readonly string Endpoint = "dysmsapi.aliyuncs.com";
        private readonly string RegionId = "cn-hangzhou";
        private readonly string AccessKeyId;
        private readonly string AccessKeySecret;

        public AliyunSmsSdk(string accessKeyId, string accessKeySecret)
        {
            AccessKeyId = accessKeyId;
            AccessKeySecret = accessKeySecret;
        }
        public AliyunSmsSdk(string accessKeyId, string accessKeySecret, string regionId, string endpoint)
        {
            AccessKeyId = accessKeyId;
            AccessKeySecret = accessKeySecret;
            RegionId = regionId;
            Endpoint = endpoint;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        public async Task<(bool success, string response)> Send(SmsObject sms)
        {
            Dictionary<string, string> paramers = new()
            {
                { "PhoneNumbers", sms.Mobile },
                { "SignName", sms.Signature },
                { "TemplateCode", sms.TempletKey },
                { "TemplateParam", JsonConvert.SerializeObject(sms.Data) },
                { "OutId", sms.OutId },
                { "AccessKeyId", AccessKeyId }
            };

            try
            {
                string url = GetSignUrl(paramers, AccessKeySecret);

                int retryTimes = 1;
                var reply = await HttpGetAsync(url);
                while (500 <= reply.StatusCode && AutoRetry && retryTimes < MaxRetryNumber)
                {
                    url = GetSignUrl(paramers, AccessKeySecret);
                    reply = await HttpGetAsync(url);
                    retryTimes++;
                }

                if (!string.IsNullOrEmpty(reply.response))
                {
                    var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(reply.response);
                    if (res != null && res.ContainsKey("Code") && "OK".Equals(res["Code"]))
                    {
                        return (true, response: reply.response);
                    }
                }

                return (false, response: reply.response);
            }
            catch (Exception ex)
            {
                return (false, response: ex.Message);
            }
        }

        private string GetSignUrl(Dictionary<string, string> parameters, string accessSecret)
        {
            var imutableMap = new Dictionary<string, string>(parameters)
            {
                { "Timestamp", FormatIso8601Date(DateTime.Now) },
                { "SignatureMethod", "HMAC-SHA1" },
                { "SignatureVersion", "1.0" },
                { "SignatureNonce", Guid.NewGuid().ToString() },
                { "Action", Action },
                { "Version", Version },
                { "Format", Format },
                { "RegionId", RegionId }
            };
            IDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>(imutableMap, StringComparer.Ordinal);
            StringBuilder canonicalizedQueryString = new();
            foreach (var p in sortedDictionary)
            {
                canonicalizedQueryString.Append('&')
                .Append(PercentEncode(p.Key)).Append('=')
                .Append(PercentEncode(p.Value ?? ""));
            }

            StringBuilder stringToSign = new();
            stringToSign.Append("GET");
            stringToSign.Append(SEPARATOR);
            stringToSign.Append(PercentEncode("/"));
            stringToSign.Append(SEPARATOR);
            stringToSign.Append(PercentEncode(canonicalizedQueryString.ToString().Substring(1)));

            string signature = SignString(stringToSign.ToString(), accessSecret + "&");

            imutableMap.Add("Signature", signature);

            return ComposeUrl(Endpoint, imutableMap);
        }

        private static string FormatIso8601Date(DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.CreateSpecificCulture("en-US"));
        }

        /// <summary>
        /// 签名
        /// </summary>
        public static string SignString(string source, string accessSecret)
        {
            using (var algorithm = new HMACSHA1(Encoding.UTF8.GetBytes(accessSecret.ToCharArray())))
            {
                return Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(source.ToCharArray())));
            }
        }

        private static string ComposeUrl(string endpoint, Dictionary<String, String> parameters)
        {
            StringBuilder urlBuilder = new("");
            urlBuilder.Append("http://").Append(endpoint);
            if (!urlBuilder.ToString().Contains("?", StringComparison.CurrentCulture))
            {
                urlBuilder.Append("/?");
            }
            string query = ConcatQueryString(parameters);
            return urlBuilder.Append(query).ToString();
        }

        private static string ConcatQueryString(Dictionary<string, string> parameters)
        {
            if (null == parameters)
            {
                return null;
            }
            StringBuilder sb = new();

            foreach (var entry in parameters)
            {
                String key = entry.Key;
                String val = entry.Value;

                sb.Append(HttpUtility.UrlEncode(key, Encoding.UTF8));
                if (val != null)
                {
                    sb.Append('=').Append(HttpUtility.UrlEncode(val, Encoding.UTF8));
                }
                sb.Append('&');
            }

            int strIndex = sb.Length;
            if (parameters.Count > 0)
                sb.Remove(strIndex - 1, 1);

            return sb.ToString();
        }

        public static string PercentEncode(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(value);
            foreach (char c in bytes)
            {
                if (text.IndexOf(c) >= 0)
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    stringBuilder.Append('%').Append(
                        string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int)c));
                }
            }
            return stringBuilder.ToString();
        }

        private async Task<(int StatusCode, string response)> HttpGetAsync(string url)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.Proxy = null;
            handler.AutomaticDecompression = DecompressionMethods.GZip;

            using (var http = new HttpClient(handler))
            {
                http.Timeout = new TimeSpan(TimeSpan.TicksPerMillisecond * TimeoutInMilliSeconds);
                HttpResponseMessage response = await http.GetAsync(url);
                return ((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }
    }
    public class SmsObject
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { set; get; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 模板Key
        /// </summary>
        public string TempletKey { set; get; }

        /// <summary>
        /// 短信数据
        /// </summary>
        public IDictionary<string, string> Data { set; get; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public string OutId { set; get; }
    }
}