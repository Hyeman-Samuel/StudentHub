using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StudentHub.Core.Asp.NetCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("Exception");

        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Your app crashed : {ex}", ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return httpContext.Response.WriteAsync(new ApiResponse
            {

                Code = ApiResponseCode.ERROR,
                Description = ex.Message,
                Errors = new List<string>
                                    {ex.Message?? ex.InnerException.Message}
            }.ToString());

        }
    }


    public interface IAppMiddleware
    {
        Task InvokeAsync(HttpContext httpContext);
    }
}
