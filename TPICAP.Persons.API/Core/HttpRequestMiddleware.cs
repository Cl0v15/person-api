using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TPICAP.Persons.API.Core
{
    public class HttpRequestMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await LogRequest(context);
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task LogRequest(HttpContext context)
        {
            Log.Information("HTTP Request: " + JsonConvert.SerializeObject(new Request(
                DateTime.Now,
                GetRequestCallerInfo(context),
                await GetRequestBody(context),
                GetRequestUrl(context))));
        }

        private static string GetRequestCallerInfo(HttpContext context) =>
            $"Ip: { context.Request.HttpContext.Connection.RemoteIpAddress?.ToString() } User Agent: { context.Request.Headers["User-Agent"] }";

        private static string GetRequestUrl(HttpContext context) =>
            context.Request.Method + " " + context.Request.Scheme + "://" + context.Request.Host + context.Request.Path + context.Request.QueryString;

        private static async Task<string> GetRequestBody(HttpContext context)
        {
            var body = "";
            var request = context.Request;

            request.EnableBuffering();

            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }

            request.Body.Position = 0;

            return body;
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Log.Error(exception, exception.Message);

            string result;
            var code = HttpStatusCode.InternalServerError;
            var error = exception.Message;

            if (exception is ValidationException)
            {
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { error });
            }
            else
                result = error;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
