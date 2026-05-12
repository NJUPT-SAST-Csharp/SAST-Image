using Domain;
using Microsoft.EntityFrameworkCore;
using Orleans.Dashboard;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.UseOrleans(builder =>
{
    builder.UseDomain();
    builder.AddDashboard(options => options.HideTrace = true);
    builder.UseAdoNetClustering(options =>
    {
        options.Invariant = nameof(Npgsql);
        options.ConnectionString = builder.Configuration.GetConnectionString(nameof(Domain));
    });
});

var app = builder.Build();

app.MapOrleansDashboard();

app.MapDefaultEndpoints();
app.UseHttpsRedirection();

await app.StartAsync();

await app.WaitForShutdownAsync();
