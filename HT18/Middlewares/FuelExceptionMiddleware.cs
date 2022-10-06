using _15.Models.Exceptions;
using System.Text;

namespace HT18.Middlewares
{
    public class FuelExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public FuelExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FuelException ex)
            {
                context.Response.Body.Write(Encoding.UTF8.GetBytes("FuelException:\n" + ex.Message));
            }
        }
    }
}
