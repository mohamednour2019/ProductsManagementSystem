using ProductManagementSystem.Domain.Base.Dto;


namespace ProductManagementSystem.API.Middlewares
{
    public class GlobalExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) {
                var response = new ApiResponse<bool>();
                response.CreateFailedResponse(false,new List<string>() { ex.Message});
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(response);
            }

        }
    }


    public static class GlobalExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandleMiddleware>();
        }
    }
}
