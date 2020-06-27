using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace cinema_core.ErrorHandle
{
    public class ErrorHandling
    {
        private readonly RequestDelegate next;
        public ErrorHandling(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, CustomException ex)
        {
            Error result = null;
            context.Response.ContentType = "application/json";
            if (ex is CustomException)
            {
                result = new Error() { message = ex.Message, statusCode = (int)ex.StatusCode };
                context.Response.StatusCode = (int)ex.StatusCode;
            }
            else
            {
                result = new Error() { message = "Runtime Error", statusCode = (int)HttpStatusCode.BadRequest };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Error result = new Error() { message = exception.Message, statusCode = (int)HttpStatusCode.InternalServerError };
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
