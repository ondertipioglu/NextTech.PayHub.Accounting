using Microsoft.AspNetCore.Builder;
using System;
using System.Net;

namespace NextTech.Core.WebApi.Middlewares.ExceptionHandling
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app, Func<Exception, HttpStatusCode>? customMap = null)
        {
            ExceptionToHttpStatusMapper.CustomMap = customMap;
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
