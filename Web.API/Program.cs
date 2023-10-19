using Application;
using Carter;
using Infrastructure;
using Persistence;
using Web.API.Extensions;
using Web.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCorsPolicy();

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

app.UseCorsPolicy();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<UserIdentifierMiddleware>();

app.MapCarter();
    
app.Run();

public partial class Program { }