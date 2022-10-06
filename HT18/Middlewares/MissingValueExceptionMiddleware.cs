using _15.Models.Exceptions;
using System.Text;

namespace HT18.Middlewares
{
    public class MissingValueExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public MissingValueExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (MissingValueException ex)
            {
                context.Response.Body.Write(Encoding.UTF8.GetBytes("MissingValueException:\n" + ex.Message));
            }
        }
    }
}
