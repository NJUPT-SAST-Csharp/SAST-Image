var builder = DistributedApplication.CreateBuilder(args);

builder
    .AddProject<Projects.Silo>("silo")
    .WithEnvironment(
        "ConnectionStrings__Domain",
        "Server=10.0.0.153;Port=5444;Database=server;User Id=postgres;Password=123456"
    )
    .WithEnvironment(
        "ConnectionStrings__Redis",
        "10.0.0.153:6379,password=123456,abortConnect=False"
    );

builder.Build().Run();
