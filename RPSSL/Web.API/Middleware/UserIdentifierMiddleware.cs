using System.Security.Cryptography;
using System.Text;

namespace Web.API.Middleware;

public class UserIdentifierMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdentifierMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsRelevantEndpoint(context.Request.Path.Value))
        {
            context.Items["UserIdentifier"] = GetHashedIdentifier(context);
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