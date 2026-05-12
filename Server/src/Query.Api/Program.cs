using Orleans.Serialization;
using Query;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddOpenApi();

builder.UseOrleansClient(client =>
{
    client.UseAdoNetClustering(options =>
    {
        options.Invariant = "Npgsql";
        options.ConnectionString = client.Configuration.GetConnectionString("Domain");
    });
    client.Services.AddQuery(client.Configuration);
    client.Services.AddSerializer(b => b.AddJsonSerializer(t => t.Namespace!.StartsWith("Domain")));
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
