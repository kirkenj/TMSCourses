using System.Text;

namespace HT18.Middlewares
{
    public class ArgumentOutOfRangeExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ArgumentOutOfRangeExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                context.Response.Body.Write(Encoding.UTF8.GetBytes("ArgumentOutOfRangeException:\n" + ex.Message));
            }
        }
    }
}
