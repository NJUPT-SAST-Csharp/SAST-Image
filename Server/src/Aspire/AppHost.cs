using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var orleans = builder
    .AddOrleans(nameof(Aspire.Hosting.Orleans))
    .WithClustering(builder.AddConnectionString("Domain"));

var domain = builder
    .AddProject<Projects.Domain_Silo>("Domain-Silo")
    .WithReference(orleans)
    .WithAdoNetClustering()
    .WithConnectionString("Domain");

var query1 = builder
    .AddProject<Projects.Query_Silo>("Query-Silo-1")
    .WithReference(orleans)
    .WithAdoNetClustering()
    .WithConnectionString("Query")
    .WithConnectionString("Domain")
    .WaitFor(domain);

var query2 = builder
    .AddProject<Projects.Query_Silo>("Query-Silo-2")
    .WithReference(orleans)
    .WithAdoNetClustering()
    .WithConnectionString("Query")
    .WithConnectionString("Domain")
    .WaitFor(domain);
var query3 = builder
    .AddProject<Projects.Query_Silo>("Query-Silo-3")
    .WithReference(orleans)
    .WithAdoNetClustering()
    .WithConnectionString("Query")
    .WithConnectionString("Domain")
    .WaitFor(domain);

var storage = builder
    .AddProject<Projects.Storage_Silo>("Storage-Silo")
    .WithReference(orleans)
    .WithAdoNetClustering()
    .WithConnectionString("Domain")
    .WithConnectionString("Storage")
    .WaitFor(domain);

builder
    .AddProject<Projects.WebApp>("webapp")
    .WithReference(orleans.AsClient())
    .WithAdoNetClustering()
    .WithConnectionString("Domain")
    .WaitFor(query1)
    .WaitFor(query2)
    .WaitFor(query3)
    .WaitFor(storage);

builder
    .AddProject<Projects.WebApi>("webapi")
    .WithReference(orleans.AsClient())
    .WithAdoNetClustering()
    .WithConnectionString("Domain")
    .WithConnectionString("Query")
    .WithConnectionString("Storage")
    .WaitFor(domain);

builder.Build().Run();

file static class Rua
{
    static int port = 30000;
    extension<T>(IResourceBuilder<T> resource)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        public IResourceBuilder<T> WithAdoNetClustering()
        {
            return resource
                .WithEnvironment("Orleans__Clustering__ProviderType", "AdoNet")
                .WithEnvironment("Orleans__Silo__SiloName", resource.Resource.Name)
                //.WithEnvironment("Orleans__Endpoints__AdvertisedIPAddress", "127.0.0.1")
                .WithEnvironment("Orleans__Endpoints__GatewayPort", (++port).ToString())
                .WithEnvironment("Orleans__Endpoints__SiloPort", (++port).ToString());
        }

        public IResourceBuilder<T> WithConnectionString(string name) =>
            resource.WithEnvironment(
                $"ConnectionStrings__{name}",
                resource.ApplicationBuilder.Configuration.GetConnectionString(name)
            );
    }
}
