
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuXin.Core.Configuration;
using GuXin.Core.Services;

namespace GuXin.Core.Utilities
{
    public class AliyunManage
    {
        /// <summary>
        /// 验证码获取
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        public static async Task<(bool, string)> SendRegisterSmsAsync(string phoneNo)
        {
            AliyunSmsSdk sdk = new(
                                    AppSetting.AliyunConfig.AccessKey,
                                    AppSetting.AliyunConfig.AccessKeySecret,
                                    AppSetting.AliyunConfig.RegionId,
                                    AppSetting.AliyunConfig.Endpoint);
            var code = new Random().Next(1000, 9999).ToString();
            SmsObject smsObject = new()
            {
                Mobile = phoneNo,
                Signature = AppSetting.AliyunConfig.RegisterTemp.SignName,
                TempletKey = AppSetting.AliyunConfig.RegisterTemp.TemplateCode,
                Data = new Dictionary<string, string>() { { "code", code } }
            };
            var (success, response) = await sdk.Send(smsObject);
            if (success) return new(true, code);
            else
            {
                Logger.Error(Enums.LoggerType.Aliyun, phoneNo, response);
                return new(false, code);
            }
        }
    }
}
