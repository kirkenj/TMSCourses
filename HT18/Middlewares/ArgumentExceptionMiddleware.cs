using System.Text;

namespace HT18.Middlewares
{
    public class ArgumentExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ArgumentExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                context.Response.Body.Write(Encoding.UTF8.GetBytes("ArgumentException:\n" + ex.Message));
            }
        }
    }
}
