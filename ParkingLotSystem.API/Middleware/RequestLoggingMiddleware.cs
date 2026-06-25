namespace ParkingLotSystem.API.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;

        _logger.LogInformation(
            "Incoming Request | Method: {Method} | Path: {Path} | Time: {Time}",
            context.Request.Method,
            context.Request.Path,
            startTime.ToString("HH:mm:ss")
        );

        await _next(context);

        var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;

        _logger.LogInformation(
            "Outgoing Response | Status: {StatusCode} | Duration: {Duration}ms",
            context.Response.StatusCode,
            duration
        );
    }
}
