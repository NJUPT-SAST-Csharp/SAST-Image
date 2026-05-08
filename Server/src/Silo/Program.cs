using Domain;
using Microsoft.EntityFrameworkCore;
using Orleans.Dashboard;
using Query;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.UseOrleans(builder =>
{
    builder.UseDomain();
    builder.Services.AddQuery(builder.Configuration);

    builder.AddDashboard();
    builder.UseRedisClustering(builder.Configuration.GetConnectionString("Redis")!);
});

var app = builder.Build();

app.MapOrleansDashboard();
app.MapDefaultEndpoints();
app.UseHttpsRedirection();

await app.StartAsync();

await app.WaitForShutdownAsync();
