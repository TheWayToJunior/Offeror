using System.Text;

namespace Offeror.TelegramBot.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            var builder = new StringBuilder();

            builder.Append(ex.Message);

            if (ex.InnerException is not null)
            {
                builder.Append(ex.InnerException.Message);
            }

            _logger.LogError(builder.ToString());

            return Task.CompletedTask;
        }
    }
}
