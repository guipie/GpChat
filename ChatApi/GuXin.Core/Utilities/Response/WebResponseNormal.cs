using GuXin.Core.Enums;
using GuXin.Core.Extensions;

namespace GuXin.Core.Utilities
{
    public class WebResponseNormal
    {
        public WebResponseNormal()
        {
        }
        public WebResponseNormal(bool status)
        {
            this.status = status;
        }
        public bool status { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string successmessage { get; set; }
        //public string message { get; set; }
        public object data { get; set; }

        public WebResponseNormal OK()
        {
            this.status = true;
            return this;
        }
        public WebResponseNormal Info(int result, string smessage = "")
        {
            this.status = result > 0;
            if (this.status)
                this.successmessage = smessage;
            return this;
        }
        public WebResponseNormal Info(bool result)
        {
            this.status = result;
            return this;
        }
        public WebResponseNormal Info<T>(bool result, T data)
        {
            this.data = data;
            this.status = result;
            return this;
        }
        public WebResponseNormal Info<T>(string smessage, T data)
        {
            this.status = true;
            this.successmessage = smessage;
            this.data = data;
            return this;
        }
        public static WebResponseNormal Instance
        {
            get { return new WebResponseNormal(); }
        }
        public WebResponseNormal OK(string message = null, object data = null)
        {
            this.status = true;
            this.message = message;
            this.data = data;
            return this;
        }
        public WebResponseNormal OK(object data = null)
        {
            this.status = true;
            this.data = data;
            return this;
        }
        public WebResponseNormal OK(ResponseType responseType)
        {
            return Set(responseType, true);
        }
        public WebResponseNormal Error(string message = null)
        {
            this.status = false;
            this.message = message;
            return this;
        }
        public WebResponseNormal Error(ResponseType responseType, string message = "")
        {
            return Set(responseType, message, false);
        }

        public WebResponseNormal Set(ResponseType responseType)
        {
            bool? b = null;
            return this.Set(responseType, b);
        }
        public WebResponseNormal Set(ResponseType responseType, bool? status)
        {
            return this.Set(responseType, null, status);
        }
        public WebResponseNormal Set(ResponseType responseType, string msg)
        {
            bool? b = null;
            return this.Set(responseType, msg, b);
        }
        public WebResponseNormal Set(ResponseType responseType, string msg, bool? status)
        {
            if (status != null)
            {
                this.status = (bool)status;
            }
            this.code = ((int)responseType).ToString();
            if (!string.IsNullOrEmpty(msg))
            {
                message = msg;
                return this;
            }
            message = responseType.GetMsg();
            return this;
        }

    }
}
