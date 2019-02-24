using BookSystem.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseNotFoundExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<EntityNotFoundMiddleware>();
        }
    }
}
