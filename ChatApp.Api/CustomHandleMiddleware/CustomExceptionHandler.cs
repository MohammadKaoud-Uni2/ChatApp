using ChatApp.Api.Dtos;
using System.Text.Json;

namespace ChatApp.Api.CustomHandleMiddleware
{
    public class CustomExceptionHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RequestDelegate _requestDelegate;
        public CustomExceptionHandler(IHttpContextAccessor contextAccessor,RequestDelegate requestDelegate)
        {
            _contextAccessor = contextAccessor;
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                var response = new ApiException(statueCode:StatusCodes.Status500InternalServerError,ex.Message,ex.StackTrace);
               context.Response.ContentType= "application/json";
                context.Response.StatusCode = (int) StatusCodes.Status500InternalServerError;
                
               var option=new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; 
                var json=JsonSerializer.Serialize(response, option);
                await context.Response.WriteAsync(json);
            }

        }
    }
}
