using Application;
using Carter;
using Infrastructure;
using Persistence;
using Web.API;
using Web.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DevelopmentCorsPolicy",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence();
builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DevelopmentCorsPolicy");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<UserIdentifierMiddleware>();

app.MapCarter();

app.Run();

public partial class Program { }