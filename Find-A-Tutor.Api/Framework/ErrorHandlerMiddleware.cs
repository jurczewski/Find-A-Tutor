using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Find_A_Tutor.Api.Framework
{
    public class ErrorHandlerMiddleware
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandlerErrorAsync(context, exception);
            }
        }

        private static Task HandlerErrorAsync(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();
            var statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    logger.Trace(e.GetType().Name + ": " + e.Message);
                    break;
                case Exception e when exceptionType == typeof(ArgumentException):
                    statusCode = HttpStatusCode.BadRequest;
                    logger.Trace(e.GetType().Name + ": " + e.Message);
                    break;
                case Exception e when exceptionType == typeof(ValidationException):
                    statusCode = HttpStatusCode.BadRequest;
                    logger.Trace(e.GetType().Name + ": " + e.Message);
                    break;
            }

            var response = Result.Error(exception.Message);
            var payLoad = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payLoad);
        }
    }
}
