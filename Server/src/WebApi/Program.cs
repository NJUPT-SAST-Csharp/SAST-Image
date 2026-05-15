using System.Security.Claims;
using System.Text;
using Domain;
using Domain.Album.Image;
using Domain.User;
using WebApi.Hubs;
using WebApi.Services;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Orleans.Serialization;
using Query;
using Storage;
using Storage.Image;
using Storage.Image.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// JWT Authentication
var secKey = builder.Configuration.GetValue<string>("Auth:SecKey")!;
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        // Support access_token in query string for SignalR WebSocket connections
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                    context.Token = accessToken;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSignalR().AddJsonProtocol();

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
builder.Services.AddSingleton<JwtTokenService>();

// Register S3 as the IImageFileManager at host level
// TODO: restore S3 when ready
// builder.Services.AddS3Storage(builder.Configuration, startRustFs: false);

builder.UseOrleansClient(client =>
{
    client.UseAdoNetClustering(options =>
    {
        options.Invariant = "Npgsql";
        options.ConnectionString = client.Configuration.GetConnectionString("Domain");
    });
    client.Services.AddQuery(client.Configuration);
    client.Services.AddStorage(client.Configuration);
    // TODO: restore S3 when ready
    // client.Services.AddS3Storage(client.Configuration);
    client.Services.AddSerializer(b => b.AddJsonSerializer(t => t.Namespace!.StartsWith("Domain")));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();

// SignalR Hub
app.MapHub<MainHub>("/hub");

await app.RunAsync();
