using API.Canina.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace API.Canina.Extensions
{
	public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app) {
            app.UseMiddleware<ErrorHandlerMiddlewares>();
        }
    }
}
