using _15.Models.Exceptions;
using System.Text;

namespace HT18.Middlewares
{
    public class InvalidValueExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public InvalidValueExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidValueException ex)
            {
                context.Response.Body.Write(Encoding.UTF8.GetBytes("InvalidValueException:\n" + ex.Message));
            }
        }
    }
}
