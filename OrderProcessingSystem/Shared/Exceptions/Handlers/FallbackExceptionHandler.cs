using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OPS.Shared.Exceptions.Handlers
{
    public sealed class FallbackExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<FallbackExceptionHandler> _logger;

        public FallbackExceptionHandler(ILogger<FallbackExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext http, Exception ex, CancellationToken ct)
        {
            _logger.LogError(ex, "Unhandled exception. TraceId: {TraceId}", http.TraceIdentifier);
            
            var pd = new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Instance = http.TraceIdentifier
            };
            
            http.Response.StatusCode = pd.Status!.Value;
            await http.Response.WriteAsJsonAsync(pd, cancellationToken: ct);
            return true;
        }
    }
}