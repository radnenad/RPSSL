using System.Security.Cryptography;
using System.Text;

namespace Web.API.Middleware;

public class UserIdentifierMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserIdentifierMiddleware> _logger;

    public UserIdentifierMiddleware(RequestDelegate next, ILogger<UserIdentifierMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsRelevantEndpoint(context.Request.Path.Value))
        {
            var userIdentifier = GetHashedIdentifier(context);
            context.Items["UserIdentifier"] = userIdentifier;
            _logger.LogInformation($"Determined user identifier: {userIdentifier}");
        }

        await _next(context);
    }

    private static bool IsRelevantEndpoint(string? path)
    {
        return path != null && 
               (path.Equals("/play", StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("/scoreboard", StringComparison.OrdinalIgnoreCase));
    }

    private static string GetHashedIdentifier(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var userAgent = context.Request.Headers["User-Agent"].ToString();

        var combinedValue = $"{ipAddress}-{userAgent}";
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(combinedValue));

        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}