namespace Web.API.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserId(this HttpContext context)
    {
        return context.Items["UserIdentifier"] as string ?? string.Empty;
    }
}