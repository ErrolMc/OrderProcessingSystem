using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace OPS.Shared.Exceptions.Handlers
{
    public sealed class MongoExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<MongoExceptionHandler> _logger;

        public MongoExceptionHandler(ILogger<MongoExceptionHandler> logger)
        {
            _logger = logger;
        }
        
        public async ValueTask<bool> TryHandleAsync(HttpContext http, Exception ex, CancellationToken ct)
        {
            if (ex is MongoWriteException { WriteError.Category: ServerErrorCategory.DuplicateKey })
            {
                _logger.LogWarning(ex, "Duplicate key during Mongo write. TraceId: {TraceId}", http.TraceIdentifier);

                var pd = new ProblemDetails
                {
                    Title = "Duplicate value",
                    Detail = "A record with the same unique value already exists.",
                    Status = StatusCodes.Status409Conflict,
                    Instance = http.TraceIdentifier
                };

                http.Response.StatusCode = pd.Status.Value;
                await http.Response.WriteAsJsonAsync(pd, cancellationToken: ct);
                return true;
            }

            if (ex is MongoException)
            {
                _logger.LogError(ex, "Database unavailable. TraceId: {TraceId}", http.TraceIdentifier);

                var pd = new ProblemDetails
                {
                    Title = "Database unavailable",
                    Detail = "We’re having trouble reaching the database. Please try again later.",
                    Status = StatusCodes.Status503ServiceUnavailable,
                    Instance = http.TraceIdentifier
                };

                http.Response.StatusCode = pd.Status.Value;
                await http.Response.WriteAsJsonAsync(pd, cancellationToken: ct);
                return true;
            }
            
            return false; // not handled, let the next handler try
        }
    }
}

