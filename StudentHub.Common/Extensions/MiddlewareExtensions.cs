using Microsoft.AspNetCore.Builder;
using StudentHub.Core.Middleware;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Core.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
