using GuXin.Core.Enums;
using GuXin.Core.Extensions;

namespace GuXin.Core.Utilities
{
    public class WebResponseContent
    {
        public WebResponseContent()
        {
        }
        public WebResponseContent(bool status)
        {
            this.Status = status;
        }
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string SuccessMessage { get; set; }
        //public string Message { get; set; }
        public object Data { get; set; }

        public WebResponseContent OK()
        {
            this.Status = true;
            return this;
        }
        public WebResponseContent Info(int result, string smessage = "")
        {
            this.Status = result > 0;
            if (this.Status)
                this.SuccessMessage = smessage;
            return this;
        }
        public WebResponseContent Info(bool result)
        {
            this.Status = result;
            return this;
        }
        public WebResponseContent Info<T>(bool result, T data)
        {
            this.Data = data;
            this.Status = result;
            return this;
        }
        public WebResponseContent Info<T>(string smessage, T data)
        {
            this.Status = true;
            this.SuccessMessage = smessage;
            this.Data = data;
            return this;
        }
        public WebResponseContent Info(string smessage, bool result)
        {
            this.Status = result;
            this.SuccessMessage = smessage;
            return this;
        }
        public static WebResponseContent Instance
        {
            get { return new WebResponseContent(); }
        }
        public WebResponseContent OK(string message = null, object data = null)
        {
            this.Status = true;
            this.Message = message;
            this.Data = data;
            return this;
        }
        public WebResponseContent OK(object data = null)
        {
            this.Status = true;
            this.Data = data;
            return this;
        }
        public WebResponseContent OK(ResponseType responseType)
        {
            return Set(responseType, true);
        }
        public WebResponseContent Error(string message = null)
        {
            this.Status = false;
            this.Message = message;
            return this;
        }
        public WebResponseContent Error(ResponseType responseType, string message = "")
        {
            return Set(responseType, message, false);
        }

        public WebResponseContent Set(ResponseType responseType)
        {
            bool? b = null;
            return this.Set(responseType, b);
        }
        public WebResponseContent Set(ResponseType responseType, bool? status)
        {
            return this.Set(responseType, null, status);
        }
        public WebResponseContent Set(ResponseType responseType, string msg)
        {
            bool? b = null;
            return this.Set(responseType, msg, b);
        }
        public WebResponseContent Set(ResponseType responseType, string msg, bool? status)
        {
            if (status != null)
            {
                this.Status = (bool)status;
            }
            this.Code = ((int)responseType).ToString();
            if (!string.IsNullOrEmpty(msg))
            {
                Message = msg;
                return this;
            }
            Message = responseType.GetMsg();
            return this;
        }

    }
}
