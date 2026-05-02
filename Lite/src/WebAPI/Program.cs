using System.Text.Json;
using Infrastructure;
using Microsoft.AspNetCore.OpenApi.Generated;
using WebAPI.Exceptions;
using WebAPI.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();
builder.Services.AddExceptionHandlers();
builder.Services.AddResponseCaching();
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi(options => options.AddSchemaTransformer<OpenApiSchemaTransformer>());
builder.Logging.AddLogger();
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringLongConverter());
    });

var app = builder.Build();

app.MapHealthChecks("/api/v1/health");

if (app.Environment.IsDevelopment())
    app.MapOpenApi("/api/v1.json");

app.RunBackend();
