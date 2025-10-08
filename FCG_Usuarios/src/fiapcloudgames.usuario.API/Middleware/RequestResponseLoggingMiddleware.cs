using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace fiapcloudgames.usuario.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = Guid.NewGuid().ToString();
            context.Items["CorrelationId"] = correlationId;

            // Log request
            await LogRequestAsync(context, correlationId);

            // Capture response
            var originalResponseBody = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred. CorrelationId: {CorrelationId}", correlationId);
                await HandleExceptionAsync(context, ex, correlationId);
            }
            finally
            {
                stopwatch.Stop();
                
                // Log response
                await LogResponseAsync(context, correlationId, stopwatch.ElapsedMilliseconds);
                
                // Copy response back to original stream
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBody);
            }
        }

        private async Task LogRequestAsync(HttpContext context, string correlationId)
        {
            var request = context.Request;
            
            var requestLog = new
            {
                CorrelationId = correlationId,
                Method = request.Method,
                Path = request.Path.Value,
                QueryString = request.QueryString.Value,
                Headers = request.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
                UserAgent = request.Headers.UserAgent.ToString(),
                RemoteIP = context.Connection.RemoteIpAddress?.ToString(),
                Timestamp = DateTime.UtcNow
            };

            _logger.LogInformation("HTTP Request: {RequestLog}", JsonSerializer.Serialize(requestLog));

            // Log request body for POST/PUT requests
            if (request.Method == "POST" || request.Method == "PUT")
            {
                var body = await ReadRequestBodyAsync(request);
                if (!string.IsNullOrEmpty(body))
                {
                    _logger.LogInformation("Request Body - CorrelationId: {CorrelationId}, Body: {Body}", correlationId, body);
                }
            }
        }

        private async Task LogResponseAsync(HttpContext context, string correlationId, long elapsedMilliseconds)
        {
            var response = context.Response;
            
            var responseLog = new
            {
                CorrelationId = correlationId,
                StatusCode = response.StatusCode,
                ContentType = response.ContentType,
                ElapsedMilliseconds = elapsedMilliseconds,
                Timestamp = DateTime.UtcNow
            };

            var logLevel = response.StatusCode >= 400 ? LogLevel.Warning : LogLevel.Information;
            _logger.Log(logLevel, "HTTP Response: {ResponseLog}", JsonSerializer.Serialize(responseLog));

            // Log response body for errors
            if (response.StatusCode >= 400)
            {
                var body = await ReadResponseBodyAsync(context.Response);
                if (!string.IsNullOrEmpty(body))
                {
                    _logger.LogWarning("Error Response Body - CorrelationId: {CorrelationId}, Body: {Body}", correlationId, body);
                }
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength ?? 0)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            request.Body.Position = 0;
            return Encoding.UTF8.GetString(buffer);
        }

        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Error = new
                {
                    Message = "An internal server error occurred.",
                    CorrelationId = correlationId,
                    Timestamp = DateTime.UtcNow
                }
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}