using Orleans.Dashboard;
using Orleans.Serialization;
using Storage;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddOpenApi();

builder.UseOrleans(builder =>
{
    builder.UseAdoNetClustering(options =>
    {
        options.Invariant = "Npgsql";
        options.ConnectionString = builder.Configuration.GetConnectionString("Domain");
    });
    builder.AddDashboard(options => options.HideTrace = true);
    builder.Services.AddStorage(builder.Configuration);
    builder.Services.AddSerializer(b =>
        b.AddJsonSerializer(t => t.Namespace!.StartsWith("Domain"))
    );
    builder.Services.AddHostedService<StorageSyncService>();
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapOrleansDashboard();

app.Run();
