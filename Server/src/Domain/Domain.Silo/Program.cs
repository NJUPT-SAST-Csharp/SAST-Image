using Domain;
using Microsoft.EntityFrameworkCore;
using Orleans.Dashboard;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.UseOrleans(builder =>
{
    builder.UseAdoNetClustering(options =>
    {
        options.Invariant = nameof(Npgsql);
        options.ConnectionString = builder.Configuration.GetConnectionString(nameof(Domain));
    });

    //builder.Services.Configure<EndpointOptions>(
    //    builder.Configuration.GetRequiredSection("Orleans:Endpoints")
    //);
    //builder.Services.Configure<SiloOptions>(
    //    builder.Configuration.GetRequiredSection("Orleans:Silo")
    //);

    builder.UseDomain();
    builder.AddDashboard(options => options.HideTrace = true);
});

var app = builder.Build();

app.MapOrleansDashboard();

app.MapDefaultEndpoints();
app.UseHttpsRedirection();

await app.StartAsync();

await app.WaitForShutdownAsync();
