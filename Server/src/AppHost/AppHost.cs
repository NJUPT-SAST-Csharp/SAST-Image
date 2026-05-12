var builder = DistributedApplication.CreateBuilder(args);

var domainConnection = builder.AddConnectionString("Domain");
var queryConnection = builder.AddConnectionString("Query");
var storageConnection = builder.AddConnectionString("Storage");

var silo = builder.AddProject<Projects.Silo>("Silo").WithReference(domainConnection);

builder
    .AddProject<Projects.Query_Api>("Query-Api")
    .WithReference(queryConnection)
    .WithReference(domainConnection)
    .WaitFor(silo)
    .WithReplicas(3);

builder
    .AddProject<Projects.Storage_Api>("Storage-Api")
    .WithReference(domainConnection)
    .WithReference(storageConnection)
    .WaitFor(silo);

builder.Build().Run();
