using System.Text;
using System;

namespace WebApi.MiddleWares
{
    public class ExceptionCatchingMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionCatchingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new {ex.Message});
            }
        }
    }
}
