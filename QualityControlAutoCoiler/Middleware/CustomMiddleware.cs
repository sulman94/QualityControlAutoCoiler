using Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _loggerManager;
        private readonly IConfiguration _config;

        public CustomMiddleware(RequestDelegate next, ILoggerManager loggerManager, IConfiguration configuration)
        {
            _next = next;
            _loggerManager = loggerManager;
            _config = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            // Create a unique LogId for this request
            var logId = Guid.NewGuid();

            // Capture Start Time
            var startTime = DateTime.Now;

            // Prepare placeholders for logs
            string requestBody = string.Empty;
            string responseBody = string.Empty;
            bool isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            bool isFormPost = context.Request.Method == "POST" && context.Request.HasFormContentType;
            // Capture details from HttpContext
            var controller = context.Request.RouteValues["controller"]?.ToString();
            var action = context.Request.RouteValues["action"]?.ToString();
            var queryString = context.Request.QueryString.ToString();
            var isAjax = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var httpMethod = context.Request.Method;
            var requestHeaders = string.Join("; ", context.Request.Headers.Select(h => $"{h.Key}: {h.Value}"));
            int statusCode = 0;
            string responseHeaders = string.Empty;

            Exception exception = null;
            var originalResponseBody = context.Response.Body;
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;
            try
            {
                // Capture Request Body
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering(); // Allows reading request body multiple times
                    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0; // Reset stream position
                }

                // Call the next middleware
                await _next(context);

                // Read Response Body
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                responseBodyStream.Seek(0, SeekOrigin.Begin);

                // Copy Response Back to Original Stream
                await responseBodyStream.CopyToAsync(originalResponseBody);
                statusCode = context.Response.StatusCode;
                responseHeaders = string.Join("; ", context.Response.Headers.Select(h => $"{h.Key}: {h.Value}"));
            }
            catch (Exception ex)
            {
                exception = ex;
                throw; // Ensure exception is passed up the pipeline
            }
            finally
            {
                // Capture End Time and Calculate Duration
                var endTime = DateTime.Now;
                var requestDuration = (endTime - startTime).TotalMilliseconds;
                responseBodyStream.Dispose(); // Dispose only at the en
                // Log to the database
                var ado = new AdoContext(_config);
                var _ = ado.InsertLogs(new Logs
                {
                    LogId = logId,
                    Controller = controller,
                    ActionName = action,
                    RequestBody = requestBody,
                    QueryString = queryString,
                    IsAjax = isAjax,
                    IsFormPost = isFormPost,
                    StartTime = startTime,
                    EndTime = endTime,
                    RequestDurationMs = requestDuration,
                    IsException = exception != null,
                    ExceptionDetails = exception?.ToString(),
                    ResponseBody = responseBody,
                    HttpMethod = httpMethod,
                    IpAddress = ipAddress,
                    StatusCode = statusCode,
                    RequestHeaders = requestHeaders,
                    ResponseHeaders = responseHeaders
                });
            }           
        }
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
