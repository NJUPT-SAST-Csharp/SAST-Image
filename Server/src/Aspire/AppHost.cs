using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var orleans = builder.AddOrleans(nameof(Aspire.Hosting.Orleans)).WithDevelopmentClustering();

var silo = builder
    .AddProject<Projects.Domain_Silo>("Domain-Silo")
    .WithReference(orleans)
    .WithConnectionString("Domain");

builder
    .AddProject<Projects.Query_Silo>("Query-Silo")
    .WithReference(orleans)
    .WithConnectionString("Query")
    .WithConnectionString("Domain")
    .WaitFor(silo)
    .WithReplicas(3);

builder
    .AddProject<Projects.Storage_Silo>("Storage-Silo")
    .WithReference(orleans)
    .WithConnectionString("Domain")
    .WithConnectionString("Storage")
    .WaitFor(silo);

builder.Build().Run();

file static class Rua
{
    extension<T>(IResourceBuilder<T> resource)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        public IResourceBuilder<T> WithConnectionString(string name) =>
            resource.WithEnvironment(
                $"ConnectionStrings__{name}",
                resource.ApplicationBuilder.Configuration.GetConnectionString(name)
            );
    }
}
