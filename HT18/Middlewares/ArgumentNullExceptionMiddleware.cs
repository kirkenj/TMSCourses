using System.Text;

namespace HT18.Middlewares
{
    public class ArgumentNullExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ArgumentNullExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentNullException ex)
            {
                context.Response.Body.Write(Encoding.UTF8.GetBytes("ArgumentNullException:\n" + ex.Message));
            }
        }
    }
}
