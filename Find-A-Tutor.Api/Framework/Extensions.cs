using Microsoft.AspNetCore.Builder;

namespace Find_A_Tutor.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
                => builder.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}
