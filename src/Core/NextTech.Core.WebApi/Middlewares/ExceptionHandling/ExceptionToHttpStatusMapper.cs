using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NextTech.Core.WebApi.Middlewares.ExceptionHandling
{
    public static class ExceptionToHttpStatusMapper
    {
        public static Func<Exception, HttpStatusCode>? CustomMap { get; set; }

        public static HttpStatusCodeInfo Map(Exception exception)
        {
            var code = exception switch
            {
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                NotImplementedException _ => HttpStatusCode.NotImplemented,
                InvalidOperationException _ => HttpStatusCode.Conflict,
                ArgumentException _ => HttpStatusCode.BadRequest,
                ValidationException _ => HttpStatusCode.BadRequest,
                _ => CustomMap?.Invoke(exception) ?? HttpStatusCode.InternalServerError
            };

            return new HttpStatusCodeInfo(code, exception.Message);
        }
    }
}
