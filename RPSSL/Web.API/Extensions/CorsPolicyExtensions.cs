namespace Web.API.Extensions;

public static class CorsPolicyExtensions
{
    private const string PolicyName = "DevCorsPolicy";
    
    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: PolicyName,
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }
    
    public static void UseCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors(PolicyName);
    }
}