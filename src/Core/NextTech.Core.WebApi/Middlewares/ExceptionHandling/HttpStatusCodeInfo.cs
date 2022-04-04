using System.Net;


namespace NextTech.Core.WebApi.Middlewares.ExceptionHandling
{
    public class HttpStatusCodeInfo
    {
        public HttpStatusCode Code { get; }
        public string Message { get; }

        public HttpStatusCodeInfo(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public static HttpStatusCodeInfo Create(HttpStatusCode code, string message)
        {
            return new HttpStatusCodeInfo(code, message);
        }
    }
}
