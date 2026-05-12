---
name: Orleans
description: Solving Orleans
tools: ["code_search", "readfile", "editfiles", "find_references", "runcommandinterminal", "getwebpages"]
---

### Install Orleans Runtime Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This library is for configuring and starting a silo. Reference it in your silo host project. It is included in the Microsoft.Orleans.Server meta-package.

```powershell
Install-Package Microsoft.Orleans.OrleansRuntime

```

--------------------------------

### Install Orleans Client Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Use this package to easily build and start an Orleans client (frontend). It includes core Orleans packages.

```powershell
Install-Package Microsoft.Orleans.Client

```

--------------------------------

### Aspire Deployment Parameters Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

An example JSON file defining parameters for Aspire deployment, including location, environment, and resource group name.

```json
{
  "location": "eastus",
  "environment": "production",
  "resourceGroupName": "my-orleans-app-rg"
}

```

--------------------------------

### Install Orleans Server Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Use this package to easily build and start an Orleans silo. It includes several other essential Orleans packages.

```powershell
Install-Package Microsoft.Orleans.Server

```

--------------------------------

### Orleans Component Lifecycle Timing Log Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/silo-lifecycle

Shows log entries detailing the time taken for specific lifecycle observers to start within their designated stages. This is useful for performance analysis during startup.

```log
Information, Orleans.Runtime.SiloLifecycleSubject, "Lifecycle observer Orleans.Runtime.InsideRuntimeClient started in stage 2000 which took 33 Milliseconds."

Information, Orleans.Runtime.SiloLifecycleSubject, "Lifecycle observer Orleans.Statistics.PerfCounterEnvironmentStatistics started in stage 2000 which took 17 Milliseconds."
```

--------------------------------

### Azure Deployment Output Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Example output after successfully provisioning and deploying your application to Azure Container Apps. The output includes the endpoint URL for the web application.

```text
Deploying services (azd deploy)

  (✓) Done: Deploying service web
- Endpoint: <https://[container-app-sub-domain].azurecontainerapps.io>

SUCCESS: Your application was provisioned and deployed to Azure in 5 minutes 0 seconds.

```

--------------------------------

### Install Orleans Google Utilities Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the Google Protocol Buffers serializer.

```powershell
Install-Package Microsoft.Orleans.OrleansGoogleUtils
```

--------------------------------

### Install Orleans protobuf-net Serializer Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the protobuf-net version of the Protocol Buffers serializer.

```powershell
Install-Package Microsoft.Orleans.ProtobufNet
```

--------------------------------

### Install ADO.NET Grain Directory NuGet Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/grain-directory

Install the Microsoft.Orleans.GrainDirectory.AdoNet NuGet package using the .NET CLI.

```csharp
dotnet add package Microsoft.Orleans.GrainDirectory.AdoNet

```

--------------------------------

### Install Orleans Testing Host Library Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the library for hosting silos and clients in a testing project.

```powershell
Install-Package Microsoft.Orleans.TestingHost
```

--------------------------------

### Client Work Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/overview-helloworld

Demonstrates how a client interacts with a grain by obtaining a reference to an IHello grain and invoking its SayHello method.

```csharp
static async Task DoClientWork(IClusterClient client)
{
    var friend = client.GetGrain<IHello>(0);
    var response = await friend.SayHello("Good morning, my friend!");
    Console.WriteLine($"\n\n{response}\n\n");
}

```

--------------------------------

### Install Orleans Transactions Support Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include support for cross-grain transactions (beta).

```powershell
Install-Package Microsoft.Orleans.Transactions
```

--------------------------------

### Install Aspire CLI (Bash)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Installs the .NET Aspire CLI using a Bash script. Ensure you have Bash available.

```bash
curl -sSL https://aspire.dev/install.sh | bash

```

--------------------------------

### Install ADO.NET Persistence Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/relational-storage

Install the base package for ADO.NET grain persistence using PowerShell.

```powershell
Install-Package Microsoft.Orleans.Persistence.AdoNet

```

--------------------------------

### Install Aspire CLI (PowerShell)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Installs the .NET Aspire CLI using a PowerShell script. Ensure you have PowerShell available.

```powershell
irm https://aspire.dev/install.ps1 | iex

```

--------------------------------

### Start a Local Orleans Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/local-development-configuration

This code demonstrates how to build and start a local Orleans silo. It configures the silo to use localhost clustering, sets cluster and service IDs, specifies the loopback address, and adds console logging. The silo is then started and kept running until the user presses Enter.

```csharp
public static async Task StartLocalSilo()
{
    try
    {
        var host = await BuildAndStartSiloAsync();

        Console.WriteLine("Press Enter to terminate...");
        Console.ReadLine();

        await host.StopAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

static async Task<IHost> BuildAndStartSiloAsync()
{
    var host = new HostBuilder()
      .UseOrleans(builder =>
      {
          builder.UseLocalhostClustering()
              .Configure<ClusterOptions>(options =>
              {
                  options.ClusterId = "dev";
                  options.ServiceId = "MyAwesomeService";
              })
              .Configure<EndpointOptions>(
                  options => options.AdvertisedIPAddress = IPAddress.Loopback)
              .ConfigureLogging(logging => logging.AddConsole());
      })
      .Build();

    await host.StartAsync();

    return host;
}

```

--------------------------------

### Install Orleans Telemetry Consumer - NewRelic Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the telemetry consumer for NewRelic.

```powershell
Install-Package Microsoft.Orleans.OrleansTelemetryConsumers.NewRelic
```

--------------------------------

### Install Orleans Event-Sourcing Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to use a set of base types for creating grain classes with event-sourced state.

```powershell
Install-Package Microsoft.Orleans.EventSourcing
```

--------------------------------

### Install Orleans Google Cloud Platform Utilities Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the stream provider for GCP PubSub service.

```powershell
Install-Package Microsoft.Orleans.OrleansGCPUtils
```

--------------------------------

### Install Orleans Bond Serializer Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include support for the Bond serializer.

```powershell
Install-Package Microsoft.Orleans.Serialization.Bond
```

--------------------------------

### Implement IHostedService for Initialization Tasks

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/startup-tasks

Implement IHostedService for simpler initialization tasks that don't require continuous background operation. This example initializes a grain upon startup.

```csharp
public class GrainInitializerService : IHostedService
{
    private readonly IGrainFactory _grainFactory;
    private readonly ILogger<GrainInitializerService> _logger;

    public GrainInitializerService(
        IGrainFactory grainFactory,
        ILogger<GrainInitializerService> logger)
    {
        _grainFactory = grainFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Initializing grains...");
        var grain = _grainFactory.GetGrain<IMyGrain>("initializer");
        await grain.Initialize();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Orleans Silo Startup Logging Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/silo-lifecycle

Illustrates the log output indicating which components are participating at specific lifecycle stages during silo startup. This helps in understanding the order of component initialization.

```log
Information, Orleans.Runtime.SiloLifecycleSubject, "Stage 2000: Orleans.Statistics.PerfCounterEnvironmentStatistics, Orleans.Runtime.InsideRuntimeClient, Orleans.Runtime.Silo"

Information, Orleans.Runtime.SiloLifecycleSubject, "Stage 4000: Orleans.Runtime.Silo"

Information, Orleans.Runtime.SiloLifecycleSubject, "Stage 10000: Orleans.Runtime.Versions.GrainVersionStore, Orleans.Storage.AzureTableGrainStorage-Default, Orleans.Storage.AzureTableGrainStorage-PubSubStore"
```

--------------------------------

### Add Aspire.Hosting.Kubernetes NuGet package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/kubernetes

Install the Aspire.Hosting.Kubernetes NuGet package in your AppHost project to enable Kubernetes integration.

```bash
dotnet add package Aspire.Hosting.Kubernetes

```

--------------------------------

### Install Orleans Telemetry Consumer - Performance Counters Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package for the Windows Performance Counters implementation of the Orleans Telemetry API.

```powershell
Install-Package Microsoft.Orleans.OrleansTelemetryConsumers.Counters
```

--------------------------------

### Navigate to Web Project Directory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Change your current working directory to the web project folder before installing NuGet packages. This ensures packages are installed in the correct project context.

```bash
cd ./src/web

```

--------------------------------

### Configure Silo Host with Localhost Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/local-development-configuration

This example shows how to create a default host builder and configure the silo to use localhost clustering. It then runs the silo as a console application.

```csharp
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder.UseLocalhostClustering();
    })
    .RunConsoleAsync();

```

--------------------------------

### Install Orleans Providers Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package for persistence and stream providers that store data in memory. Intended for testing and not generally recommended for production unless data loss is acceptable.

```powershell
Install-Package Microsoft.Orleans.OrleansProviders
```

--------------------------------

### Example Orleans Query with Parameters

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/relational-storage

Demonstrates how Orleans might construct and execute a query using parameters. Ensure that the query parameters and expected return types match Orleans' expectations.

```csharp
const int Param1 = 1;
const DateTime Param2 = DateTime.UtcNow;
const string queryFromOrleansQueryTableWithSomeKey =
    "SELECT column1, column2 "+
    "FROM <some Orleans table> " + 
    "WHERE column1 = @param1 " + 
    "AND column2 = @param2;";
TExpected queryResult =
    SpecificQuery12InOrleans<TExpected>(query, Param1, Param2);

```

--------------------------------

### Register GrainService and its Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

This example demonstrates how to register a GrainService and its corresponding client using the Orleans host builder. Ensure the Microsoft.Orleans.Runtime and Microsoft.Orleans.OrleansRuntime NuGet packages are referenced.

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddGrainService<DataService>();  // Register GrainService
});

// Register Client of GrainService
builder.Services.AddSingleton<IDataServiceClient, DataServiceClient>();

using var host = builder.Build();
await host.RunAsync();

```

--------------------------------

### Implement Implicit Subscription Logic in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

In the grain's OnActivateAsync method, obtain the stream provider, create a stream ID, get the stream reference, and subscribe to it using an asynchronous lambda function to process incoming data. This example uses StreamId.Create with namespace and GUID.

```csharp
// Create a GUID based on our GUID as a grain
var guid = this.GetPrimaryKey();

// Get one of the providers which we defined in config
var streamProvider = GetStreamProvider("StreamProvider");

// Get the reference to a stream
var streamId = StreamId.Create("RANDOMDATA", guid);
var stream = streamProvider.GetStream<int>(streamId);

// Set our OnNext method to the lambda which simply prints the data.
// This doesn't make new subscriptions, because we are using implicit
// subscriptions via [ImplicitStreamSubscription].
await stream.SubscribeAsync<int>(
    async (data, token) =>
    {
        Console.WriteLine(data);
        await Task.CompletedTask;
    });

```

--------------------------------

### Install Orleans Streaming Azure Storage Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the stream provider for Azure Queues.

```powershell
Install-Package Microsoft.Orleans.Streaming.AzureStorage
```

--------------------------------

### Install Orleans Code Generation Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the run-time code generator.

```powershell
Install-Package Microsoft.Orleans.OrleansCodeGenerator
```

--------------------------------

### Implement IGrainBase with Lifecycle Methods

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Implement IGrainBase and override OnActivateAsync and OnDeactivateAsync to participate in the grain's lifecycle. This example includes logging for activation and deactivation.

```csharp
public sealed class PingGrain : IGrainBase, IPingGrain
{
    private readonly ILogger<PingGrain> _logger;

    public PingGrain(IGrainContext context, ILogger<PingGrain> logger)
    {
        _logger = logger;
        GrainContext = context;
    }

    public IGrainContext GrainContext { get; }

    public Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnActivateAsync()");
        return Task.CompletedTask;
    }

    public Task OnDeactivateAsync(DeactivationReason reason, CancellationToken token)
    {
        _logger.LogInformation("OnDeactivateAsync({Reason})", reason);
        return Task.CompletedTask;
    }

    public ValueTask Ping() => ValueTask.CompletedTask;
}
```

--------------------------------

### AWS credentials file example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/dynamodb-storage

An example of an AWS credentials file showing the structure for named profiles, including access keys, secret keys, and tokens. This format is used when configuring Orleans DynamoDB persistence with profiles.

```bash
[YOUR_PROFILE_NAME]
aws_access_key_id = ***
aws_secret_access_key = ***
aws_security_token = ***
aws_session_expiration = ***
aws_session_token = ***


```

--------------------------------

### Complete Program.cs for Stateless Service

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/service-fabric

This example shows the complete Program.cs file for a stateless Service Fabric service that registers an Orleans hosted service. It demonstrates how to use ServiceRuntime.RegisterServiceAsync and configure the Orleans host.

```csharp
using System.Fabric;
using Microsoft.Extensions.Hosting;
using Microsoft.ServiceFabric.Services.Runtime;
using ServiceFabric.HostingExample;

try
{
    // The ServiceManifest.XML file defines one or more service type names.
    // Registering a service maps a service type name to a .NET type.
    // When Service Fabric creates an instance of this service type,
    // an instance of the class is created in this host process.
    await ServiceRuntime.RegisterServiceAsync(
        "Orleans.ServiceFabric.Stateless",
        context => new OrleansHostedStatelessService(
            CreateHostAsync, context));

    ServiceEventSource.Current.ServiceTypeRegistered(
        Environment.ProcessId,
        typeof(OrleansHostedStatelessService).Name);

    // Prevents this host process from terminating so services keep running.
    await Task.Delay(Timeout.Infinite);
}
catch (Exception ex)
{
    ServiceEventSource.Current.ServiceHostInitializationFailed(
        ex.ToString());
    throw;
}

static async Task<IHost> CreateHostAsync(StatelessServiceContext context)
{
    await Task.CompletedTask;

    return Host.CreateDefaultBuilder()
        .UseOrleans((_, builder) =>
        {
            // TODO, Use real storage, something like table storage
            // or SQL Server for clustering.
            builder.UseLocalhostClustering();

            // Service Fabric manages port allocations, so update the 

```

--------------------------------

### Basic InProcessTestCluster Usage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

Demonstrates the basic setup and usage of `InProcessTestCluster` for testing grains. Requires implementing `IAsyncLifetime` for cluster initialization and disposal.

```csharp
using Orleans.TestingHost;
using Xunit;

public class HelloGrainTests : IAsyncLifetime
{
    private InProcessTestCluster _cluster = null!;

    public async Task InitializeAsync()
    {
        var builder = new InProcessTestClusterBuilder();
        _cluster = builder.Build();
        await _cluster.DeployAsync();
    }

    public async Task DisposeAsync()
    {
        await _cluster.DisposeAsync();
    }

    [Fact]
    public async Task SaysHello()
    {
        var grain = _cluster.Client.GetGrain<IHelloGrain>(0);
        var result = await grain.SayHello("World");
        Assert.Equal("Hello, World!", result);
    }
}
```

--------------------------------

### Install Orleans Streaming AWS SQS Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the stream provider for the AWS SQS service.

```powershell
Install-Package Microsoft.Orleans.Streaming.SQS
```

--------------------------------

### Define Multiple Grain Implementations

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

This example shows two different grain implementations, UpCounterGrain and DownCounterGrain, both implementing the ICounterGrain interface. This setup can lead to ambiguity when creating grain references.

```csharp
public interface ICounterGrain : IGrainWithStringKey
{
    ValueTask<int> UpdateCount();
}

public class UpCounterGrain : ICounterGrain
{
    private int _count;

    public ValueTask<string> UpdateCount() => new(++_count); // Increment count
}

public class DownCounterGrain : ICounterGrain
{
    private int _count;

    public ValueTask<string> UpdateCount() => new(--_count); // Decrement count
}
```

--------------------------------

### Implement Custom Partition Key Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Implement the IPartitionKeyProvider interface to define custom logic for determining the partition key. This example uses the grain ID's key as the partition key.

```csharp
public class MyPartitionKeyProvider : IPartitionKeyProvider
{
    public ValueTask<string> GetPartitionKey(string grainType, GrainId grainId)
    {
        // Custom logic to determine partition key
        return new ValueTask<string>(grainId.Key.ToString()!);
    }
}
```

--------------------------------

### Add Cassandra Clustering NuGet Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Install the necessary NuGet package for Cassandra clustering.

```bash
dotnet add package Microsoft.Orleans.Clustering.Cassandra
```

--------------------------------

### Get Simple Message Stream Provider and Stream Reference

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

Obtain a reference to a simple message stream provider and a specific stream identified by a GUID and a string.

```csharp
// Pick a GUID for a chat room grain and chat room stream
var guid = new Guid("some guid identifying the chat room");
// Get one of the providers which we defined in our config
var streamProvider = GetStreamProvider("SMSProvider");
// Get the reference to a stream
var stream = streamProvider.GetStream<int>(guid, "RANDOMDATA");

```

--------------------------------

### Get Stream Provider and Stream Handle

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Obtain a stream provider and a handle to a specific stream using its namespace and primary key. This is the starting point for interacting with Orleans streams.

```csharp
public async Task SetupStream()
{
    IStreamProvider streamProvider = this.GetStreamProvider("SimpleStreamProvider");
    StreamId streamId = StreamId.Create("MyStreamNamespace", this.GetPrimaryKey());
    IAsyncStream<string> stream = streamProvider.GetStream<string>(streamId);
}
```

```csharp
IStreamProvider streamProvider = base.GetStreamProvider("SimpleStreamProvider");
IAsyncStream<T> stream = streamProvider.GetStream<T>(Guid, "MyStreamNamespace");
```

--------------------------------

### V1 Grain Method Implementation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/backward-compatibility-guidelines

Example of a grain method implementation in Version 1.

```csharp
// V1
public interface MyGrain : IMyGrain
{
    // First method
    Task MyMethod(int arg)
    {
        SomeSubRoutine(arg);
    }
}
```

--------------------------------

### Install Orleans Performance Counter Tool Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include `OrleansCounterControl.exe`, which registers Windows performance counter categories for Orleans statistics and deployed grain classes. Requires elevation and can be executed in Azure as part of a role startup task.

```powershell
Install-Package Microsoft.Orleans.CounterControl
```

--------------------------------

### Get Memory Stream Provider and Stream Reference

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

Obtain a reference to a memory stream provider and a specific stream identified by a GUID and namespace.

```csharp
// Pick a GUID for a chat room grain and chat room stream
var guid = new Guid("some guid identifying the chat room");
// Get one of the providers which we defined in our config
var streamProvider = GetStreamProvider("StreamProvider");
// Get the reference to a stream
var streamId = StreamId.Create("RANDOMDATA", guid);
var stream = streamProvider.GetStream<int>(streamId);

```

--------------------------------

### Subscribe to Orleans Stream with Specific GUID and Namespace

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

This code snippet demonstrates subscribing to an Orleans stream using a specific grain GUID and a stream namespace. It retrieves the stream provider, gets the stream reference, and sets up an OnNextAsync handler to process incoming integer data.

```csharp
// Create a GUID based on our GUID as a grain
var guid = this.GetPrimaryKey();

// Get one of the providers which we defined in config
var streamProvider = GetStreamProvider("SMSProvider");

// Get the reference to a stream
var stream = streamProvider.GetStream<int>(guid, "RANDOMDATA");

// Set our OnNext method to the lambda which simply prints the data.
// This doesn't make new subscriptions, because we are using implicit
// subscriptions via [ImplicitStreamSubscription].
await stream.SubscribeAsync<int>(
    async (data, token) =>
    {
        Console.WriteLine(data);
        await Task.CompletedTask;
    });

```

--------------------------------

### Install Orleans Runtime Abstractions Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package contains interfaces and abstractions for types implemented in Microsoft.Orleans.OrleansRuntime.

```powershell
Install-Package Microsoft.Orleans.Runtime.Abstractions

```

--------------------------------

### Initialize Custom Grain Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Subscribes to the ServiceLifecycleStage.ApplicationServices to ensure the storage directory is created before application services start.

```csharp
public void Participate(ISiloLifecycle lifecycle) =>
    lifecycle.Subscribe(
        observerName: OptionFormattingUtilities.Name<FileGrainStorage>(_storageName),
        stage: ServiceLifecycleStage.ApplicationServices,
        onStart: (ct) =>
        {
            Directory.CreateDirectory(_options.RootDirectory);
            return Task.CompletedTask;
        });

```

--------------------------------

### Configure Log Consistency Providers in XML

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/event-sourcing-configuration

Add log-consistency providers to the `<Globals>` element of the configuration file. This example includes all three built-in providers.

```xml
<LogConsistencyProviders>
    <Provider Name="StateStorage"
        Type="Orleans.EventSourcing.StateStorage.LogConsistencyProvider" />
    <Provider Name="LogStorage"
        Type="Orleans.EventSourcing.LogStorage.LogConsistencyProvider" />
    <Provider Name="CustomStorage"
        Type="Orleans.EventSourcing.CustomStorage.LogConsistencyProvider" />
</LogConsistencyProviders>
```

--------------------------------

### Install Orleans Service Fabric Hosting Support Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package contains helper classes for hosting silos as a stateless Service Fabric service.

```powershell
Install-Package Microsoft.Orleans.Hosting.ServiceFabric

```

--------------------------------

### Install Cosmos DB Persistence Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Install the necessary NuGet package for Azure Cosmos DB grain persistence in your Orleans project.

```bash
dotnet add package Microsoft.Orleans.Persistence.Cosmos

```

--------------------------------

### Grain Lifecycle Methods in Orleans 7.0

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Example demonstrating the updated signatures for OnActivateAsync and OnDeactivateAsync in Orleans 7.0. These methods now accept CancellationToken and DeactivationReason parameters for better control and diagnostics.

```csharp
public sealed class PingGrain : Grain, IPingGrain
{
    private readonly ILogger<PingGrain> _logger;

    public PingGrain(ILogger<PingGrain> logger) =>
        _logger = logger;

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnActivateAsync()");
        return Task.CompletedTask;
    }

    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken token)
    {
        _logger.LogInformation("OnDeactivateAsync({Reason})", reason);
        return Task.CompletedTask;
    }

    public ValueTask Ping() => ValueTask.CompletedTask;
}
```

--------------------------------

### Client Project: Production Configuration with Redis

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configures a client application with service defaults and registers a keyed Redis client. This setup is for applications that consume Orleans services.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedRedisClient("redis");
builder.UseOrleansClient();

var app = builder.Build();
// ... configure API endpoints
app.Run();

```

--------------------------------

### Registering Orleans Client with Host Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Configure the .NET Generic Host to use Orleans client services. This example demonstrates setting up local clustering and registering the custom hosted service.

```csharp
await Host.CreateDefaultBuilder(args)
    .UseOrleansClient(builder =>
    {
        builder.UseLocalhostClustering();
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<ClusterClientHostedService>();
    })
    .RunConsoleAsync();

```

--------------------------------

### Run Silo Project

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/tutorial-1

Command to build and run the Silo project from the command line. This starts the server that hosts and runs the grains.

```bash
dotnet run

```

--------------------------------

### Install Orleans Transactions on Azure Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include a plugin for persisting transaction logs in Azure Table (beta).

```powershell
Install-Package Microsoft.Orleans.Transactions.AzureStorage
```

--------------------------------

### Silo Configuration for In-Memory Streaming

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/stream-providers

Basic Silo project setup for in-memory streaming. No specific client registration is needed for in-memory providers.

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### Deploy Azure Resources and Sample App

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Deploy the Azure Cosmos DB for NoSQL account and a sample web application using `azd up`. Select your Azure subscription and desired location during provisioning. This process takes approximately five minutes.

```bash
azd up

```

--------------------------------

### Connect Orleans Client to Local Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/local-development-configuration

This C# example demonstrates how to build and connect an Orleans client to a local silo. It configures the client with localhost clustering, cluster and service IDs, and logging.

```C#
public static async Task StartLocalClient()
{
    var client = new ClientBuilder()
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "MyAwesomeService";
        })
        .ConfigureLogging(logging => logging.AddConsole())
        .Build();

    await client.Connect();
}

```

--------------------------------

### Register BackgroundService with Host Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/startup-tasks

Register the BackgroundService after configuring Orleans. This ensures the background service starts only after the Orleans silo has initialized.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configure Orleans first
builder.UseOrleans(siloBuilder => 
{
    // Orleans configuration...
});

// Register the background service after calling 'UseOrleans' to make it start once Orleans has started.
builder.Services.AddHostedService<GrainPingService>();

var app = builder.Build();

```

--------------------------------

### Install Orleans Telemetry Consumer - Azure Application Insights Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install this package to include the telemetry consumer for Azure Application Insights.

```powershell
Install-Package Microsoft.Orleans.OrleansTelemetryConsumers.AI
```

--------------------------------

### Install Orleans ServiceBus Utilities Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the stream provider for Azure Event Hubs.

```powershell
Install-Package Microsoft.Orleans.OrleansServiceBus

```

--------------------------------

### Register a Delegate as a Startup Task

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/startup-tasks

Use this method to register a delegate as a startup task. It requires access to the service provider and cancellation token.

```csharp
siloBuilder.AddStartupTask(
    async (IServiceProvider services, CancellationToken cancellation) =>
    {
        var grainFactory = services.GetRequiredService<IGrainFactory>();
        var grain = grainFactory.GetGrain<IMyGrain>("startup-task-grain");
        await grain.Initialize();
    });


```

--------------------------------

### Install Orleans Build-Time Code Generation (Legacy)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install the build-time support package for grain interfaces and implementation projects. This package enables code generation for grain references and serializers. Appeared in Orleans 1.2.0.

```powershell
Install-Package Microsoft.Orleans.OrleansCodeGenerator.Build
```

--------------------------------

### Install Orleans Core Abstractions

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install the core abstractions package for Orleans. This package is required for developing application code, including grain interfaces and classes, and must be referenced directly or indirectly by any Orleans project.

```powershell
Install-Package Microsoft.Orleans.Core.Abstractions
```

--------------------------------

### Start a standalone Consul agent

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/consul-deployment

Run a standalone Consul agent in server mode. This command starts a Consul agent that acts as a server, bootstraps a new cluster, and listens for client connections on all interfaces. Ensure the data directory exists before running.

```powershell
./consul.exe agent -server -bootstrap -data-dir "C:\Consul\Data" -client='0.0.0.0'
```

--------------------------------

### Initialize Orleans URL Shortener Sample

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Initialize the Orleans URL shortener sample application using the Azure Developer CLI template. Configure a unique environment name during initialization.

```bash
azd init --template orleans-url-shortener

```

--------------------------------

### Install Orleans Persistence ADO.NET Providers Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using ADO.NET for storing grain state in one of the supported databases.

```powershell
Install-Package Microsoft.Orleans.Persistence.AdoNet

```

--------------------------------

### Multi-Stage Lifecycle Observer in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/orleans-lifecycle

Implement ILifecycleParticipant to subscribe to multiple lifecycle stages. Use dictionaries to track when stages start and stop.

```csharp
enum TestStages
{
    Down,
    Initialize,
    Configure,
    Run,
};

class MultiStageObserver : ILifecycleParticipant<ILifecycleObservable>
{
    public Dictionary<TestStages,bool> Started { get; }
    public Dictionary<TestStages, bool> Stopped { get; }

    private Task OnStartStage(TestStages stage)
    {
        Started[stage] = true;

        return Task.CompletedTask;
    }

    private Task OnStopStage(TestStages stage)
    {
        Stopped[stage] = true;

        return Task.CompletedTask;
    }

    public void Participate(ILifecycleObservable lifecycle)
    {
        lifecycle.Subscribe<MultiStageObserver>(
            (int)TestStages.Down,
            _ => OnStartStage(TestStages.Down),
            _ => OnStopStage(TestStages.Down));

        lifecycle.Subscribe<MultiStageObserver>(
            (int)TestStages.Initialize,
            _ => OnStartStage(TestStages.Initialize),
            _ => OnStopStage(TestStages.Initialize));

        lifecycle.Subscribe<MultiStageObserver>(
            (int)TestStages.Configure,
            _ => OnStartStage(TestStages.Configure),
            _ => OnStopStage(TestStages.Configure));

        lifecycle.Subscribe<MultiStageObserver>(
            (int)TestStages.Run,
            _ => OnStartStage(TestStages.Run),
            _ => OnStopStage(TestStages.Run));
    }
}
```

--------------------------------

### Initialize Silo with Localhost Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/tutorial-1

Configures the host to use Orleans with the localhost clustering provider. This setup is suitable for local development without external storage.

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole());
    })
    .UseConsoleLifetime();

using IHost host = builder.Build();

await host.RunAsync();

```

--------------------------------

### Component Participation in Grain Lifecycle (IGrainActivationContext)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Similar to `IGrainContext`, this example shows a component participating in the grain's lifecycle using `IGrainActivationContext.ObservableLifecycle`. The `Create` factory method handles the subscription.

```csharp
public class MyComponent : ILifecycleParticipant<IGrainLifecycle>
{
    public static MyComponent Create(IGrainActivationContext context)
    {
        var component = new MyComponent();
        component.Participate(context.ObservableLifecycle);
        return component;
    }

    public void Participate(IGrainLifecycle lifecycle)
    {
        lifecycle.Subscribe<MyComponent>(GrainLifecycleStage.Activate, OnActivate);
    }

    private Task OnActivate(CancellationToken ct)
    {
        // Do stuff
    }
}
```

--------------------------------

### URL Shortener API Response Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Example JSON response from the URL shortener application's `shorten` endpoint. It shows the original URL and the generated shortened URL.

```json
{
  "original": "https://www.microsoft.com",
  "shortened": "http://<container-app-name>.<deployment-name>.<region>.azurecontainerapps.io:<port>/go/<generated-id>"
}

```

--------------------------------

### Install Orleans Client NuGet Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/local-development-configuration

Use this PowerShell command to add the Microsoft.Orleans.Client NuGet package to your project. This package includes the necessary components for client development.

```powershell
Install-Package Microsoft.Orleans.Client

```

--------------------------------

### Install Orleans Build-Time Code Generation (MSBuild)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

Install the MSBuild-based code generation package, an alternative to Microsoft.Orleans.OrleansCodeGenerator.Build. It leverages Roslyn for code analysis, avoiding application binary loading and improving incremental build times. Appeared as part of Orleans 2.1.0.

```powershell
Install-Package Microsoft.Orleans.CodeGenerator.MSBuild
```

--------------------------------

### Azure Table Storage Connection String Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

An example of a connection string for Azure Table storage, used for configuring Orleans clustering. Avoid using connection strings in production; prefer Microsoft Entra ID authentication.

```plaintext
"DefaultEndpointsProtocol=https;AccountName=<Azure storage account>;AccountKey=<Azure table storage account key>"
```

--------------------------------

### Initialize JSON Settings and Storage Directory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Configure JSON serializer settings and create the storage directory if it does not exist. Settings can be made configurable via options.

```csharp
private Task Init(CancellationToken ct)
{
    // Settings could be made configurable from Options.
    _jsonSettings =
        OrleansJsonSerializer.UpdateSerializerSettings(
            OrleansJsonSerializer.GetDefaultSerializerSettings(
                _typeResolver,
                _grainFactory),
            false,
            false,
            null);

    var directory = new DirectoryInfo(_options.RootDirectory);
    if (!directory.Exists)
        directory.Create();

    return Task.CompletedTask;
}
```

--------------------------------

### Basic Orleans Cluster with Redis Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configure the AppHost to define a basic Orleans cluster using Redis for clustering. This example also sets up the silo project with references and replica configuration.

```csharp
public static void BasicOrleansCluster(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    // Add Redis for Orleans clustering
    var redis = builder.AddRedis("orleans-redis");

    // Define the Orleans resource with Redis clustering
    var orleans = builder.AddOrleans("cluster")
        .WithClustering(redis);

    // Add the Orleans silo project
    builder.AddProject<Projects.Silo>("silo")
        .WithReference(orleans)
        .WaitFor(redis)
        .WithReplicas(3);

    builder.Build().Run();
}
```

--------------------------------

### Install Orleans Persistence DynamoDB Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using AWS DynamoDB for storing grain state.

```powershell
Install-Package Microsoft.Orleans.Persistence.DynamoDB

```

--------------------------------

### Install Orleans Consul Utilities Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using Consul for storing cluster membership data.

```powershell
Install-Package Microsoft.Orleans.OrleansConsulUtils

```

--------------------------------

### Client: Create and Subscribe Observer

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/observers

On the client, create a grain reference, instantiate the observer class, create an object reference for the observer, and then subscribe to the grain.

```csharp
//First create the grain reference
var friend = _grainFactory.GetGrain<IHello>(0);
Chat c = new Chat();

//Create a reference for chat, usable for subscribing to the observable grain.
var obj = _grainFactory.CreateObjectReference<IChat>(c);

//Subscribe the instance to receive messages.
await friend.Subscribe(obj);
```

--------------------------------

### Install Orleans ZooKeeper Utilities Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using ZooKeeper for storing cluster membership data.

```powershell
Install-Package Microsoft.Orleans.OrleansZooKeeperUtils

```

--------------------------------

### Configure Custom Placement Strategy with ConfigureServices

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

An alternative way to register a custom placement strategy is by using the ConfigureServices method during silo host setup.

```csharp
siloBuilder.ConfigureServices(services =>
    services.AddSingleton<PlacementStrategy, MyPlacementStrategy>());
```

--------------------------------

### Configure Orleans with Code Generation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/code-generation

Installs the Microsoft.Orleans.OrleansCodeGenerator package and uses the ApplicationPartManagerCodeGenExtensions.WithCodeGeneration extension method to enable code generation during initialization.

```csharp
public static void ConfigureWithCodeGeneration(ISiloHostBuilder builder)
{
    builder.ConfigureApplicationParts(
        parts => parts
            .AddApplicationPart(typeof(IRuntimeCodeGenGrain).Assembly)
            .WithCodeGeneration());
}
```

--------------------------------

### Install Orleans Reminders ADO.NET Providers Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using ADO.NET for storing reminders in one of the supported databases.

```powershell
Install-Package Microsoft.Orleans.Reminders.AdoNet

```

--------------------------------

### Add MessagePack Serializer Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Install the necessary NuGet package for MessagePack serialization support in Orleans projects using the .NET CLI.

```bash
dotnet add package Microsoft.Orleans.Serialization.MessagePack
```

--------------------------------

### Add Azure Cosmos DB Clustering NuGet Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Install the necessary NuGet package for Azure Cosmos DB clustering.

```bash
dotnet add package Microsoft.Orleans.Clustering.Cosmos

```

--------------------------------

### Install Orleans Reminders Provider for AWS DynamoDB Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using AWS DynamoDB for storing reminders.

```powershell
Install-Package Microsoft.Orleans.Reminders.DynamoDB

```

--------------------------------

### Build and Connect Orleans Client Programmatically

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/client-configuration

Programmatically builds and connects an Orleans client using ClientBuilder. This example demonstrates configuring cluster options, Azure Storage clustering with a provided connection string, and adding application parts.

```csharp
public static async Task ConfigureClient(string connectionString)
{
    var client = new ClientBuilder()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "my-first-cluster";
            options.ServiceId = "MyOrleansService";
        })
        .UseAzureStorageClustering(
            options => options.ConfigureTableServiceClient(connectionString))
        .ConfigureApplicationParts(
            parts => parts.AddApplicationPart(
                typeof(IValueGrain).Assembly))
        .Build();

    await client.Connect();
}

```

--------------------------------

### Configure ADO.NET Storage Provider via Silo Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/relational-storage

Example of configuring an ADO.NET storage provider using the silo builder, specifying connection string, invariant, and JSON format.

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddAdoNetGrainStorage("OrleansStorage", options =>
    {
        options.Invariant = "<Invariant>";
        options.ConnectionString = "<ConnectionString>";
        options.UseJsonFormat = true;
    });
});

using var host = builder.Build();
await host.RunAsync();

```

--------------------------------

### Configure basic DynamoDB grain storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/dynamodb-storage

Configure the DynamoDB grain persistence provider with basic authentication details. Ensure the Microsoft.Orleans.Persistence.DynamoDB package is installed.

```csharp
siloBuilder.AddDynamoDBGrainStorage(
    name: "profileStore",
    configureOptions: options =>
    {
        options.AccessKey = "<DynamoDB access key>";
        options.SecretKey = "<DynamoDB secret key>";
        options.Service = "<DynamoDB region name>"; // Such as "us-west-2"
    });

```

--------------------------------

### Implement Streaming Grain Method with yield return

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Implement a streaming grain method using 'yield return' to progressively return items. This example fetches and returns 1000 items.

```csharp
public class DataGrain : Grain, IDataGrain
{
    public async IAsyncEnumerable<DataItem> GetAllItemsAsync()
    {
        for (int i = 0; i < 1000; i++)
        {
            // Simulate async data retrieval
            var item = await FetchItemAsync(i);
            yield return item;
        }
    }

    public async IAsyncEnumerable<DataItem> GetItemsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int id = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            var item = await FetchItemAsync(id++);
            yield return item;
        }
    }

    private Task<DataItem> FetchItemAsync(int id) =>
        Task.FromResult(new DataItem { Id = id });
}

[GenerateSerializer]
public class DataItem
{
    [Id(0)]
    public int Id { get; set; }
}
```

--------------------------------

### Reference a grain by GUID in client code

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-identity

Obtain a grain reference using the IGrainFactory by providing a newly generated GUID as the grain's primary key. This is useful for creating new, uniquely identified grain instances.

```csharp
var grain = grainFactory.GetGrain<IExample>(Guid.NewGuid());
```

--------------------------------

### Install Orleans Reminders Azure Table Storage Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using Azure Tables for storing reminders.

```powershell
Install-Package Microsoft.Orleans.Reminders.AzureStorage

```

--------------------------------

### Set Trace ID in Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-context

Example of setting a trace ID in the client's request context using a new System.Guid.

```csharp
RequestContext.Set("TraceId", Guid.NewGuid());
```

--------------------------------

### Enable Build-Time Code Generation with Orleans.Sdk

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/code-generation

Install Microsoft.Orleans.Sdk in projects that contain grains, grain interfaces, custom serializers, or types sent between grains to enable build-time code generation.

```xml
<PackageReference Include="Microsoft.Orleans.Sdk" />
```

--------------------------------

### Configure Azure Storage with Connection Strings and JSON Serialization

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Configure Azure Table and Blob storage providers using connection strings and explicitly enable JSON serialization for state. This example uses `HostBuilder`.

```csharp
var host = new HostBuilder()
    .UseOrleans(siloBuilder =>
    {
        siloBuilder.AddAzureTableGrainStorage(
            name: "profileStore",
            configureOptions: options =>
            {
                // Use JSON for serializing the state in storage
                options.UseJson = true;

                // Configure the storage connection key
                options.ConnectionString =
                    "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1";
            })
            .AddAzureBlobGrainStorage(
                name: "cartStore",
                configureOptions: options =>
                {
                    // Use JSON for serializing the state in storage
                    options.UseJson = true;

                    // Configure the storage connection key
                    options.ConnectionString =
                        "DefaultEndpointsProtocol=https;AccountName=data2;AccountKey=SOMETHING2";
                });
    })
    .Build();


```

--------------------------------

### Implement IGrainBase on a Grain Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Implement IGrainBase to enable access to extension methods for POCO grains. This example shows a basic implementation without lifecycle methods.

```csharp
public sealed class PingGrain : IGrainBase, IPingGrain
{
    public PingGrain(IGrainContext context) => GrainContext = context;

    public IGrainContext GrainContext { get; }

    public ValueTask Ping() => ValueTask.CompletedTask;
}
```

--------------------------------

### Install Orleans Persistence Azure Storage Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugins for using Azure Tables or Azure Blobs for storing grain state.

```powershell
Install-Package Microsoft.Orleans.Persistence.AzureStorage

```

--------------------------------

### Configure Log Consistency Provider Programmatically

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/event-sourcing-configuration

Achieve the same programmatic configuration as the XML example using `SiloBuilder`. This method adds a log storage-based log consistency provider.

```csharp
builder.AddLogStorageBasedLogConsistencyProvider("LogStorage")
```

--------------------------------

### Integrate Orleans Metrics with OpenTelemetry

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Configure OpenTelemetry to collect metrics emitted by Orleans. This example adds Prometheus as an exporter and registers the 'Microsoft.Orleans' meter.

```csharp
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics => metrics
        .AddPrometheusExporter()
        .AddMeter("Microsoft.Orleans"));
```

--------------------------------

### Grain Participation in its Own Lifecycle

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Override the `Participate` method in a grain to subscribe to lifecycle stages. This example shows subscribing to `GrainLifecycleStage.SetupState` to execute `OnSetupState`.

```csharp
public override void Participate(IGrainLifecycle lifecycle)
{
    base.Participate(lifecycle);
    lifecycle.Subscribe(
        this.GetType().FullName,
        GrainLifecycleStage.SetupState,
        OnSetupState);
}
```

--------------------------------

### Example Grain with Request Context

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-context

This grain demonstrates retrieving a trace ID from the request context and using it in its response. It handles cases where the trace ID might not be present.

```csharp
using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

public class HelloGrain(ILogger<HelloGrain> logger) : Grain, IHelloGrain
{
    ValueTask<string> IHelloGrain.SayHello(string greeting)
    {
        _logger.LogInformation("""
            SayHello message received: greeting = "{Greeting}"
            """,
            greeting);

        var traceId = RequestContext.Get("TraceId") as string
            ?? "No trace ID";

        return ValueTask.FromResult($"""
            TraceID: {traceId}
            Client said: "{greeting}", so HelloGrain says: Hello!
            """);
    }
}

public interface IHelloGrain : IGrainWithStringKey
{
    ValueTask<string> SayHello(string greeting);
}
```

--------------------------------

### Get All Subscription Handles in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Retrieves all active subscription handles for a given stream. Useful for managing existing subscriptions.

```csharp
IList<StreamSubscriptionHandle<T>> allMyHandles =
    await IAsyncStream<T>.GetAllSubscriptionHandles();

```

--------------------------------

### V2 Grain Interface with New Method and Unchanged Existing Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/backward-compatibility-guidelines

Example of a V2 grain interface that includes a new method and keeps an existing method's body unchanged.

```csharp
// V2
public interface MyGrain : IMyGrain
{
    // Method inherited from V1
    // Do not change the body
    Task MyMethod(int arg)
    {
        SomeSubRoutine(arg);
    }

    // New method added in V2
    Task MyNewMethod(int arg)
    {
        SomeSubRoutine(arg);
        NewRoutineAdded(arg);
    }
}
```

--------------------------------

### Retrieve IGrainService Types from Service Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

The Orleans silo fetches IGrainService types from the service provider when it starts up.

```csharp
var grainServices = this.Services.GetServices<IGrainService>();

```

--------------------------------

### Install Orleans Clustering Provider for AWS DynamoDB

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using AWS DynamoDB for storing cluster membership data.

```powershell
Install-Package Microsoft.Orleans.Clustering.DynamoDB

```

--------------------------------

### Configure GC in SDK-style projects (.csproj)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/configuring-garbage-collection

Use these settings in your .csproj file for SDK-style projects targeting .NET Core and .NET 5+ to enable server and concurrent garbage collection.

```xml
<PropertyGroup>
    <ServerGarbageCollection>true</ServerGarbageCollection>
    <ConcurrentGarbageCollection>true</ConcurrentGarbageCollection>
</PropertyGroup>
```

--------------------------------

### Configure Redis Reminder Service

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Configure the Redis provider for Orleans reminders. This example sets up basic connection options for a local Redis instance.

```csharp
public static async Task ConfigureRedisAsync(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);
    builder.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseRedisReminderService(options =>
        {
            options.ConfigurationOptions = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                AbortOnConnectFail = false
            };
        });
    });

    using var host = builder.Build();
    await host.RunAsync();
}
```

--------------------------------

### Execute a Transaction with ITransactionClient

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Demonstrates using the ITransactionClient to perform atomic operations. This example withdraws and deposits credits within a single transaction, ensuring data consistency.

```csharp
await transactionClient.RunTransaction(
  TransactionOption.Create,
  () => Task.WhenAll(from.Withdraw(100), to.Deposit(100)));
```

--------------------------------

### Configure In-Memory Streaming with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/stream-providers

Set up in-memory streaming for development and testing in the AppHost project. This avoids external dependencies.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var orleans = builder.AddOrleans("cluster")
    .WithDevelopmentClustering()
    .WithMemoryStreaming("MemoryStreamProvider");

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans);

builder.Build().Run();

```

--------------------------------

### Register Custom Partition Key Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Register the custom partition key provider when configuring a storage provider. This example shows how to register 'MyPartitionKeyProvider' with Cosmos DB storage.

```csharp
// Register with custom partition key provider
siloBuilder.AddCosmosGrainStorage<MyPartitionKeyProvider>(
    name: "cosmos",
    configureOptions: options =>
    {
        options.ConfigureCosmosClient("https://myaccount.documents.azure.com:443/", new DefaultAzureCredential());
    });

```

--------------------------------

### Install Orleans Clustering Provider for ADO.NET Providers

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using ADO.NET for storing cluster membership data in one of the supported databases.

```powershell
Install-Package Microsoft.Orleans.Clustering.AdoNet

```

--------------------------------

### Install Orleans Clustering Provider for Azure Table Storages

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package includes the plugin for using Azure Tables for storing cluster membership data.

```powershell
Install-Package Microsoft.Orleans.Clustering.AzureStorage

```

--------------------------------

### Create ASP.NET Core Web API Project (CLI)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Use the .NET CLI to create a new ASP.NET Core Minimal API project. This command also opens the project folder in Visual Studio Code.

```bash
dotnet new webapi -o OrleansURLShortener
code -r OrleansURLShortener
```

--------------------------------

### Configure Multiple Stream Providers with Azure Storage (Connection String)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Configures memory streams, Azure Queue streams using a connection string, and Azure Table storage for Pub-Sub. This setup is an alternative to using managed identity for Azure services.

```csharp
hostBuilder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddMemoryStreams("StreamProvider")
        .AddAzureQueueStreams("AzureQueueProvider",
            optionsBuilder => optionsBuilder.ConfigureAzureQueue(
                options => options.Configure(
                    opt => opt.QueueServiceClient = new QueueServiceClient(connectionString))))
        .AddAzureTableGrainStorage("PubSubStore",
            options => options.TableServiceClient = new TableServiceClient(connectionString));
});

```

--------------------------------

### Configure In-Memory Reminder Service

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Use this snippet for a development-only in-memory implementation of the reminder system. This is suitable for testing without requiring external storage setup.

```csharp
public static async Task ConfigureInMemoryAsync(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);
    builder.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseInMemoryReminderService();
    });

    using var host = builder.Build();
    await host.RunAsync();
}
```

--------------------------------

### Get Request Context Value

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-context

Use this API to retrieve a value from the current request context. The value can be any serializable type.

```csharp
object Get(string key)
```

--------------------------------

### Run Orleans Shopping Cart App Locally

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

Use the .NET CLI to run the Orleans shopping cart application locally. This command starts the silo project for the application.

```bash
dotnet run --project Silo\Orleans.ShoppingCart.Silo.csproj

```

--------------------------------

### Configure Queue Mapper and Balancer for Persistent Streams

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/streams-implementation

Configures the persistent stream provider with a specific queue mapper and a dynamic cluster deployment balancer. This example sets the total queue count to eight and uses the HashRingStreamQueueMapperOptions.

```csharp
hostBuilder
    .AddPersistentStreams(StreamProviderName, GeneratorAdapterFactory.Create,
        providerConfigurator =>
        providerConfigurator.Configure<HashRingStreamQueueMapperOptions>(
            ob => ob.Configure(options => options.TotalQueueCount = 8))
      .UseDynamicClusterConfigDeploymentBalancer());

```

--------------------------------

### Add File Grain Storage to Orleans Host

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Example of adding the custom file grain storage to an Orleans host using the ISiloBuilder extension method.

```csharp
using GrainStorage;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
siloBuilder.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering()
        .AddFileGrainStorage("File", options =>
        {
            string path = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);

            options.RootDirectory = Path.Combine(path, "Orleans/GrainState/v1");
        });
});

using var host = builder.Build();
await host.RunAsync();
```

--------------------------------

### Configure Streaming Batch Size

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Optimize network round trips by configuring the batch size for streaming data. This example requests up to 50 elements per batch, reducing overhead compared to the default of 100.

```csharp
// Request up to 50 elements per batch instead of the default 100
await foreach (var item in grain.GetAllItemsAsync().WithBatchSize(50))
{
    await ProcessItemAsync(item);
}
```

--------------------------------

### Set Minimum Logging Level in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/troubleshooting-deployments

Configure the minimum logging level for Orleans to capture necessary information during troubleshooting. This example sets the level to Information.

```csharp
builder.Logging.SetMinimumLevel(LogLevel.Information);
```

--------------------------------

### Sign in to Azure

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Logs you into your Azure account using the Azure CLI. This command is necessary before deploying resources to Azure.

```bash
az login

```

--------------------------------

### Define Grain Interface with Integer Compound Key

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-identity

Inherit from `IGrainWithIntegerCompoundKey` to define a grain that uses a compound key starting with a long.

```csharp
public interface IExampleGrain : Orleans.IGrainWithIntegerCompoundKey
{
    Task Hello();
}
```

--------------------------------

### Install Orleans Hosting on Azure Cloud Services Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package contains helper classes for hosting silos and Orleans clients as Azure Cloud Services (Worker Roles and Web Roles).

```powershell
Install-Package Microsoft.Orleans.Hosting.AzureCloudServices

```

--------------------------------

### Configure Resource-Optimized Placement Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

Configure the resource-optimized placement strategy by setting weights for various resource metrics. These weights determine the relative importance of CPU usage, memory usage, available memory, maximum available memory, and activation count when selecting a silo. The LocalSiloPreferenceMargin can also be adjusted to influence the preference for the local silo.

```csharp
siloBuilder.Configure<ResourceOptimizedPlacementOptions>(options =>
{
    options.CpuUsageWeight = 40;
    options.MemoryUsageWeight = 20;
    options.AvailableMemoryWeight = 20;
    options.MaxAvailableMemoryWeight = 5;
    options.ActivationCountWeight = 15;
    options.LocalSiloPreferenceMargin = 5;
});
```

--------------------------------

### Get a Reminder by Name

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Retrieve an IGrainReminder instance using its name with Grain.GetReminder. This is useful if you only have the reminder's name and need to interact with it.

```csharp
protected Task<IGrainReminder> GetReminder(string reminderName)
```

--------------------------------

### Implement BackgroundService for Continuous Operations

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/startup-tasks

Use BackgroundService for tasks that need to run continuously in the background. This example pings a grain every 5 seconds. Ensure proper error handling and cancellation token usage.

```csharp
public class GrainPingService : BackgroundService
{
    private readonly IGrainFactory _grainFactory;
    private readonly ILogger<GrainPingService> _logger;

    public GrainPingService(
        IGrainFactory grainFactory,
        ILogger<GrainPingService> logger)
    {
        _grainFactory = grainFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Pinging grain...");
                    var grain = _grainFactory.GetGrain<IMyGrain>("ping-target");
                    await grain.Ping();
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    // Log the error but continue running
                    _logger.LogError(ex, "Failed to ping grain. Will retry in 5 seconds.");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Ignore cancellation during shutdown.
        }
        finally
        {
            _logger.LogInformation("Grain ping service is shutting down.");
        }
    }
}
```

--------------------------------

### AlwaysInterleave Attribute Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-scheduling

Demonstrates the use of AlwaysInterleaveAttribute on grain interface methods. Methods marked with this attribute always interleave with other requests.

```csharp
public interface ISlowpokeGrain : IGrainWithIntegerKey
{
    Task GoSlow();

    [AlwaysInterleave]
    Task GoFast();
}

public class SlowpokeGrain : Grain, ISlowpokeGrain
{
    public async Task GoSlow()
    {
        await Task.Delay(TimeSpan.FromSeconds(10));
    }

    public async Task GoFast()
    {
        await Task.Delay(TimeSpan.FromSeconds(10));
    }
}

```

--------------------------------

### Implement HelloGrain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/tutorial-1

Implements the IHello interface, providing the logic for the grain's behavior. It logs the received greeting and returns a formatted response. Requires Microsoft.Extensions.Logging.

```csharp
using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

public class HelloGrain : Grain, IHello
{
    private readonly ILogger _logger;

    public HelloGrain(ILogger<HelloGrain> logger) => _logger = logger;

    ValueTask<string> IHello.SayHello(string greeting)
    {
        _logger.LogInformation("""
            SayHello message received: greeting = \"{Greeting}\"",
            greeting);
        
        return ValueTask.FromResult($"""

            Client said: \"{greeting}\", so HelloGrain says: Hello!
            """);
    }
}

```

--------------------------------

### Install Orleans Core Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/nuget-packages

This package contains implementations for most Orleans public types used by application code and Orleans clients. Reference it for building libraries and client applications that use Orleans types but don't deal with hosting or silos.

```powershell
Install-Package Microsoft.Orleans.Core

```

--------------------------------

### Configure Activation Rebalancer Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

Configure the activation rebalancer's behavior by using `Configure<ActivationRebalancerOptions>` after enabling it. This allows customization of parameters like initial delay, cycle periods, and stagnation limits. Suppress experimental warnings as shown.

```csharp
#pragma warning disable ORLEANSEXP002
siloBuilder.AddActivationRebalancer();
siloBuilder.Configure<ActivationRebalancerOptions>(options =>
{
    options.RebalancerDueTime = TimeSpan.FromSeconds(60);
    options.SessionCyclePeriod = TimeSpan.FromSeconds(15);
    options.MaxStagnantCycles = 3;
});
#pragma warning restore ORLEANSEXP002

```

--------------------------------

### Use MessagePack Type in Grain Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Example of using a MessagePack-serialized type (OrderMessage) within a grain interface for defining message contracts.

```csharp
public interface IOrderGrain : IGrainWithStringKey
{
    Task<OrderMessage> GetOrder();
    Task PlaceOrder(OrderMessage order);
}
```

--------------------------------

### Deploy Application with Aspire CLI and Parameters

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Deploys your .NET Aspire application to Azure Container Apps, specifying deployment parameters like location and environment inline.

```bash
aspire deploy --deployment-param location=eastus --deployment-param environment=production

```

--------------------------------

### Start Orleans Client with Retries

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/overview-helloworld

Builds and connects an Orleans client with a retry mechanism for establishing a connection to the cluster. Includes a custom retry filter to handle specific exceptions and delays.

```csharp
static async Task<IClusterClient> StartClientWithRetries()
{
    _attempt = 0;
    var client = new ClientBuilder()
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "HelloWorldApp";
        })
        .ConfigureLogging(logging => logging.AddConsole())
        .Build();

    await client.Connect(RetryFilter);
    Console.WriteLine("Client successfully connected to silo host");
    return client;
}

private static async Task<bool> RetryFilter(Exception exception)
{
    if (exception.GetType() != typeof(SiloUnavailableException))
    {
        Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error. Exception: {exception}");
        return false;
    }
    _attempt++;
    Console.WriteLine($"Cluster client attempt {_attempt} of 5 failed to connect to cluster. Exception: {exception}");
    if (_attempt > 5)
    {
        return false;
    }
    await Task.Delay(TimeSpan.FromSeconds(4));
    return true;
}

```

--------------------------------

### Implement Per-Grain Call Filter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Implement a per-grain call filter by making a grain class implement IIncomingGrainCallFilter. This example modifies the result of a specific method.

```csharp
public class MyFilteredGrain
    : Grain, IMyFilteredGrain, Orleans.IIncomingGrainCallFilter
{
    public async Task Invoke(Orleans.IIncomingGrainCallContext context)
    {
        await context.Invoke();

        // Change the result of the call from 7 to 38.
        if (string.Equals(
            context.InterfaceMethod.Name,
            nameof(this.GetFavoriteNumber)))
        {
            context.Result = 38;
        }
    }

    public Task<int> GetFavoriteNumber() => Task.FromResult(7);
}

```

--------------------------------

### Reentrant Grain Method Interleaving Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-scheduling

Illustrates how reentrant grains can interleave execution turns from different requests. This pseudo-code shows a possible interleaving order.

```csharp
Task Foo()
{
    await task1;    // line 1
    return Do2();   // line 2
}

Task Bar()
{
    await task2;   // line 3
    return Do2();  // line 4
}

```

--------------------------------

### AppHost Project: Production Configuration with Redis

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configures an Orleans cluster using Redis for clustering, grain storage, and reminders. Use `WithReplicas(n)` for high availability and `orleans.AsClient()` for clients that only need to call grains.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var orleans = builder.AddOrleans("cluster")
    .WithClustering(redis)
    .WithGrainStorage("Default", redis)
    .WithReminders(redis);

// Add Orleans silo with 3 replicas for production
builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans)
    .WithReference(redis)
    .WithReplicas(3);

// Add a separate client project (e.g., an API)
builder.AddProject<Projects.MyApi>("api")
    .WithReference(orleans.AsClient())
    .WithReference(redis);

builder.Build().Run();

```

--------------------------------

### Configure Silo with SiloHostBuilder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/overview-helloworld

Configures the Orleans silo using SiloHostBuilder. This method allows for explicit configuration of application parts and is useful for more advanced setups.

```csharp
static async Task<ISiloHost> StartSilo(string[] args)
{
    var siloHostBuilder = new SiloHostBuilder()
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "HelloWorldApp";
        })
        .Configure<EndpointOptions>(
            options => options.AdvertisedIPAddress = IPAddress.Loopback)
        .ConfigureApplicationParts(
            parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
        .ConfigureLogging(logging => logging.AddConsole());

    var host = siloHostBuilder.Build();
    await host.StartAsync();

    return host;
}
```

--------------------------------

### Implement Custom Exception Serialization

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Example of a custom exception type that correctly implements the ISerializable pattern for serialization using the fallback serializer, such as BinaryFormatter.

```csharp
[Serializable]
public class MyCustomException : Exception
{
    public string MyProperty { get; }

    public MyCustomException(string myProperty, string message)
        : base(message)
    {
        MyProperty = myProperty;
    }

    public MyCustomException(string transactionId, string message, Exception innerException)
        : base(message, innerException)
    {
        MyProperty = transactionId;
    }

    // Note: This is the constructor called by BinaryFormatter during deserialization
    public MyCustomException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        MyProperty = info.GetString(nameof(MyProperty));
    }

    // Note: This method is called by BinaryFormatter during serialization
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(MyProperty), MyProperty);
    }
}

```

--------------------------------

### Example Azure Service Principal Credentials

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

This JSON structure represents the credentials generated after creating a service principal. These values are used for authenticating with Azure resources.

```json
{
  "clientId": "<your client id>",
  "clientSecret": "<your client secret>",
  "subscriptionId": "<your subscription id>",
  "tenantId": "<your tenant id>",
  "activeDirectoryEndpointUrl": "https://login.microsoftonline.com/",
  "resourceManagerEndpointUrl": "https://brazilus.management.azure.com",
  "activeDirectoryGraphResourceId": "https://graph.windows.net/",
  "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
  "galleryEndpointUrl": "https://gallery.azure.com",
  "managementEndpointUrl": "https://management.core.windows.net"
}
```

--------------------------------

### Mock GrainFactory and IJournalGrain in a Unit Test

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

This example demonstrates how to use Moq to mock a WorkerGrain and its GrainFactory. The mocked GrainFactory is configured to return a mocked IJournalGrain, allowing verification of interactions.

```csharp
using Xunit;
using Moq;

namespace Tests;

public class WorkerGrainTests
{
    [Fact]
    public async Task RecordsMessageInJournal()
    {
        var data = "Hello, World";
        var journal = new Mock<IJournalGrain>();
        var worker = new Mock<WorkerGrain>();
        worker
            .Setup(x => x.GrainFactory.GetGrain<IJournalGrain>(It.IsAny<Guid>()))
            .Returns(journal.Object);

        await worker.DoWork(data)

        journal.Verify(x => x.Record(data), Times.Once());
    }
}
```

--------------------------------

### Basic Orleans Client Configuration with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configure an Orleans client project using Aspire's service defaults and keyed Redis client. Aspire automatically injects clustering configuration for the client.

```csharp
public static void BasicClientConfiguration(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.AddServiceDefaults();
    builder.AddKeyedRedisClient("orleans-redis");

    // Configure Orleans client - Aspire injects clustering configuration automatically
    builder.UseOrleansClient();

    builder.Build().Run();
}

```

--------------------------------

### Registering a Grain Timer in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Use the RegisterGrainTimer method to start a timer. The callback function is invoked periodically. Dispose of the returned IGrainTimer to cancel the timer.

```csharp
protected IGrainTimer RegisterGrainTimer<TState>(
    Func<TState, CancellationToken, Task> callback, // function invoked when the timer ticks
    TState state,                                   // object to pass to callback
    GrainTimerCreationOptions options)              // timer creation options
```

--------------------------------

### Add Azure Storage Clustering and Persistence Packages

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Install the Microsoft.Orleans.Clustering.AzureStorage and Microsoft.Orleans.Persistence.AzureStorage NuGet packages. These packages provide the necessary components for Orleans to use Azure Storage for clustering and persistence.

```csharp
dotnet add package Microsoft.Orleans.Clustering.AzureStorage --version 8.*
dotnet add package Microsoft.Orleans.Persistence.AzureStorage --version 8.*

```

--------------------------------

### Configure Azure Table Storage Reminders in AppHost with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Configure Azure Table Storage for Orleans clustering and reminders in your AppHost. This example uses the Azurite emulator for local development.

```csharp
public static void RemindersAzureTableAppHost(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    var storage = builder.AddAzureStorage("storage")
        .RunAsEmulator();

    var reminders = storage.AddTables("reminders");

    var orleans = builder.AddOrleans("cluster")
        .WithClustering(reminders)
        .WithReminders(reminders);

    builder.AddProject<Projects.Silo>("silo")
        .WithReference(orleans)
        .WaitFor(storage);

    builder.Build().Run();
}

```

--------------------------------

### Enable Build-Time Code Generation with CodeGenerator.MSBuild

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/code-generation

Install Microsoft.Orleans.CodeGenerator.MSBuild into C# projects for a newer code generation approach that leverages Roslyn for both analysis and generation, avoiding issues with application binaries and improving incremental build support.

```xml
<PackageReference Include="Microsoft.Orleans.CodeGenerator.MSBuild" />
```

--------------------------------

### Implement Logging Grain Call Filter Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Implement a grain call filter by creating a class that inherits from IIncomingGrainCallFilter. This example logs method calls and exceptions.

```csharp
public class LoggingCallFilter : IIncomingGrainCallFilter
{
    private readonly ILogger<LoggingCallFilter> _logger;

    public LoggingCallFilter(ILogger<LoggingCallFilter> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(IIncomingGrainCallContext context)
    {
        try
        {
            await context.Invoke();

            _logger.LogInformation(
                "{GrainType}.{MethodName} returned value {Result}",
                context.Grain.GetType(),
                context.MethodName,
                context.Result);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "{GrainType}.{MethodName} threw an exception",
                context.Grain.GetType(),
                context.MethodName);

            // If this exception is not re-thrown, it is considered to be
            // handled by this filter.
            throw;
        }
    }
}

```

--------------------------------

### Subscribe to Silo Lifecycle for Storage Initialization

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Register an initialization function on the `ApplicationServices` lifecycle stage to set up custom storage.

```csharp
public void Participate(ISiloLifecycle lifecycle)
{
    lifecycle.Subscribe(
        OptionFormattingUtilities.Name<FileGrainStorageImpl>(_storageName),
        ServiceLifecycleStage.ApplicationServices,
        Init);
}
```

--------------------------------

### Configure OTLP Exporter Endpoint

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

This snippet shows how to configure the OTLP exporter endpoint, for example, to `http://localhost:4317`. The default configuration uses gRPC on port 4317.

```csharp
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:4317");
            })
            .AddMeter("Microsoft.Orleans");
    });

```

--------------------------------

### Main Bicep Deployment File

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

This Bicep file defines the main entry point for deploying Azure resources. It includes module definitions for storage, logging, and the App Service, as well as the virtual network configuration.

```bicep
param appName string
param location string = resourceGroup().location

module storageModule 'storage.bicep' = {
  name: 'orleansStorageModule'
  params: {
    name: '${appName}storage'
    location: location
  }
}

module logsModule 'logs-and-insights.bicep' = {
  name: 'orleansLogModule'
  params: {
    operationalInsightsName: '${appName}-logs'
    appInsightsName: '${appName}-insights'
    location: location
  }
}

resource vnet 'Microsoft.Network/virtualNetworks@2021-05-01' = {
  name: '${appName}-vnet'
  location: location
  properties: {
    addressSpace: {
      addressPrefixes: [
        '172.17.0.0/16',
        '192.168.0.0/16'
      ]
    }
    subnets: [
      {
        name: 'default'
        properties: {
          addressPrefix: '172.17.0.0/24'
          delegations: [
            {
              name: 'delegation'
              properties: {
                serviceName: 'Microsoft.Web/serverFarms'
              }
            }
          ]
        }
      }
      {
        name: 'staging'
        properties: {
          addressPrefix: '192.168.0.0/24'
          delegations: [
            {
              name: 'delegation'
              properties: {
                serviceName: 'Microsoft.Web/serverFarms'
              }
            }
          ]
        }
      }
    ]
  }
}

module siloModule 'app-service.bicep' = {
  name: 'orleansSiloModule'
  params: {
    appName: appName
    location: location
    vnetSubnetId: vnet.properties.subnets[0].id
    stagingSubnetId: vnet.properties.subnets[1].id
    appInsightsConnectionString: logsModule.outputs.appInsightsConnectionString
    appInsightsInstrumentationKey: logsModule.outputs.appInsightsInstrumentationKey
    storageConnectionString: storageModule.outputs.connectionString
  }
}

```

--------------------------------

### Define a Stateless Worker Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/stateless-worker-grains

Apply the StatelessWorkerAttribute to a grain class to designate it as a stateless worker. This example uses the default maximum activation limit.

```csharp
[StatelessWorker]
public class MyStatelessWorkerGrain : Grain, IMyStatelessWorkerGrain
{
    // ...
}
```

--------------------------------

### RequiredMatchSiloMetadataPlacementFilter Example

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Use RequiredMatchSiloMetadataPlacementFilter to ensure a grain is only placed on silos that match specific metadata keys with the calling silo. If no matches are found, placement will fail.

```csharp
[RequiredMatchSiloMetadataPlacementFilter(new[] { "zone" })]
public class ZoneRestrictedGrain : Grain, IZoneRestrictedGrain
{
    // This grain will only activate on silos in the same zone as the caller
}
```

```csharp
[RequiredMatchSiloMetadataPlacementFilter(new[] { "zone", "tier" })]
public class ZoneAndTierGrain : Grain, IZoneAndTierGrain
{
    // Requires both zone AND tier to match the calling silo's values
}
```

--------------------------------

### Register an IStartupTask Implementation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/startup-tasks

Implement the IStartupTask interface for more complex startup logic. The implementation is then registered using the AddStartupTask extension method.

```csharp
public class CallGrainStartupTask : IStartupTask
{
    private readonly IGrainFactory _grainFactory;

    public CallGrainStartupTask(IGrainFactory grainFactory) =>
        _grainFactory = grainFactory;

    public async Task Execute(CancellationToken cancellationToken)
    {
        var grain = _grainFactory.GetGrain<IMyGrain>("startup-task-grain");
        await grain.Initialize();
    }
}


```

```csharp
siloBuilder.AddStartupTask<CallGrainStartupTask>();


```

--------------------------------

### Subscribe to Stream in OnActivateAsync (Implicit)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

This method is called when the grain activates. It retrieves the stream provider, gets a stream, and subscribes to it using the OnNextAsync method for implicit subscriptions.

```csharp
public override async Task OnActivateAsync()
{
    var streamProvider = this.GetStreamProvider(PROVIDER_NAME);
    var stream = 
        streamProvider.GetStream<string>(
            this.GetPrimaryKey(), "MyStreamNamespace");

    await stream.SubscribeAsync(OnNextAsync);
}
```

--------------------------------

### Define Grain Interface for Stock Data

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/broadcast-channel

Defines the interface for a grain that retrieves stock data. Grains implementing this interface are identified by a GUID key.

```csharp
namespace BroadcastChannel.GrainInterfaces;

public interface ILiveStockGrain : IGrainWithGuidKey
{
    ValueTask<Stock> GetStock(StockSymbol symbol);
}

```

--------------------------------

### Define IGrainStateAccessor Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-extensions

Define a generic interface for state access, deriving from IGrainExtension. This interface includes methods for getting and setting the grain's state.

```csharp
public interface IGrainStateAccessor<T> : IGrainExtension
{
    Task<T> GetState();
    Task SetState(T state);
}
```

--------------------------------

### Consul Warning on Windows

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/consul-deployment

When Consul starts on Windows, it logs a warning indicating that Windows is not recommended for Consul servers in production environments. This is due to testing focus, not known issues.

```output
==> WARNING: Windows is not recommended as a Consul server. Do not use in production.


```

--------------------------------

### Default Grain Implementation by Naming Convention

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

Orleans selects a grain implementation by stripping a leading 'I' from the interface name. This example shows how `CounterGrain` is chosen for `ICounterGrain`.

```C#
/// This will refer to an instance of CounterGrain, since that matches the convention.
ICounterGrain myUpCounter = grainFactory.GetGrain<ICounterGrain>("my-counter");

```

--------------------------------

### Participate Method for Lifecycle Subscription

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Subscribes the storage provider to the silo lifecycle to ensure the root directory is created on startup.

```csharp
public void Participate(ISiloLifecycle lifecycle) =>
    lifecycle.Subscribe(
        observerName: OptionFormattingUtilities.Name<FileGrainStorage>(_storageName),
        stage: ServiceLifecycleStage.ApplicationServices,
        onStart: (ct) =>
        {
            Directory.CreateDirectory(_options.RootDirectory);
            return Task.CompletedTask;
        });
```

--------------------------------

### Configure Azure Table Grain Storage Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-storage

Configures the Azure Table Storage grain persistence provider with a connection string. Ensure the Microsoft.Orleans.Persistence.AzureStorage NuGet package is installed.

```csharp
siloBuilder.AddAzureTableGrainStorage(
    name: "profileStore",
    configureOptions: options =>
    {
        options.ConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1";
    });


```

--------------------------------

### Add Cosmos DB Clustering and Persistence Packages

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Install the Microsoft.Orleans.Clustering.Cosmos and Microsoft.Orleans.Persistence.Cosmos NuGet packages. These packages enable Orleans to use Azure Cosmos DB for clustering and persistence.

```csharp
dotnet add package Microsoft.Orleans.Clustering.Cosmos --version 8.*
dotnet add package Microsoft.Orleans.Persistence.Cosmos --version 8.*

```

--------------------------------

### Add OTLP Exporter for Metrics

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

Install the OpenTelemetry.Exporter.OpenTelemetryProtocol NuGet package and use this extension method to add the OTLP exporter to your service collection. This configures Orleans to use the 'Microsoft.Orleans' meter.

```csharp
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics
            .AddOtlpExporter()
            .AddMeter("Microsoft.Orleans");
    });

```

--------------------------------

### Configure Azure Storage with Connection Strings

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Configure Azure Table and Blob storage providers using connection strings. Avoid this method in production; use managed identity instead.

```csharp
var builder = Host.CreateApplicationBuilder();
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddAzureTableGrainStorage(
        name: "profileStore",
        configureOptions: options =>
        {
            options.TableServiceClient = new TableServiceClient(
                "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1");
        })
        .AddAzureBlobGrainStorage(
            name: "cartStore",
            configureOptions: options =>
            {
                options.BlobServiceClient = new BlobServiceClient(
                    "DefaultEndpointsProtocol=https;AccountName=data2;AccountKey=SOMETHING2");
            });
});

using var host = builder.Build();


```

--------------------------------

### Register Outgoing Call Filter with a Delegate

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Register a lambda expression as an outgoing call filter. This example intercepts calls to 'MyInterceptedMethod' and modifies integer results.

```csharp
builder.AddOutgoingGrainCallFilter(async context =>
{
    // If the method being called is 'MyInterceptedMethod', then set a value
    // on the RequestContext which can then be read by other filters or the grain.
    if (string.Equals(
        context.InterfaceMethod.Name,
        nameof(IMyGrain.MyInterceptedMethod)))
    {
        RequestContext.Set(
            "intercepted value", "this value was added by the filter");
    }

    await context.Invoke();

    // If the grain method returned an int, set the result to double that value.
    if (context.Result is int resultValue)
    {
        context.Result = resultValue * 2;
    }
});
```

--------------------------------

### Configure OpenTelemetry Tracing with OTLP Exporter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

Enables distributed tracing for your Orleans application using OpenTelemetry and the OTLP exporter. Ensure the OpenTelemetry.Exporter.OpenTelemetryProtocol NuGet package is installed. This configuration sets a service name and adds Orleans runtime and application sources.

```csharp
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        // Set a service name
        tracing.SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName: "GPSTracker", serviceVersion: "1.0"));

        tracing.AddSource("Microsoft.Orleans.Runtime");
        tracing.AddSource("Microsoft.Orleans.Application");

        tracing.AddOtlpExporter(otlp =>
        {
            otlp.Endpoint = new Uri("http://localhost:4317");
        });
    });

```

--------------------------------

### Deploy Application with Aspire CLI and Parameters File

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Deploys your .NET Aspire application to Azure Container Apps using a JSON file to specify deployment parameters. This is useful for complex configurations.

```bash
aspire deploy --deployment-params-file deployment-params.json

```

--------------------------------

### Enable Distributed Tracing with OpenTelemetry

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Configure OpenTelemetry for distributed tracing in Orleans. This setup includes adding ASP.NET Core instrumentation, specifying Orleans activity sources, and configuring a Zipkin exporter.

```csharp
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService(serviceName: "ExampleService", serviceVersion: "1.0"));

        tracing.AddAspNetCoreInstrumentation();
        tracing.AddSource("Microsoft.Orleans.Runtime");
        tracing.AddSource("Microsoft.Orleans.Application");

        tracing.AddZipkinExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
        });
    });
```

--------------------------------

### Example dotnet counters monitor output

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

This is a typical output from the `dotnet counters monitor` command when monitoring an Orleans application. It displays various metrics categorized by Orleans components, such as networking, messaging, and gateway.

```text
Press p to pause, r to resume, q to quit.
    Status: Running
[Microsoft.Orleans]
    orleans-app-requests-latency-bucket (Count / 1 sec)                    0
        duration=10000ms                                                   0
        duration=1000ms                                                    0
        duration=100ms                                                     0
        duration=10ms                                                      0
        duration=15000ms                                                   0
        duration=1500ms                                                    0
        duration=1ms                                                   2,530
        duration=2000ms                                                    0
        duration=200ms                                                     0
        duration=2ms                                                       0
        duration=400ms                                                     0
        duration=4ms                                                       0
        duration=5000ms                                                    0
        duration=50ms                                                      0
        duration=6ms                                                       0
        duration=800ms                                                     0
        duration=8ms                                                       0
        duration=9223372036854775807ms                                     0
    orleans-app-requests-latency-count (Count / 1 sec)                 2,530
    orleans-app-requests-latency-sum (Count / 1 sec)                       0
    orleans-catalog-activation-working-set                                36
    orleans-catalog-activations                                           38
    orleans-consistent-ring-range-percentage-average                     100
    orleans-consistent-ring-range-percentage-local                       100
    orleans-consistent-ring-size                                           1
    orleans-directory-cache-size                                          27
    orleans-directory-partition-size                                      26
    orleans-directory-ring-local-portion-average-percentage              100
    orleans-directory-ring-local-portion-distance                          0
    orleans-directory-ring-local-portion-percentage                        0
    orleans-directory-ring-size                                        1,295
    orleans-gateway-received (Count / 1 sec)                           1,291
    orleans-gateway-sent (Count / 1 sec)                               2,582
    orleans-messaging-processing-activation-data                           0
    orleans-messaging-processing-dispatcher-forwarded (Count / 1           0
    orleans-messaging-processing-dispatcher-processed (Count / 1       2,543
        Direction=Request,Status=Ok                                    2,582
    orleans-messaging-processing-dispatcher-received (Count / 1        1,271
        Context=Grain,Direction=Request                                1,291
        Context=None,Direction=Request                                 1,291
    orleans-messaging-processing-ima-enqueued (Count / 1 sec)          5,113

```

--------------------------------

### Configure Azure Blob Grain Storage Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-storage

Configures the Azure Blob Storage grain persistence provider with a connection string. Ensure the Microsoft.Orleans.Persistence.AzureStorage NuGet package is installed.

```csharp
siloBuilder.AddAzureBlobGrainStorage(
    name: "profileStore",
    configureOptions: options =>
    {
        options.ConnectionString =
             "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1";
    });


```

--------------------------------

### Implement a Grain Service

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

Implement the grain service by inheriting from GrainService and the defined interface. You can inject IGrainFactory to make grain calls from within the service. Ensure to override Init, Start, and Stop methods.

```csharp
[Reentrant]
public class DataService : GrainService, IDataService
{
    readonly IGrainFactory _grainFactory;

    public DataService(
        IServiceProvider services,
        GrainId id,
        Silo silo,
        ILoggerFactory loggerFactory,
        IGrainFactory grainFactory)
        : base(id, silo, loggerFactory)
    {
        _grainFactory = grainFactory;
    }

    public override Task Init(IServiceProvider serviceProvider) =>
        base.Init(serviceProvider);

    public override Task Start() => base.Start();

    public override Task Stop() => base.Stop();

    public Task MyMethod()
    {
        // TODO: custom logic here.
        return Task.CompletedTask;
    }
}
```

```csharp
[Reentrant]
public class DataService : GrainService, IDataService
{
    readonly IGrainFactory _grainFactory;

    public DataService(
        IServiceProvider services,
        IGrainIdentity id,
        Silo silo,
        ILoggerFactory loggerFactory,
        IGrainFactory grainFactory)
        : base(id, silo, loggerFactory)
    {
        _grainFactory = grainFactory;
    }

    public override Task Init(IServiceProvider serviceProvider) =>
        base.Init(serviceProvider);

    public override Task Start() => base.Start();

    public override Task Stop() => base.Stop();

    public Task MyMethod()
    {
        // TODO: custom logic here.
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Define and implement a grain with delayed execution in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/scheduler

This example defines an interface and a grain implementation that includes a method with a delayed execution. Use this pattern to handle operations that require waiting before proceeding.

```csharp
public interface IMyGrain : IGrain
{
    Task DelayExecution();
}

public class MyGrain : Grain, IMyGrain
{
    private readonly ILogger<MyGrain> _logger;

    public MyGrain(ILogger<MyGrain> logger) => _logger = logger;

    public async Task DelayExecution()
    {
        _logger.LogInformation("Executing first task");

        await Task.Delay(1_000);

        _logger.LogInformation("Executing second task");
    }
}
```

--------------------------------

### Sample Custom Placement Strategy Implementation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

This C# code defines a custom placement strategy that implements the IPlacementDirector interface. It includes a method to determine the target silo for a new grain activation.

```csharp
public class SamplePlacementStrategyFixedSiloDirector : IPlacementDirector
{
    public Task<SiloAddress> OnAddActivation(
        PlacementStrategy strategy,
        PlacementTarget target,
        IPlacementContext context)
    {
        var silos = context.GetCompatibleSilos(target).OrderBy(s => s).ToArray();
        int silo = GetSiloNumber(target.GrainIdentity.PrimaryKey, silos.Length);

        return Task.FromResult(silos[silo]);
    }
}
```

--------------------------------

### Configure Simple Endpoints

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Configure the default ports for silo-to-silo communication (11111) and client-to-silo gateway (30000) using a helper method. This method automatically detects the network interface to listen on.

```csharp
public static void ConfigureSimpleEndpoints(ISiloHostBuilder siloBuilder)
{
    siloBuilder.ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000);
}
```

--------------------------------

### Orleans 7.x Timer Registration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Example of registering a timer using the `RegisterTimer` method in Orleans 7.x. The callback `DoWorkAsync` receives an untyped object state and interleaves by default.

```csharp
public class MyGrain : Grain, IMyGrain
{
    private IDisposable? _timer;

    public override Task OnActivateAsync()
    {
        _timer = RegisterTimer(
            DoWorkAsync,
            null,
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(10));

        return base.OnActivateAsync();
    }

    private Task DoWorkAsync(object state)
    {
        // Timer work - this interleaves with other calls
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Get Unconfirmed Events in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/immediate-vs-delayed-confirmation

Access the collection of events that have been raised but not yet confirmed. This property is available on grains using the JournaledGrain API.

```csharp
IEnumerable<EventType> UnconfirmedEvents { get; }
```

--------------------------------

### Implement Custom Orleans Serializer

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-customization

This example shows a basic implementation of a custom Orleans serializer by inheriting from IGeneralizedCodec, IGeneralizedCopier, and ITypeFilter. Each method throws NotImplementedException, indicating that actual serialization logic needs to be added.

```csharp
internal sealed class CustomOrleansSerializer :
    IGeneralizedCodec, IGeneralizedCopier, ITypeFilter
{
    void IFieldCodec.WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        object value) =>
        throw new NotImplementedException();

    object IFieldCodec.ReadValue<TInput>(
        ref Reader<TInput> reader, Field field) =>
        throw new NotImplementedException();

    bool IGeneralizedCodec.IsSupportedType(Type type) =>
        throw new NotImplementedException();

    object IDeepCopier.DeepCopy(object input, CopyContext context) =>
        throw new NotImplementedException();

    bool IGeneralizedCopier.IsSupportedType(Type type) =>
        throw new NotImplementedException();
}
```

--------------------------------

### Configure Simple Message Streams and Azure Queue Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Sets up Simple Message Streams, Azure Queue streams with a connection string, and Azure Table storage for Pub-Sub. This configuration uses a specific adapter for Azure Queue streams.

```csharp
hostBuilder.AddSimpleMessageStreamProvider("SMSProvider")
    .AddAzureQueueStreams<AzureQueueDataAdapterV2>("AzureQueueProvider",
        optionsBuilder => optionsBuilder.Configure(
            options => options.ConnectionString = "<Secret>"))
    .AddAzureTableGrainStorage("PubSubStore",
        options => options.ConnectionString = "<Secret>");

```

--------------------------------

### Configure ADO.NET for Client (Orleans 10.0+)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/configuring-ado-dot-net-providers

Configure ADO.NET for clustering on the client side. Requires `Microsoft.Data.SqlClient`.

```csharp
var builder = Host.CreateApplicationBuilder(args);

var invariant = "Microsoft.Data.SqlClient";
var connectionString = "Data Source=(localdb)\MSSQLLocalDB;" +
    "Initial Catalog=Orleans;Integrated Security=True;" +
    "Pooling=False;Max Pool Size=200;" +
    "MultipleActiveResultSets=True";

builder.UseOrleansClient(clientBuilder =>
{
    // Use ADO.NET for clustering
    clientBuilder.UseAdoNetClustering(options =>
    {
        options.Invariant = invariant;
        options.ConnectionString = connectionString;
    });
});

```

--------------------------------

### Implement a Grain Service Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

Implement the grain service client by inheriting from GrainServiceClient. This client acts as a proxy to the grain service. Use GetGrainService to obtain a reference to the target grain service.

```csharp
public class DataServiceClient : GrainServiceClient<IDataService>, IDataServiceClient
{
    public DataServiceClient(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    // For convenience when implementing methods, you can define a property which gets the IDataService
    // corresponding to the grain which is calling the DataServiceClient.
    private IDataService GrainService => GetGrainService(CurrentGrainReference.GrainId);

    public Task MyMethod() => GrainService.MyMethod();
}
```

--------------------------------

### Configure Orleans Silos with Localhost Clustering and Memory Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Configure the application host to use Orleans with localhost clustering and in-memory grain storage. This setup is suitable for development environments.

```csharp
using Orleans.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans(static siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorage("urls");
});

using var app = builder.Build();

```

--------------------------------

### Configure Broadcast Channel with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/stream-providers

Set up a broadcast channel for pub/sub messaging in the AppHost project. This enables broadcasting messages to all subscribers.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var orleans = builder.AddOrleans("cluster")
    .WithDevelopmentClustering()
    .WithBroadcastChannel("BroadcastChannel");

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans);

builder.Build().Run();

```

--------------------------------

### Define a custom placement filter attribute

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Create a marker attribute derived from PlacementFilterAttribute to apply the custom filter to grain classes. This example uses a specific strategy for ordering.

```csharp
[AttributeUsage(
    AttributeTargets.Class, AllowMultiple = false)]
public class ExamplePreferLocalPlacementFilterAttribute(int order)
    : PlacementFilterAttribute(
        new ExamplePreferLocalPlacementFilterStrategy(order)));

```

--------------------------------

### Apply the custom placement filter to a grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Apply the custom placement filter attribute to a grain class to enforce the filtering logic. This example also specifies ActivationCountBasedPlacement as the underlying placement strategy.

```csharp
[ExamplePreferLocalPlacementFilter]
[ActivationCountBasedPlacement]
public class MyGrain() : Grain, IMyGrain
{
    // ...
}

```

--------------------------------

### Advanced TLS Configuration for Orleans Silos

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/transport-layer-security

This example demonstrates advanced TLS configuration for production Orleans silos. It includes custom certificate selection using `LocalServerCertificateSelector`, custom remote certificate validation via `RemoteCertificateValidation`, and enables certificate revocation checking.

```csharp
using IHost host = Host.CreateDefaultBuilder()
    .UseOrleans(builder =>
    {
        builder
            .UseLocalhostClustering()
            .UseTls(StoreName.My, "my-certificate-subject", allowInvalid: false, StoreLocation.LocalMachine, options =>
            {
                options.LocalServerCertificateSelector = (connection, serverName) =>
                {
                    using var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                    store.Open(OpenFlags.ReadOnly);

                    var certificates = store.Certificates.Find(
                        X509FindType.FindBySubjectName,
                        serverName ?? "my-certificate-subject",
                        validOnly: true);

                    return certificates.Count > 0 ? certificates[0] : null;
                };

                options.RemoteCertificateValidation = (certificate, chain, sslPolicyErrors) =>
                    sslPolicyErrors == SslPolicyErrors.None;

                options.OnAuthenticateAsClient = (connection, sslOptions) =>
                {
                    sslOptions.TargetHost = "my-certificate-subject";
                };

                options.CheckCertificateRevocation = true;
            });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.RunAsync();

```

--------------------------------

### Deploy Kubernetes manifests using kubectl

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/kubernetes

Apply the generated Kubernetes manifests to your cluster using kubectl.

```bash
# Using kubectl
kubectl apply -f ./k8s-manifests

```

--------------------------------

### Configure Azure Blob Storage in AppHost

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-storage

Sets up Azure Storage and Blob container for Orleans grain state in the AppHost project.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage");
var blobs = storage.AddBlobs("grainstate");

var orleans = builder.AddOrleans("cluster")
    .WithClustering(builder.AddRedis("redis"))
    .WithGrainStorage("Default", blobs);

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans)
    .WithReference(blobs);

builder.Build().Run();

```

--------------------------------

### Get Grain Reference from Orleans Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

Obtain a grain reference for IPlayerGrain from Orleans client code using client.GetGrain. The player ID is typically read from an external source.

```csharp
IPlayerGrain player = client.GetGrain<IPlayerGrain>(playerId);
```

--------------------------------

### Apply LogConsistencyProvider and StorageProvider Attributes

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/event-sourcing-configuration

Apply the `StorageProviderAttribute` for state storage and `LogConsistencyProviderAttribute` for event sourcing storage to a journaled grain class. Ensure provider names match cluster configuration.

```csharp
[StorageProvider(ProviderName = "OrleansLocalStorage")]
[LogConsistencyProvider(ProviderName = "LogStorage")]
public class EventSourcedBankAccountGrain :
    JournaledGrain<BankAccountState>, IEventSourcedBankAccountGrain
{
    //...
}
```

--------------------------------

### Implement Custom Client Connection Retry Filter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Implement IClientConnectionRetryFilter to customize retry logic for client connection attempts. This example retries up to 5 times with increasing delays when a SiloUnavailableException occurs.

```csharp
using Orleans.Runtime;

internal sealed class ClientConnectRetryFilter : IClientConnectionRetryFilter
{
    private int _retryCount = 0;
    private const int MaxRetry = 5;
    private const int Delay = 1_500;

    public async Task<bool> ShouldRetryConnectionAttempt(
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (_retryCount >= MaxRetry)
        {
            return false;
        }

        if (!cancellationToken.IsCancellationRequested &&
            exception is SiloUnavailableException siloUnavailableException)
        {
            await Task.Delay(++ _retryCount * Delay, cancellationToken);
            return true;
        }

        return false;
    }
}

```

--------------------------------

### Configure InProcessTestCluster

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

Shows how to configure silos, clients, and shared services using `InProcessTestClusterBuilder`. This includes adding memory grain storage and registering custom services.

```csharp
var builder = new InProcessTestClusterBuilder(initialSilosCount: 2);

// Configure silos
builder.ConfigureSilo ((options, siloBuilder) =>
{
    siloBuilder.AddMemoryGrainStorage("Default");
    siloBuilder.AddMemoryGrainStorage("PubSubStore");
});

// Configure clients
builder.ConfigureClient (clientBuilder =>
{
    // Client-specific configuration
});

// Configure both silos and clients (shared services)
builder.ConfigureHost (hostBuilder =>
{
    hostBuilder.Services.AddSingleton<IMyService, MyService>();
});

var cluster = builder.Build();
await cluster.DeployAsync();

```

--------------------------------

### Implement a Startup Task for Silo Lifecycle

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/silo-lifecycle

Inherit from ILifecycleParticipant to subscribe custom logic to the silo lifecycle at a specified stage. This pattern replaces older bootstrap provider methods for injecting logic during silo startup.

```csharp
class StartupTask : ILifecycleParticipant<ISiloLifecycle>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Func<IServiceProvider, CancellationToken, Task> _startupTask;
    private readonly int _stage;

    public StartupTask(
        IServiceProvider serviceProvider,
        Func<IServiceProvider, CancellationToken, Task> startupTask,
        int stage)
    {
        _serviceProvider = serviceProvider;
        _startupTask = startupTask;
        _stage = stage;
    }

    public void Participate(ISiloLifecycle lifecycle)
    {
        lifecycle.Subscribe<StartupTask>(
            _stage,
            cancellation => _startupTask(_serviceProvider, cancellation));
    }
}
```

--------------------------------

### Add Azure Identity NuGet Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Install the Azure.Identity NuGet package to enable passwordless authentication using Azure role-based access control. This package is required for secure communication with Azure services.

```csharp
dotnet add package Azure.Identity --version 1.*

```

--------------------------------

### Retrieve JournaledGrain Statistics

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/journaledgrain-diagnostics

Call this method to get the current statistics collected for a JournaledGrain. This provides basic insights into the grain's operational metrics.

```csharp
LogConsistencyStatistics GetStats()
```

--------------------------------

### Implement ICommunicationListener for Orleans Host

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/service-fabric

This class implements the ICommunicationListener interface to manage the lifecycle of an IHost instance within Service Fabric. It starts and stops the host when the service opens and closes.

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace ServiceFabric.HostingExample;

internal sealed class HostedServiceCommunicationListener : ICommunicationListener
{
    private IHost? _host;
    private readonly Func<Task<IHost>> _createHost;

    public HostedServiceCommunicationListener(Func<Task<IHost>> createHost) =>
        _createHost = createHost ?? throw new ArgumentNullException(nameof(createHost));

    /// <inheritdoc />
    public async Task<string?> OpenAsync(CancellationToken cancellationToken)
    {
        try
        {
            _host = await _createHost.Invoke();
            await _host.StartAsync(cancellationToken);
        }
        catch
        {
            Abort();
            throw;
        }

        // This service does not expose any endpoints to Service Fabric for discovery by others.
        return null;
    }

    /// <inheritdoc />
    public async Task CloseAsync(CancellationToken cancellationToken)
    {
        if (_host is { } host)
        {
            await host.StopAsync(cancellationToken);
        }

        _host = null;
    }

    /// <inheritdoc />
    public void Abort()
    {
        IHost? host = _host;
        if (host is null)
        {
            return;
        }

        using CancellationTokenSource cancellation = new();
        cancellation.Cancel(false);

        try
        {
            host.StopAsync(cancellation.Token).GetAwaiter().GetResult();
        }
        catch
        {
            // Ignore.
        }
        finally
        {
            _host = null;
        }
    }
}

```

--------------------------------

### Configure Azure Queue Storage Streaming with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/stream-providers

Set up Azure Queue Storage for Orleans streaming in the AppHost project. Ensure the 'streaming' queues are registered and referenced.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage");
var queues = storage.AddQueues("streaming");

var orleans = builder.AddOrleans("cluster")
    .WithClustering(builder.AddRedis("redis"))
    .WithStreaming("AzureQueueProvider", queues);

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans)
    .WithReference(queues);

builder.Build().Run();

```

--------------------------------

### Configure ADO.NET for Silo (Orleans 10.0+)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/configuring-ado-dot-net-providers

Configure ADO.NET for clustering, reminder service, and grain storage on the silo. Requires `Microsoft.Data.SqlClient`.

```csharp
var builder = Host.CreateApplicationBuilder(args);

var invariant = "Microsoft.Data.SqlClient";
var connectionString = "Data Source=(localdb)\MSSQLLocalDB;" +
    "Initial Catalog=Orleans;Integrated Security=True;" +
    "Pooling=False;Max Pool Size=200;" +
    "MultipleActiveResultSets=True";

builder.UseOrleans(siloBuilder =>
{
    // Use ADO.NET for clustering
    siloBuilder.UseAdoNetClustering(options =>
    {
        options.Invariant = invariant;
        options.ConnectionString = connectionString;
    });
    // Use ADO.NET for reminder service
    siloBuilder.UseAdoNetReminderService(options =>
    {
        options.Invariant = invariant;
        options.ConnectionString = connectionString;
    });
    // Use ADO.NET for persistence
    siloBuilder.AddAdoNetGrainStorage("GrainStorageForTest", options =>
    {
        options.Invariant = invariant;
        options.ConnectionString = connectionString;
    });
});

```

--------------------------------

### Configure Application Parts

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/client-configuration

Configure the client's application parts by adding a specific assembly, such as the one containing grain interfaces like IValueGrain. The WithReferences() method ensures that referenced assemblies are also included.

```csharp
public static void ConfigureApplicationParts(IClientBuilder clientBuilder)
{
    clientBuilder.ConfigureApplicationParts(
        parts => parts.AddApplicationPart(
            typeof(IValueGrain).Assembly)
            .WithReferences());
}
```

--------------------------------

### Orleans Grain Implementation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Implements the URL shortener grain, using persistent state to store the full URL. The grain provides methods to set and get the URL, persisting changes using `WriteStateAsync`.

```csharp
// <grain>
public sealed class UrlShortenerGrain(
    [PersistentState(
        stateName: "url",
        storageName: "urls")]
        IPersistentState<UrlDetails> state)
    : Grain, IUrlShortenerGrain
{
    public async Task SetUrl(string fullUrl)
    {
        state.State = new()
        {
            ShortenedRouteSegment = this.GetPrimaryKeyString(),
            FullUrl = fullUrl
        };

        await state.WriteStateAsync();
    }

    public Task<string> GetUrl() =>
        Task.FromResult(state.State.FullUrl);
}

[GenerateSerializer, Alias(nameof(UrlDetails))]
public sealed record class UrlDetails
{
    [Id(0)]
    public string FullUrl { get; set; } = "";

    [Id(1)]
    public string ShortenedRouteSegment { get; set; } = "";
}
// </grain>
```

--------------------------------

### Reliable Client Deployment using SQL Server

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure clients to connect to a cluster using SQL Server for clustering. Requires a SQL Server connection string.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";

var client = new ClientBuilder()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAdoNetClustering(options =>
    {
      options.ConnectionString = connectionString;
      options.Invariant = "System.Data.SqlClient";
    })
    .Build();


```

--------------------------------

### Inject IGrainFactory into an Orleans Interceptor

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Inject IGrainFactory into your interceptor class to enable making grain calls. This example shows how to conditionally call another grain to avoid infinite recursion.

```csharp
public class CustomCallFilter : Orleans.IIncomingGrainCallFilter
{
    private readonly IGrainFactory _grainFactory;

    public CustomCallFilter(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public async Task Invoke(Orleans.IIncomingGrainCallContext context)
    {
        // Hook calls to any grain other than ICustomFilterGrain implementations.
        // This avoids potential infinite recursion when calling OnReceivedCall() below.
        if (context.Grain is not ICustomFilterGrain)
        {
            var filterGrain = _grainFactory.GetGrain<ICustomFilterGrain>(
                ((IAddressable)context.Grain).GetPrimaryKeyLong());

            // Perform some grain call here.
            await filterGrain.OnReceivedCall();
        }

        // Continue invoking the call on the target grain.
        await context.Invoke();
    }
}
```

--------------------------------

### Azure Service Principal Credentials

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Example JSON output for Azure service principal credentials. This structure is used to authenticate with Azure for automated tasks. Replace placeholder values with your actual subscription and tenant information.

```json
{
  "clientId": "<your client id>",
  "clientSecret": "<your client secret>",
  "subscriptionId": "<your subscription id>",
  "tenantId": "<your tenant id>",
  "activeDirectoryEndpointUrl": "https://login.microsoftonline.com/",
  "resourceManagerEndpointUrl": "https://brazilus.management.azure.com",
  "activeDirectoryGraphResourceId": "https://graph.windows.net/",
  "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
  "galleryEndpointUrl": "https://gallery.azure.com",
  "managementEndpointUrl": "https://management.core.windows.net"
}

```

--------------------------------

### FileGrainStorage Class Constructor and Members

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Initializes the FileGrainStorage with necessary dependencies like storage name, options, cluster options, grain factory, and type resolver.

```csharp
public class FileGrainStorage
    : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
{
    private readonly string _storageName;
    private readonly FileGrainStorageOptions _options;
    private readonly ClusterOptions _clusterOptions;
    private readonly IGrainFactory _grainFactory;
    private readonly ITypeResolver _typeResolver;
    private JsonSerializerSettings _jsonSettings;

    public FileGrainStorage(
        string storageName,
        FileGrainStorageOptions options,
        IOptions<ClusterOptions> clusterOptions,
        IGrainFactory grainFactory,
        ITypeResolver typeResolver)
    {
        _storageName = storageName;
        _options = options;
        _clusterOptions = clusterOptions.Value;
        _grainFactory = grainFactory;
        _typeResolver = typeResolver;
    }

    public Task ClearStateAsync(
        string grainType,
        GrainReference grainReference,
        IGrainState grainState)
    {
        throw new NotImplementedException();
    }

    public Task ReadStateAsync(
        string grainType,
        GrainReference grainReference,
        IGrainState grainState)
    {
        throw new NotImplementedException();
    }

    public Task WriteStateAsync(
        string grainType,
        GrainReference grainReference,
        IGrainState grainState)
    {
        throw new NotImplementedException();
    }

    public void Participate(
        ISiloLifecycle lifecycle)
    {
        throw new NotImplementedException();
    }
}
```

--------------------------------

### Orleans 8.x+ Timer Registration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Example of registering a timer using the `RegisterGrainTimer` extension method in Orleans 8.x+. The callback is strongly typed and receives a `CancellationToken`. Interleaving is explicitly controlled via `GrainTimerCreationOptions`.

```csharp
public class MyGrainAfter : Grain, IMyGrain
{
    private IGrainTimer? _timer;

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        // Use this.RegisterGrainTimer() - an extension method on IGrainBase
        _timer = this.RegisterGrainTimer(
            static (state, ct) => state.DoWorkAsync(ct),
            this,
            new GrainTimerCreationOptions
            {
                DueTime = TimeSpan.FromSeconds(5),
                Period = TimeSpan.FromSeconds(10),
                Interleave = true  // Set to true to match old behavior
            });
        
        return Task.CompletedTask;
    }

    private Task DoWorkAsync(CancellationToken cancellationToken)
    {
        // Timer work - check cancellationToken for graceful shutdown
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Configure Multiple Named Directories

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/grain-directory

Configure multiple grain directories with different names, such as Redis and Azure Table, for use with different grain classes.

```csharp
siloBuilder
    .AddRedisGrainDirectory(
        "redis-directory-1",
        options => options.ConfigurationOptions = redisConfiguration1)
    .AddRedisGrainDirectory(
        "redis-directory-2",
        options => options.ConfigurationOptions = redisConfiguration2)
    .AddAzureTableGrainDirectory(
        "azure-directory",
        options => options.ConnectionString = azureConnectionString);

```

--------------------------------

### Retrieve Trace ID in Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-context

Example of retrieving a trace ID from the request context within grain code for logging purposes. If no trace ID is found, it defaults to 'No trace ID'.

```csharp
Logger.LogInformation("Currently processing external request {TraceId}",
    RequestContext.Get("TraceId"));
```

--------------------------------

### Enable Aspire Deploy Command (Bash)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Enables the preview 'aspire deploy' command by setting an environment variable in Bash. This is required before running deployment commands.

```bash
export DOTNET_ASPIRE_ENABLE_DEPLOY_COMMAND=true

```

--------------------------------

### Create a Grain Timer with RegisterGrainTimer

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Use `RegisterGrainTimer` to create a timer that fires periodically. Set `KeepAlive` to `true` to prevent grain collection while the timer is active. The `Period` is the time between the resolution of one callback and the start of the next.

```csharp
public class MyGrain : Grain, IMyGrain
{
    private IGrainTimer? _timer;

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        // Create a timer that fires every 10 seconds, starting 5 seconds after activation
        // RegisterGrainTimer is an extension method on IGrainBase (which Grain implements)
        _timer = this.RegisterGrainTimer(
            static (state, ct) => state.DoWorkAsync(ct),
            this,
            new GrainTimerCreationOptions
            {
                DueTime = TimeSpan.FromSeconds(5),
                Period = TimeSpan.FromSeconds(10),
                KeepAlive = true  // Prevent grain collection while timer is active
            });

        return Task.CompletedTask;
    }

    private Task DoWorkAsync(CancellationToken cancellationToken)
    {
        // Timer callback work
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Configure ADO.NET for Client (Orleans < 10.0)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/configuring-ado-dot-net-providers

Configure ADO.NET for clustering on the client side. Uses `System.Data.SqlClient`.

```csharp
var clientBuilder = new ClientBuilder();

var invariant = "System.Data.SqlClient";
var connectionString = "Data Source=(localdb)\MSSQLLocalDB;" +
    "Initial Catalog=Orleans;Integrated Security=True;" +
    "Pooling=False;Max Pool Size=200;" +
    "Asynchronous Processing=True;MultipleActiveResultSets=True";

// Use ADO.NET for clustering
clientBuilder.UseAdoNetClustering(options =>
{
    options.Invariant = invariant;
    options.ConnectionString = connectionString;
});


```

--------------------------------

### Unreliable Silo Deployment on Dedicated Servers

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure silos for testing on a cluster of dedicated servers where reliability is not a concern. This setup avoids Azure Table dependency and designates a primary silo.

```csharp
var primarySiloEndpoint = new IPEndPoint(PRIMARY_SILO_IP_ADDRESS, 11_111);

var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleans(siloBuilder =>
{
    siloBuilder
        .UseDevelopmentClustering(primarySiloEndpoint)
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "Cluster42";
            options.ServiceId = "MyAwesomeService";
        })
        .ConfigureEndpoints(siloPort: 11_111, gatewayPort: 30_000);
});
builder.Logging.AddConsole();

using var host = builder.Build();
await host.RunAsync();

```

--------------------------------

### Orleans Client with ClientBuilder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

This snippet demonstrates configuring an Orleans client using `ClientBuilder`. It connects to Azure Storage, retrieves a player's game, and subscribes to updates via an observer.

```csharp
internal static class ExternalClientExample
{
    private static string connectionString = "UseDevelopmentStorage=true";

    public static async Task RunWatcherAsync()
    {
        try
        {
            var client = new ClientBuilder()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "my-first-cluster";
                    options.ServiceId = "MyOrleansService";
                })
                .UseAzureStorageClustering(
                    options => options.ConfigureTableServiceClient(connectionString))
                .ConfigureApplicationParts(
                    parts => parts.AddApplicationPart(typeof(IValueGrain).Assembly))
                .Build();

                // Hardcoded player ID
                Guid playerId = new("{2349992C-860A-4EDA-9590-000000000006}");
                IPlayerGrain player = client.GetGrain<IPlayerGrain>(playerId);
                IGameGrain? game = null;
                while (game is null)
                {
                    Console.WriteLine(
                        $"Getting current game for player {playerId}...");

                    try
                    {
                        game = await player.GetCurrentGame();
                        if (game is null) // Wait until the player joins a game
                        {
                            await Task.Delay(TimeSpan.FromMilliseconds(5_000));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.GetBaseException()}");
                    }
                }

                Console.WriteLine(
                    $"Subscribing to updates for game {game.GetPrimaryKey()}...");

                // Subscribe for updates
                var watcher = new GameObserver();
                await game.SubscribeForGameUpdates(
                    await client.CreateObjectReference<IGameObserver>(watcher));

                Console.WriteLine(
                    "Subscribed successfully. Press <Enter> to stop.");

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Unexpected Error: {e.GetBaseException()}");
            }
        }
    }

/// <summary>
/// Observer class that implements the observer interface.
/// Need to pass a grain reference to an instance of
/// this class to subscribe for updates.
/// </summary>
class GameObserver : IGameObserver
{
    public void UpdateGameScore(string score)
    {
        Console.WriteLine("New game score: {0}", score);
    }
}

```

--------------------------------

### Account Grain Interface with Transactional Withdraw, Deposit, and GetBalance

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

Defines transactional operations for an account grain. Withdraw and Deposit require an existing transaction context (TransactionOption.Join), while GetBalance can operate within an existing transaction or start a new one (TransactionOption.CreateOrJoin).

```csharp
namespace TransactionalExample.Abstractions;

public interface IAccountGrain : IGrainWithStringKey
{
    [Transaction(TransactionOption.Join)]
    Task Withdraw(decimal amount);

    [Transaction(TransactionOption.Join)]
    Task Deposit(decimal amount);

    [Transaction(TransactionOption.CreateOrJoin)]
    Task<decimal> GetBalance();
}
```

--------------------------------

### Ambiguous Grain Reference Creation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

Attempting to get a grain reference using a generic interface when multiple implementations exist will result in an exception. Orleans cannot determine which concrete grain class to instantiate.

```csharp
// This will throw an exception: there is no unambiguous mapping from ICounterGrain to a grain class.
ICounterGrain myCounter = grainFactory.GetGrain<ICounterGrain>("my-counter");
```

--------------------------------

### Configure Azure Cosmos DB Reminder Service

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Configure the Azure Cosmos DB provider for Orleans reminders. This requires installing the Microsoft.Orleans.Reminders.Cosmos NuGet package and providing your Cosmos DB account details.

```csharp
public static async Task ConfigureCosmosAsync(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);
    builder.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseCosmosReminderService(options =>
        {
            options.ConfigureCosmosClient(
                "https://myaccount.documents.azure.com:443/",
                new DefaultAzureCredential());
            options.DatabaseName = "Orleans";
            options.ContainerName = "OrleansReminders";
            options.IsResourceCreationEnabled = true;
        });
    });

    using var host = builder.Build();
    await host.RunAsync();
}
```

--------------------------------

### Enable Transactions on Client Host Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

Configure the client host builder to enable transaction support by calling UseTransactions.

```csharp
var builder = Host.CreateDefaultBuilder(args)
    .UseOrleansClient((context, clientBuilder) =>
    {
        clientBuilder.UseTransactions();
    });

```

--------------------------------

### Basic ASP.NET Core Program Structure

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

This is the initial structure of the Program.cs file in an ASP.NET Core Minimal API project before Orleans integration. It sets up a basic web application with a root endpoint.

```csharp
using Orleans.Runtime;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("", () => "Hello World!");

app.Run();

```

--------------------------------

### Example of a Reentrant Grain with an Error

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

This grain is marked with ReentrantAttribute, allowing concurrent requests. However, the Increment method contains an error due to improper handling of state changes around an await point, leading to a potential race condition. Do not use this code as is; removing ReentrantAttribute fixes the issue.

```csharp
[Reentrant]
public sealed class CounterGrain : Grain, ICounterGrain
{
    int _value;

    /// <summary>
    /// Increments the grain's value and returns the previous value.
    /// </summary>
    public Task<int> Increment()
    {
        // Do not copy this code, it contains an error.
        var currentVal = _value;
        await Task.Delay(TimeSpan.FromMilliseconds(1_000));
        _value = currentVal + 1;
        return currentValue;
    }
}
```

--------------------------------

### Configure Simple Message Stream Provider on Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

Add a simple message stream provider for the client to enable stream functionality.

```csharp
clientBuilder.AddSimpleMessageStreamProvider("SMSProvider");

```

--------------------------------

### Create StockWorker to Publish Messages - C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/broadcast-channel

A background service that publishes stock updates to a broadcast channel. It retrieves stock data and broadcasts it every 15 seconds. Ensure the channel name used here matches the configuration in the silo setup.

```csharp
using System.Diagnostics;
using BroadcastChannel.GrainInterfaces;
using Microsoft.Extensions.Hosting;
using Orleans.BroadcastChannel;

namespace BroadcastChannel.Silo.Services;

internal sealed class StockWorker : BackgroundService
{
    private readonly StockClient _stockClient;
    private readonly IBroadcastChannelProvider _provider;
    private readonly List<StockSymbol> _symbols = Enum.GetValues<StockSymbol>().ToList();

    public StockWorker(
        StockClient stockClient, IClusterClient clusterClient) =>
        (_stockClient, _provider) = 
        (stockClient, clusterClient.GetBroadcastChannelProvider(ChannelNames.LiveStockTicker));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Capture the starting timestamp.
            long startingTimestamp = Stopwatch.GetTimestamp();

            // Get all updated stock values.
            Stock[] stocks = await Task.WhenAll(
                tasks: _symbols.Select(selector: _stockClient.GetStockAsync));

            // Get the live stock ticker broadcast channel.
            ChannelId channelId = ChannelId.Create(ChannelNames.LiveStockTicker, Guid.Empty);
            IBroadcastChannelWriter<Stock> channelWriter = _provider.GetChannelWriter<Stock>(channelId);

            // Broadcast all stock updates on this channel.
            await Task.WhenAll(
                stocks.Where(s => s is not null).Select(channelWriter.Publish));

            // Use the elapsed time to calculate a 15 second delay.
            int elapsed = Stopwatch.GetElapsedTime(startingTimestamp).Milliseconds;
            int remaining = Math.Max(0, 15_000 - elapsed);

            await Task.Delay(remaining, stoppingToken);
        }
    }
}

```

--------------------------------

### Implement Access Controlled Grain Call Filter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Implement a per-grain call filter that uses a custom attribute to enforce access control. This example checks for an 'isAdmin' flag in the RequestContext before allowing access to methods marked with AdminOnlyAttribute.

```csharp
public class MyAccessControlledGrain
    : Grain, IMyFilteredGrain, Orleans.IIncomingGrainCallFilter
{
    public Task Invoke(Orleans.IIncomingGrainCallContext context)
    {
        // Check access conditions.
        var isAdminMethod =
            context.ImplementationMethod.GetCustomAttribute<AdminOnlyAttribute>();
        if (isAdminMethod is not null && RequestContext.Get("isAdmin") is not true)
        {
            throw new AccessDeniedException(
                $"Only admins can access {context.ImplementationMethod.Name}!");
        }

        return context.Invoke();
    }

    [AdminOnly]
    public Task<int> GetFavoriteNumber() => Task.FromResult(7);
}

```

--------------------------------

### AppHost Project: Production Configuration with Azure Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configures an Orleans cluster using Azure Table Storage for clustering and Azure Blob Storage for grain state. `.RunAsEmulator()` is used for local development with Azurite; remove it for production.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator();  // Use Azurite for local development
var tables = storage.AddTables("clustering");
var blobs = storage.AddBlobs("grainstate");

var orleans = builder.AddOrleans("cluster")
    .WithClustering(tables)
    .WithGrainStorage("Default", blobs);

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans)
    .WaitFor(storage)
    .WithReplicas(3);

builder.Build().Run();

```

--------------------------------

### Invalid grain implementation for heterogeneous silos

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/heterogeneous-silos

This C# code demonstrates an invalid grain implementation for heterogeneous silos. A grain type implementation must be the same on each silo that supports it. This example shows a grain with different interfaces and methods on different silos.

```csharp
public class C: Grain, IMyGrainInterface, IMyOtherGrainInterface
{
   public Task SomeMethod() { /* ... */ }
   public Task SomeOtherMethod() { /* ... */ }
}
```

--------------------------------

### FileGrainStorage Class Implementation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

This class implements the IGrainStorage and ILifecycleParticipant interfaces for a custom file-based grain storage provider. It includes constructor parameters for storage name, options, and cluster options, along with placeholder methods for state operations and lifecycle participation.

```csharp
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Storage;

namespace GrainStorage;

public sealed class FileGrainStorage : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
{
    private readonly string _storageName;
    private readonly FileGrainStorageOptions _options;
    private readonly ClusterOptions _clusterOptions;

    public FileGrainStorage(
        string storageName,
        FileGrainStorageOptions options,
        IOptions<ClusterOptions> clusterOptions)
    {
        _storageName = storageName;
        _options = options;
        _clusterOptions = clusterOptions.Value;
    }

    public Task ClearStateAsync<T>(
        string stateName,
        GrainId grainId,
        IGrainState<T> grainState)
    {
        throw new NotImplementedException();
    }

    public Task ReadStateAsync<T>(
        string stateName,
        GrainId grainId,
        IGrainState<T> grainState)
    {
        throw new NotImplementedException();
    }

    public Task WriteStateAsync<T>(
        string stateName,
        GrainId grainId,
        IGrainState<T> grainState)
    {
        throw new NotImplementedException();
    }

    public void Participate(ISiloLifecycle lifecycle) =>
        throw new NotImplementedException();
}

```

--------------------------------

### Use Grain State Extension to Get and Set State

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-extensions

Access and modify grain state using the registered IGrainStateAccessor<T> extension. Obtain a reference to the extension via AsReference and then call GetState() and SetState(T state).

```csharp
// Get a reference to the IGrainStateAccessor<int> extension
var accessor = grain.AsReference<IGrainStateAccessor<int>>();

// Get the current value of the state
var value = await accessor.GetState();

// Set a new value of the state
await accessor.SetState(10);
```

--------------------------------

### Enable Activation Repartitioning in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

Use the `AddActivationRepartitioner` extension method to enable the activation repartitioning feature. Suppress experimental warnings using `#pragma warning disable ORLEANSP001` or project file configuration.

```csharp
#pragma warning disable ORLEANSEXP001
siloBuilder.AddActivationRepartitioner();
#pragma warning restore ORLEANSEXP001

```

--------------------------------

### Explicitly Subscribe to an Orleans Stream

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

This code demonstrates how to explicitly subscribe a grain to an Orleans stream within its OnActivateAsync method. It retrieves a stream provider, creates a stream ID, gets the stream, and then subscribes an IAsyncObserver to it.

```csharp
public override async Task OnActivateAsync(CancellationToken cancellationToken)
{
    IStreamProvider streamProvider =
        this.GetStreamProvider("SimpleStreamProvider");

    StreamId streamId =
        StreamId.Create("MyStreamNamespace", this.GetPrimaryKey());
    IAsyncStream<string> stream = 
        streamProvider.GetStream<string>(streamId);

    StreamSubscriptionHandle<string> subscription = 
        await stream.SubscribeAsync(new MyStreamObserver());
}
```

--------------------------------

### Configure Azure Table Storage in AppHost

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-storage

Sets up Azure Storage and Table container for Orleans grain state in the AppHost project.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage");
var tables = storage.AddTables("grainstate");

var orleans = builder.AddOrleans("cluster")
    .WithClustering(builder.AddRedis("redis"))
    .WithGrainStorage("Default", tables);

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans)
    .WithReference(tables);

builder.Build().Run();

```

--------------------------------

### Accessing JournaledGrain State and Version

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/journaledgrain-basics

Use the `State` property to read the current grain state and the `Version` property to get the total number of confirmed events. Never modify the `State` object directly; raise events instead.

```csharp
GrainState State { get; }
int Version { get; }
```

--------------------------------

### Provision Azure Resources

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Run 'azd provision' to redeploy your application architecture with the new configuration. This command provisions the necessary Azure resources based on your environment settings. The process can take approximately two minutes.

```bash
azd provision

```

--------------------------------

### Configure MemoryStream

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Configure MemoryStream with a default serializer and specify the number of partitions. The partition count should not be changed after deployment.

```csharp
builder.AddMemoryStreams<DefaultMemoryMessageBodySerializer>(
    "in-mem-provider",
    _ =>
    {
        // Number of pulling agent to start.
        // DO NOT CHANGE this value once deployed, if you do rolling deployment
        _.ConfigurePartitioning(partitionCount: 8);
    });

```

--------------------------------

### Configure ADO.NET Grain Storage Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/relational-storage

Configure an ADO.NET storage provider via the silo builder in C#. Ensure to replace `<Invariant>` and `<ConnectionString>` with your specific database details.

```csharp
var builder = Host.CreateApplicationBuilder(args);
siloBuilder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddAdoNetGrainStorage("OrleansStorage", options =>
    {
        options.Invariant = "<Invariant>";
        options.ConnectionString = "<ConnectionString>";
    });
});

using var host = builder.Build();
await host.RunAsync();

```

--------------------------------

### Azure Storage Configuration for Orleans with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Sets up Azure Storage for Orleans clustering, grain storage, and reminders using .NET Aspire. Uses the Azurite emulator for local development.

```csharp
public static void AzureStorageWithAspire(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    // Add Azure Storage for Orleans
    var storage = builder.AddAzureStorage("orleans-storage")
        .RunAsEmulator();  // Use Azurite emulator for local development

    var tables = storage.AddTables("orleans-tables");
    var blobs = storage.AddBlobs("orleans-blobs");

    var orleans = builder.AddOrleans("cluster")
        .WithClustering(tables)
        .WithGrainStorage("Default", blobs)
        .WithReminders(tables);

    builder.AddProject<Projects.Silo>("silo")
        .WithReference(orleans)
        .WaitFor(storage)
        .WithReplicas(3);

    builder.Build().Run();
}
```

--------------------------------

### Orleans Client with Host Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

This snippet shows how to configure and run an Orleans client using `Host.CreateDefaultBuilder`. It connects to an Azure Storage clustered service and subscribes to game updates.

```csharp
try
{
    using IHost host = Host.CreateDefaultBuilder(args)
        .UseOrleansClient((context, client) =>
        {
            client.Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "my-first-cluster";
                options.ServiceId = "MyOrleansService";
            })
            .UseAzureStorageClustering(
                options => options.TableServiceClient = new TableServiceClient(
                    context.Configuration["ORLEANS_AZURE_STORAGE_CONNECTION_STRING"]));
        })
        .UseConsoleLifetime()
        .Build();

    await host.StartAsync();

    IGrainFactory client = host.Services.GetRequiredService<IGrainFactory>();

    // Hardcoded player ID
    Guid playerId = new("{2349992C-860A-4EDA-9590-000000000006}");
    IPlayerGrain player = client.GetGrain<IPlayerGrain>(playerId);
    IGameGrain? game = null;
    while (game is null)
    {
        Console.WriteLine(
            $"Getting current game for player {playerId}...");

        try
        {
            game = await player.GetCurrentGame();
            if (game is null) // Wait until the player joins a game
            {
                await Task.Delay(TimeSpan.FromMilliseconds(5_000));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.GetBaseException()}");
        }
    }

    Console.WriteLine(
        $"Subscribing to updates for game {game.GetPrimaryKey()}...");

    // Subscribe for updates
    var watcher = new GameObserver();
    await game.ObserveGameUpdates(
        client.CreateObjectReference<IGameObserver>(watcher));

    Console.WriteLine(
        "Subscribed successfully. Press <Enter> to stop.");
}
catch (Exception e)
{
    Console.WriteLine(
        $"Unexpected Error: {e.GetBaseException()}");
}

```

--------------------------------

### Orleans Silo Membership Data from Consul KV

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/consul-deployment

The decoded Base64 UTF-8 string from Consul's KV store provides the actual Orleans membership data for a silo, including its hostname, ports, start time, status, and name.

```json
{
    "Hostname": "[YOUR_MACHINE_NAME]",
    "ProxyPort": 30000,
    "StartTime": "2023-05-15T14:22:00.004977Z",
    "Status": 3,
    "SiloName": "Silo_fcad0",
    "SuspectingSilos": []
}

```

--------------------------------

### AppHost Project Package References

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Add these package references to your AppHost project to enable Orleans integration with .NET Aspire.

```xml
<ItemGroup>
  <PackageReference Include="Aspire.Hosting.AppHost" Version="13.1.3" />
  <PackageReference Include="Aspire.Hosting.Orleans" Version="13.1.3" />
  <PackageReference Include="Aspire.Hosting.Redis" Version="13.1.3" />
</ItemGroup>
```

--------------------------------

### Configure ADO.NET for Silo (Orleans < 10.0)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/configuring-ado-dot-net-providers

Configure ADO.NET for clustering, reminder service, and grain storage on the silo. Uses `System.Data.SqlClient`.

```csharp
var siloHostBuilder = new SiloHostBuilder();

var invariant = "System.Data.SqlClient";
var connectionString = "Data Source=(localdb)\MSSQLLocalDB;" +
    "Initial Catalog=Orleans;Integrated Security=True;" +
    "Pooling=False;Max Pool Size=200;" +
    "Asynchronous Processing=True;MultipleActiveResultSets=True";

// Use ADO.NET for clustering
siloHostBuilder.UseAdoNetClustering(options =>
{
    options.Invariant = invariant;
    options.ConnectionString = connectionString;
});
// Use ADO.NET for reminder service
siloHostBuilder.UseAdoNetReminderService(options =>
{
    options.Invariant = invariant;
    options.ConnectionString = connectionString;
});
// Use ADO.NET for persistence
siloHostBuilder.AddAdoNetGrainStorage("GrainStorageForTest", options =>
{
    options.Invariant = invariant;
    options.ConnectionString = connectionString;
});


```

--------------------------------

### Configure Orleans Logging to Info Level (Declarative)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/troubleshooting-azure-cloud-services-deployments

Add the `<Tracing>` element with `DefaultTraceLevel="Info"` to your Orleans configuration files (_OrleansConfiguration.xml and/or _ClientConfiguration.xml). This enables informational logging declaratively.

```xml
<Tracing DefaultTraceLevel="Info" />
```

--------------------------------

### Register Startup Task with Silo Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/silo-lifecycle

Register a startup task with the silo builder to execute custom initialization logic at a specific lifecycle stage. This ensures your custom logic participates in the silo's startup sequence.

```csharp
siloBuilder.AddStartupTask(
    async (serviceProvider, cancellationToken) =>
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Silo is starting up...");

        // Perform initialization logic, such as warming caches or validating configuration
        var config = serviceProvider.GetRequiredService<IConfiguration>();
        await ValidateExternalDependenciesAsync(config, cancellationToken);
    },
    ServiceLifecycleStage.Active);

```

--------------------------------

### Configure Orleans Client with Localhost Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/tutorial-1

Sets up an Orleans client that connects to a cluster using the localhost clustering provider. Ensure the client's clustering configuration matches the Silo's.

```csharp
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrainInterfaces;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(client =>
    {
        client.UseLocalhostClustering();
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();

using IHost host = builder.Build();
await host.StartAsync();

IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

IHello friend = client.GetGrain<IHello>(0);
string response = await friend.SayHello("Hi friend!");

Console.WriteLine($"""
    {response}

    Press any key to exit...
    """);

Console.ReadKey();

await host.StopAsync();

```

--------------------------------

### Get Grain Reference from Grain Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

Obtain a grain reference for IPlayerGrain within a grain class using GrainFactory.GetGrain. The player ID is typically read from an external source.

```csharp
IPlayerGrain player = GrainFactory.GetGrain<IPlayerGrain>(playerId);
```

--------------------------------

### Configure Cassandra Clustering with Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Use the options-based configuration for more granular control over Cassandra clustering, including client configuration and retry delays.

```csharp
siloBuilder.UseCassandraClustering(options =>
{
    options.ConfigureClient("Contact Points=cassandra-node1,cassandra-node2;Port=9042", "orleans");
    options.UseCassandraTtl = true;
    options.InitializeRetryMaxDelay = TimeSpan.FromSeconds(30);
});

```

--------------------------------

### Get Tentative State in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/immediate-vs-delayed-confirmation

Retrieve a 'tentative' state that includes the effect of all unconfirmed events. This property is useful when the confirmed state might not yet reflect the latest events.

```csharp
StateType TentativeState { get; }
```

--------------------------------

### Configure TLS with a Certificate File in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/transport-layer-security

Use this snippet to load a TLS certificate from a PKCS#12 file (PFX) and configure Orleans to use it. The `OnAuthenticateAsClient` callback sets the `TargetHost` for client authentication.

```csharp
using var cert = X509CertificateLoader.LoadPkcs12FromFile("path/to/certificate.pfx", "password");

using IHost host = Host.CreateDefaultBuilder()
    .UseOrleans(builder =>
    {
        builder
            .UseLocalhostClustering()
            .UseTls(cert, options =>
            {
                options.OnAuthenticateAsClient = (connection, sslOptions) =>
                {
                    sslOptions.TargetHost = cert.GetNameInfo(X509NameType.DnsName, false) ?? "my-certificate-subject";
                };
            });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.RunAsync();

```

--------------------------------

### GitHub Actions Workflow for Azure App Service Deployment

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

This YAML workflow automates the build and deployment of a .NET application to Azure App Service. It checks out code, sets up .NET, publishes the app, logs into Azure, deploys resources using Bicep, and deploys the application zip to both production and staging slots.

```yaml
name: Deploy to Azure App Service

on:
  push:
    branches:
    - main

env:
  UNIQUE_APP_NAME: cartify
  AZURE_RESOURCE_GROUP_NAME: orleans-resourcegroup
  AZURE_RESOURCE_GROUP_LOCATION: centralus

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3

    - name: Setup .NET 8.0
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x

    - name: .NET publish shopping cart app
      run: dotnet publish ./Silo/Orleans.ShoppingCart.Silo.csproj --configuration Release

    - name: Login to Azure
      uses: azure/login@v1
      with:
        creds: ${{ secrets.AZURE_CREDENTIALS }}

    - name: Flex bicep
      run: |
        az deployment group create \
          --resource-group ${{ env.AZURE_RESOURCE_GROUP_NAME }}
          --template-file '.github/workflows/flex/main.bicep' \
          --parameters location=${{ env.AZURE_RESOURCE_GROUP_LOCATION }} \
            appName=${{ env.UNIQUE_APP_NAME }} \
          --debug

    - name: Webapp deploy
      run: |
        az webapp deploy --name ${{ env.UNIQUE_APP_NAME }} \
          --resource-group ${{ env.AZURE_RESOURCE_GROUP_NAME  }} \
          --clean true --restart true \
          --type zip --src-path silo.zip --debug

    - name: Staging deploy
      run: |
        az webapp deploy --name ${{ env.UNIQUE_APP_NAME }} \
          --slot ${{ env.UNIQUE_APP_NAME }}stg \
          --resource-group ${{ env.AZURE_RESOURCE_GROUP_NAME  }} \
          --clean true --restart true \
          --type zip --src-path silo.zip --debug

```

--------------------------------

### Use Async/Await

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

Leverage the standard .NET Task-Async programming model with async/await keywords. This approach is fully supported and recommended for asynchronous operations.

```csharp
The normal .NET Task-Async programming model. Supported & recommended
```

--------------------------------

### Configure Redis Reminders in AppHost with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Use this snippet in your AppHost's Program.cs to configure Redis for Orleans clustering and reminders. Ensure Redis is added and referenced correctly.

```csharp
public static void RemindersRedisAppHost(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    var redis = builder.AddRedis("redis");

    var orleans = builder.AddOrleans("cluster")
        .WithClustering(redis)
        .WithReminders(redis);

    builder.AddProject<Projects.Silo>("silo")
        .WithReference(orleans)
        .WaitFor(redis);

    builder.Build().Run();
}

```

--------------------------------

### Define Hello Grain Implementation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/overview-helloworld

Implements the IHello grain interface. This grain logs received greetings and returns a formatted response.

```csharp
public class HelloGrain : Orleans.Grain, IHello
{
    private readonly ILogger<HelloGrain> _logger;

    public HelloGrain(ILogger<HelloGrain> logger) => _logger = logger;

    Task<string> IHello.SayHello(string greeting)
    {
        _logger.LogInformation("SayHello message received: greeting = '{Greeting}'", greeting);
        return Task.FromResult($"You said: '{greeting}', I say: Hello!");
    }
}

```

--------------------------------

### Add Microsoft.Orleans.Server NuGet Package

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/local-development-configuration

Use this command to add the necessary NuGet package for Orleans server development to your project.

```bash
dotnet add package Microsoft.Orleans.Server

```

--------------------------------

### Deploy Application with Aspire CLI

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Deploys your .NET Aspire application to Azure Container Apps. This command handles provisioning, building, and deploying your application.

```bash
aspire deploy

```

--------------------------------

### Basic Silo Configuration with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configure an Orleans silo project using Aspire's service defaults and keyed Redis client. Aspire automatically injects Orleans configuration.

```csharp
public static void BasicSiloConfiguration(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    // Add Aspire service defaults (OpenTelemetry, health checks, etc.)
    builder.AddServiceDefaults();

    // Add the Aspire Redis client for Orleans
    builder.AddKeyedRedisClient("orleans-redis");

    // Configure Orleans - Aspire injects all configuration automatically
    builder.UseOrleans();

    builder.Build().Run();
}

```

--------------------------------

### Generate Kubernetes manifests with Aspire CLI

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/kubernetes

Use the Aspire CLI to publish your application and generate Kubernetes manifests. This command creates a set of YAML files for deployment.

```bash
aspire publish -o ./k8s-manifests

```

--------------------------------

### Configure Simple Message Stream Provider on Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

Add a simple message stream provider and in-memory grain storage for local development on the silo. This is not recommended for production.

```csharp
hostBuilder.AddSimpleMessageStreamProvider("SMSProvider")
           .AddMemoryGrainStorage("PubSubStore");

```

--------------------------------

### FileGrainStorage Implementation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

The full implementation of the FileGrainStorage class, including constructor and ClearStateAsync method.

```csharp
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Storage;

namespace GrainStorage;

public sealed class FileGrainStorage : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
{
    private readonly string _storageName;
    private readonly FileGrainStorageOptions _options;
    private readonly ClusterOptions _clusterOptions;

    public FileGrainStorage(
        string storageName,
        FileGrainStorageOptions options,
        IOptions<ClusterOptions> clusterOptions)
    {
        _storageName = storageName;
        _options = options;
        _clusterOptions = clusterOptions.Value;
    }

    // <clearstateasync>
    public Task ClearStateAsync<T>(
        string stateName,
        GrainId grainId,
        IGrainState<T> grainState)
    {
        var fName = GetKeyString(stateName, grainId);
        var path = Path.Combine(_options.RootDirectory, fName!);
        var fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {

```

--------------------------------

### Subscribe with Lambda Functions

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/orleans-lifecycle

Extension functions simplify subscribing to an observable lifecycle by allowing the use of lambda functions for startup and shutdown operations, instead of implementing ILifecycleObserver.

```csharp
IDisposable Subscribe(
    this ILifecycleObservable observable,
    string observerName,
    int stage,
    Func<CancellationToken, Task> onStart,
    Func<CancellationToken, Task> onStop);

IDisposable Subscribe(
    this ILifecycleObservable observable,
    string observerName,
    int stage,
    Func<CancellationToken, Task> onStart);

```

--------------------------------

### Configure Default Redis Grain Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

This C# code shows how to configure Redis as the default grain storage provider using `AddRedisGrainStorageAsDefault`. It sets up the necessary configuration options for the Redis client.

```csharp
siloBuilder.AddRedisGrainStorageAsDefault(options =>
{
    options.ConfigurationOptions = new ConfigurationOptions
    {
        EndPoints = { "localhost:6379" }
    };
});

```

--------------------------------

### Configure Grain Service and Client in Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

Configure the grain service and its client in the silo's service collection. Use AddGrainService for the service and AddSingleton for the client implementation.

```csharp
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.Services
        .AddGrainService<DataService>()
        .AddSingleton<IDataServiceClient, DataServiceClient>();
});
```

```csharp
public static void ConfigureGrainService(ISiloHostBuilder builder)
{
    builder.ConfigureServices(
        services => services.AddGrainService<DataService>()
                            .AddSingleton<IDataServiceClient, DataServiceClient>());
}
```

--------------------------------

### Configure Orleans Client Host for Local Development

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/local-development-configuration

This snippet shows how to create a default host builder and configure the Orleans client with localhost clustering and console lifetime. It's used when running a client as a host application.

```C#
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(client =>
    {
        client.UseLocalhostClustering();
    })
    .UseConsoleLifetime()
    .Build();

await host.StartAsync();

```

--------------------------------

### Configure Azure Table Storage for Pub-Sub with Connection String (Simplified)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

A simplified configuration for Azure Table Storage as the Pub-Sub store using a direct connection string. Suitable for development or environments where connection strings are managed securely.

```csharp
hostBuilder.AddAzureTableGrainStorage("PubSubStore",
    options => options.ConnectionString = "<Secret>");

```

--------------------------------

### Register IGrainService with Service Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

Register an IGrainService with the service provider. This is typically done during application startup.

```csharp
services.AddSingleton<IGrainService>(
    serviceProvider => GrainServiceFactory(grainServiceType, serviceProvider));

```

--------------------------------

### Configure Activation Repartitioner Options in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

Configure the activation repartitioner by providing `ActivationRepartitionerOptions` to the `Configure` method. This allows customization of parameters like edge count, round periods, recovery period, and anchoring filter.

```csharp
#pragma warning disable ORLEANSEXP001
siloBuilder.AddActivationRepartitioner();
siloBuilder.Configure<ActivationRepartitionerOptions>(options =>
{
    options.MaxEdgeCount = 10_000;
    options.MinRoundPeriod = TimeSpan.FromMinutes(1);
    options.MaxRoundPeriod = TimeSpan.FromMinutes(2);
    options.RecoveryPeriod = TimeSpan.FromMinutes(1);
    options.AnchoringFilterEnabled = true;
});
#pragma warning restore ORLEANSEXP001

```

--------------------------------

### Enable Transactions on Silo Host Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

Configure the silo host builder to enable transaction support by calling UseTransactions.

```csharp
var builder = Host.CreateDefaultBuilder(args)
    .UseOrleans((context, siloBuilder) =>
    {
        siloBuilder.UseTransactions();
    });

```

--------------------------------

### Register IHostedService with Host Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/startup-tasks

Register the IHostedService using AddHostedService. It will be invoked during application startup and shutdown.

```csharp
builder.Services.AddHostedService<GrainInitializerService>();

```

--------------------------------

### Add Orleans Azure SDK using directives

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Include necessary namespaces for Azure identity and Orleans configuration.

```csharp
using Azure.Identity;
using Orleans.Configuration;
```

--------------------------------

### Silo Project: Production Configuration with Redis

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Sets up the Orleans silo host with service defaults and registers a keyed Redis client for Orleans to use.

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedRedisClient("redis");
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### Deploy Kubernetes manifests using Helm

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/kubernetes

Alternatively, deploy the generated Helm charts to your cluster using the Helm package manager.

```bash
# Or using Helm (if Helm charts were generated)
helm install my-orleans-app ./k8s-manifests/charts/my-orleans-app

```

--------------------------------

### Register Custom Placement Strategy

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

To use a custom placement strategy, register your implementation of PlacementStrategy as a singleton service during silo configuration.

```csharp
siloBuilder.Services.AddSingleton<PlacementStrategy, MyPlacementStrategy>();
```

--------------------------------

### Using Task.Factory.StartNew with async delegates in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

When using Task.Factory.StartNew with async delegates in Orleans to stay within the grain's single-threaded execution model, always unwrap the returned task to ensure it completes when the async delegate finishes.

```csharp
var task = Task.Factory.StartNew(SomeDelegateAsync).Unwrap();
```

--------------------------------

### Orleans with Grain Storage and Reminders

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configure the AppHost for an Orleans cluster that uses Redis for clustering, grain storage, pub/sub, and reminders. The silo project is also configured with references and replicas.

```csharp
public static void OrleansWithStorageAndReminders(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    var redis = builder.AddRedis("orleans-redis");

    var orleans = builder.AddOrleans("cluster")
        .WithClustering(redis)
        .WithGrainStorage("Default", redis)
        .WithGrainStorage("PubSubStore", redis)
        .WithReminders(redis);

    builder.AddProject<Projects.Silo>("silo")
        .WithReference(orleans)
        .WaitFor(redis)
        .WithReplicas(3);

    builder.Build().Run();
}
```

--------------------------------

### Configure Redis Reminders in Silo with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

This snippet for your Silo's Program.cs registers Redis as a keyed client and enables Orleans. Ensure service defaults and Orleans are added.

```csharp
public static void RemindersRedisSilo(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.AddServiceDefaults();
    builder.AddKeyedRedisClient("redis");
    builder.UseOrleans();

    builder.Build().Run();
}

```

--------------------------------

### Alternative Unreliable Client Deployment on Dedicated Servers

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

An alternative client configuration for connecting to dedicated servers using `ClientBuilder` and `UseStaticClustering`.

```csharp
var gateways = new IPEndPoint[]
{
    new IPEndPoint(PRIMARY_SILO_IP_ADDRESS, 30_000),
    new IPEndPoint(OTHER_SILO__IP_ADDRESS_1, 30_000),
    // ...
    new IPEndPoint(OTHER_SILO__IP_ADDRESS_N, 30_000),
};

var client = new ClientBuilder()
    .UseStaticClustering(gateways)
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .Build();


```

--------------------------------

### Enable Aspire Deploy Command (PowerShell)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Enables the preview 'aspire deploy' command by setting an environment variable in PowerShell. This is required before running deployment commands.

```powershell
$env:DOTNET_ASPIRE_ENABLE_DEPLOY_COMMAND="true"

```

--------------------------------

### Configure Client with Application Insights Telemetry Consumer

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

Adds the Application Insights telemetry consumer to the client configuration. Replace 'INSTRUMENTATION_KEY' with your actual Application Insights instrumentation key.

```csharp
var clientBuilder = new ClientBuilder();
clientBuilder.AddApplicationInsightsTelemetryConsumer("INSTRUMENTATION_KEY");

```

--------------------------------

### Authenticate to Azure Developer CLI

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Authenticate to the Azure Developer CLI to manage Azure resources. Follow the tool's prompts for authentication.

```bash
azd auth login

```

--------------------------------

### Disambiguating Grain Types with Resolved Grain ID

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

When using `IGrainFactory.GetGrain` overloads that accept `GrainId`, Orleans bypasses interface-to-grain type mapping, eliminating ambiguity. This example explicitly creates a `GrainId` for the 'up-counter' grain type.

```C#
public interface ICounterGrain : IGrainWithStringKey
{
    ValueTask<int> UpdateCount();
}

[GrainType("up-counter")]
public class UpCounterGrain : ICounterGrain
{
    private int _count;

    public ValueTask<string> UpdateCount() => new(++_count); // Increment count
}

[GrainType("down-counter")]
public class DownCounterGrain : ICounterGrain
{
    private int _count;

    public ValueTask<string> UpdateCount() => new(--_count); // Decrement count
}

```

```C#
// This will refer to an instance of UpCounterGrain, since "up-counter" was specified as the grain type
// and the UpCounterGrain uses [GrainType("up-counter")] to specify its grain type.
ICounterGrain myUpCounter = grainFactory.GetGrain<ICounterGrain>(GrainId.Create("up-counter", "my-counter"));

```

--------------------------------

### Implement Custom Storage Interface for Orleans Event Sourcing

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/log-consistency-providers

Implement this interface to define custom storage logic for Orleans event sourcing. Ensure ReadStateFromStorage returns the version and state, and ApplyUpdatesToStorage handles version mismatches and potential retries safely.

```csharp
public interface ICustomStorageInterface<StateType, EventType>
{
    Task<KeyValuePair<int, StateType>> ReadStateFromStorage();

    Task<bool> ApplyUpdatesToStorage(
        IReadOnlyList<EventType> updates,
        int expectedVersion);
}
```

--------------------------------

### Configure In-Memory Reminders in AppHost with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Use in-memory reminders for local development in your AppHost. This configuration bypasses the need for external storage like Redis.

```csharp
public static void RemindersInMemoryAppHost(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    var redis = builder.AddRedis("redis");

    var orleans = builder.AddOrleans("cluster")
        .WithClustering(redis)
        .WithMemoryReminders();

    builder.AddProject<Projects.Silo>("silo")
        .WithReference(orleans)
        .WaitFor(redis);

    builder.Build().Run();
}

```

--------------------------------

### Add Kubernetes environment to AppHost

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/kubernetes

Configure your AppHost to use a Kubernetes environment. This sets up the necessary integration for deploying to Kubernetes.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Add the Kubernetes environment
var k8s = builder.AddKubernetesEnvironment("k8s");

// Add your Orleans silo project
var silo = builder.AddProject<Projects.MySilo>("silo");

builder.Build().Run();

```

--------------------------------

### Silo Project: Production Configuration with Azure Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Sets up the Orleans silo host with service defaults and registers keyed Azure Table and Blob Storage clients. Ensure the appropriate `AddKeyed*` methods are called to register backing resources.

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedAzureTableServiceClient("clustering");
builder.AddKeyedAzureBlobServiceClient("grainstate");
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### Configure Silo Metadata Directly in Code

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/silo-metadata

Programmatically configure silo metadata by passing a Dictionary of key-value pairs to the `UseSiloMetadata` extension method. This allows for dynamic metadata configuration during silo initialization.

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.UseSiloMetadata(new Dictionary<string, string>
    {
        ["cloud.region"] = "us-east1",
        ["compute.reservation.type"] = "spot",
        ["role"] = "worker"
    });
});

```

--------------------------------

### Basic TestCluster Usage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

Demonstrates a basic test case using `TestCluster` to test a grain. Ensure the `Microsoft.Orleans.TestingHost` NuGet package is included.

```csharp
using Orleans.TestingHost;

namespace Tests;

public class HelloGrainTests
{
    [Fact]
    public async Task SaysHelloCorrectly()
    {
        var builder = new TestClusterBuilder();
        var cluster = builder.Build();
        cluster.Deploy();

        var hello = cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());
        var greeting = await hello.SayHello("World");

        cluster.StopAllSilos();

        Assert.Equal("Hello, World!", greeting);
    }
}
```

--------------------------------

### Configure Memory Streams on Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

Add memory-based stream provider for the client to enable stream functionality.

```csharp
client.AddMemoryStreams("StreamProvider");

```

--------------------------------

### Configure Client with Certificate File for TLS

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/transport-layer-security

Configure a client to use TLS with a certificate loaded from a PKCS12 file. This is useful when certificates are managed outside of the Windows certificate store.

```csharp
using var cert = X509CertificateLoader.LoadPkcs12FromFile("path/to/certificate.pfx", "password");

using IHost host = Host.CreateDefaultBuilder()
    .UseOrleansClient(builder =>
    {
        builder
            .UseLocalhostClustering()
            .UseTls(cert, options =>
            {
                options.OnAuthenticateAsClient = (connection, sslOptions) =>
                {
                    sslOptions.TargetHost = cert.GetNameInfo(X509NameType.DnsName, false) ?? "my-certificate-subject";
                };
            });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.RunAsync();

```

--------------------------------

### Configure Redis Grain Directory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/grain-directory

Configure the Redis grain directory implementation by providing a name and options, including the Redis configuration.

```csharp
siloBuilder.AddRedisGrainDirectory(
    "my-grain-directory",
    options => options.ConfigurationOptions = redisConfiguration);

```

--------------------------------

### GitHub Actions Workflow for Azure Deployment

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

This YAML workflow automates the build, push, and deployment of a .NET Orleans application to Azure Container Apps.

```yaml
name: Deploy to Azure Container Apps

on:
  push:
    branches:
    - main

env:
  UNIQUE_APP_NAME: orleanscart
  SILO_IMAGE_NAME: orleanscart-silo
  AZURE_RESOURCE_GROUP_NAME: orleans-resourcegroup
  AZURE_RESOURCE_GROUP_LOCATION: eastus

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3

    - name: Setup .NET 6.0
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 6.0.x

    - name: .NET publish shopping cart app
      run: dotnet publish ./Silo/Orleans.ShoppingCart.Silo.csproj --configuration Release

    - name: Login to Azure
      uses: azure/login@v1
      with:
        creds: ${{ secrets.AZURE_CREDENTIALS }}

    - name: Flex ACR Bicep
      run: |
        az deployment group create \
          --resource-group ${{ env.AZURE_RESOURCE_GROUP_NAME }} \
          --template-file '.github/workflows/flex/acr.bicep' \
          --parameters location=${{ env.AZURE_RESOURCE_GROUP_LOCATION }}

    - name: Get ACR Login Server
      run: |
        ACR_NAME=$(az deployment group show -g ${{ env.AZURE_RESOURCE_GROUP_NAME }} -n acr \
        --query properties.outputs.acrName.value | tr -d '"')
        echo "ACR_NAME=$ACR_NAME" >> $GITHUB_ENV
        ACR_LOGIN_SERVER=$(az deployment group show -g ${{ env.AZURE_RESOURCE_GROUP_NAME }} -n acr \
        --query properties.outputs.acrLoginServer.value | tr -d '"')
        echo "ACR_LOGIN_SERVER=$ACR_LOGIN_SERVER" >> $GITHUB_ENV

    - name: Prepare Docker buildx
      uses: docker/setup-buildx-action@v1

    - name: Login to ACR
      run: |
        access_token=$(az account get-access-token --query accessToken -o tsv)
        refresh_token=$(curl https://${{ env.ACR_LOGIN_SERVER }}/oauth2/exchange -v \
        -d "grant_type=access_token&service=${{ env.ACR_LOGIN_SERVER }}&access_token=$access_token" | jq -r .refresh_token)
        # The null GUID 0000... tells the container registry that this is an ACR refresh token during the login flow
        docker login -u 00000000-0000-0000-0000-000000000000 \
        --password-stdin ${{ env.ACR_LOGIN_SERVER }} <<< "$refresh_token"

    - name: Build and push Silo image to registry
      uses: docker/build-push-action@v2
      with:
        push: true
        tags: ${{ env.ACR_LOGIN_SERVER }}/${{ env.SILO_IMAGE_NAME }}:${{ github.sha }}
        file: Silo/Dockerfile

    - name: Flex ACA Bicep
      run: |
        az deployment group create \
          --resource-group ${{ env.AZURE_RESOURCE_GROUP_NAME }} \
          --template-file '.github/workflows/flex/main.bicep' \
          --parameters location=${{ env.AZURE_RESOURCE_GROUP_LOCATION }} \
            appName=${{ env.UNIQUE_APP_NAME }} \
            acrName=${{ env.ACR_NAME }} \
            repositoryImage=${{ env.ACR_LOGIN_SERVER }}/${{ env.SILO_IMAGE_NAME }}:${{ github.sha }} \
          --debug

    - name: Get Container App URL
      run: |
        ACA_URL=$(az deployment group show -g ${{ env.AZURE_RESOURCE_GROUP_NAME }} \
        -n main --query properties.outputs.acaUrl.value | tr -d '"')
        echo $ACA_URL

    - name: Logout of Azure
      run: az logout
```

--------------------------------

### Client Call Flow with AlwaysInterleave

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-scheduling

Shows a client call flow demonstrating the performance difference between non-interleaved and always-interleaved methods. Calls to GoSlow take sequential time, while GoFast calls execute concurrently.

```csharp
var slowpoke = client.GetGrain<ISlowpokeGrain>(0);

// A. This will take around 20 seconds.
await Task.WhenAll(slowpoke.GoSlow(), slowpoke.GoSlow());

// B. This will take around 10 seconds.
await Task.WhenAll(slowpoke.GoFast(), slowpoke.GoFast(), slowpoke.GoFast());

```

--------------------------------

### Configure Orleans Client with Host Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/overview-helloworld

Configures the Orleans client using IClientBuilder within a host builder. It sets up local clustering and defines cluster and service IDs to match the silo.

```csharp
public static async Task ClientMain(string[] args)
{
    using IHost host = Host.CreateDefaultBuilder(args)
        .UseOrleansClient(clientBuilder =>
        {
            clientBuilder.UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "HelloWorldApp";
                });
        })
        .ConfigureLogging(logging => logging.AddConsole())
        .Build();

    await host.StartAsync();

    var client = host.Services.GetRequiredService<IClusterClient>();
    Console.WriteLine("Client successfully connected to silo host");

    await DoClientWork(client);

    await host.StopAsync();
}

```

--------------------------------

### Custom Placement Strategy Classes

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

These C# classes define a custom placement strategy and an attribute to apply it to grain classes. The SamplePlacementStrategy class represents the strategy itself, while SamplePlacementStrategyAttribute allows easy assignment.

```csharp
[Serializable]
public sealed class SamplePlacementStrategy : PlacementStrategy
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class SamplePlacementStrategyAttribute : PlacementAttribute
{
    public SamplePlacementStrategyAttribute() :
        base(new SamplePlacementStrategy())
    {
    }
}
```

--------------------------------

### Specify Log Storage and Azure Blob Storage Providers

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/event-sourcing-configuration

Configure a grain to use a specific log-consistency provider (e.g., 'LogStorage') and a separate storage provider (e.g., 'AzureBlobStorage') for state persistence using `LogConsistencyProviderAttribute` and `StorageProviderAttribute`.

```csharp
[LogConsistencyProvider(ProviderName = "LogStorage")]
[StorageProvider(ProviderName = "AzureBlobStorage")]
public class ChatGrain :
    JournaledGrain<XDocument, IChatEvent>, IChatGrain
{
    // ...
}
```

--------------------------------

### Configure Orleans Logging to Info Level (Programmatic)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/troubleshooting-azure-cloud-services-deployments

Set the default trace level to 'Info' for both ClusterConfiguration and ClientConfiguration objects when creating them programmatically. This ensures informational logs are captured.

```csharp
var config = new ClusterConfiguration();
config.Defaults.DefaultTraceLevel = Severity.Info;

var clientConfig = new ClientConfiguration();
clientConfig.DefaultTraceLevel = Severity.Info;
```

--------------------------------

### Implement OnNextAsync for Implicit Subscriptions

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Implement this method to process incoming stream data for implicit subscriptions. It receives the item and an optional sequence token.

```csharp
public Task OnNextAsync(string item, StreamSequenceToken? token = null)
{
    _logger.LogInformation("Received an item from the stream: {Item}", item);
    return Task.CompletedTask;
}
```

--------------------------------

### Configure external serializers in GlobalConfiguration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-configuration

Specify external serializer providers implementing IExternalSerializer using the SerializationProviderOptions.SerializationProviders property in GlobalConfiguration. Ensure serialization configuration is identical on all clients and silos.

```csharp
// Global configuration
var globalConfiguration = new GlobalConfiguration();
globalConfiguration.SerializationProviders.Add(
    typeof(FantasticSerializer).GetTypeInfo());
```

--------------------------------

### Redeploy Application to Azure

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Use the Azure Developer CLI to redeploy the application code as a Docker container to Azure.

```bash
azd deploy
```

--------------------------------

### AdoNetGrainStorageOptions Class Definition (JSON Format)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/relational-storage

Provides definitions for AdoNetGrainStorageOptions, highlighting properties like ConnectionString, InitStage, Invariant, and UseJsonFormat for JSON serialization.

```csharp
/// <summary>
/// Options for AdoNetGrainStorage
/// </summary>
public class AdoNetGrainStorageOptions
{
    /// <summary>
    /// Define the property of the connection string
    /// for AdoNet storage.
    /// </summary>
    [Redact]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Set the stage of the silo lifecycle where storage should
    /// be initialized.  Storage must be initialized prior to use.
    /// </summary>
    public int InitStage { get; set; } = DEFAULT_INIT_STAGE;
    /// <summary>
    /// Default init stage in silo lifecycle.
    /// </summary>
    public const int DEFAULT_INIT_STAGE =
        ServiceLifecycleStage.ApplicationServices;

    /// <summary>
    /// The default ADO.NET invariant will be used for
    /// storage if none is given.
    /// </summary>
    public const string DEFAULT_ADONET_INVARIANT =
        AdoNetInvariants.InvariantNameSqlServer;

    /// <summary>
    /// Define the invariant name for storage.
    /// </summary>
    public string Invariant { get; set; } =
        DEFAULT_ADONET_INVARIANT;

    /// <summary>
    /// Determine whether the storage string payload should be formatted in JSON.
    /// <remarks>If neither <see cref="UseJsonFormat"/> nor <see cref="UseXmlFormat"/> is set to true, then BinaryFormatSerializer will be configured to format the storage string payload.</remarks>
    /// </summary>

```

--------------------------------

### Define ZipPublishOutput Target in .NET Project

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

This XML target in the .NET project file defines a build step that runs after 'Publish'. It deletes an existing silo.zip file and then creates a new one by zipping the contents of the publish directory.

```xml
<Target Name="ZipPublishOutput" AfterTargets="Publish">
    <Delete Files="$(ProjectDir)\..\silo.zip" />
    <ZipDirectory SourceDirectory="$(PublishDir)" DestinationFile="$(ProjectDir)\..\silo.zip" />
</Target>
```

--------------------------------

### Configure Cassandra Clustering with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Configure Orleans to use Cassandra for clustering by providing a connection string and keyspace.

```csharp
using Orleans.Clustering.Cassandra.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.UseOrleans(siloBuilder =>
{
    siloBuilder.UseCassandraClustering(
        connectionString: "Contact Points=localhost;Port=9042",
        keyspace: "orleans");
});

```

--------------------------------

### Main Bicep Template for Container App Deployment

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

This template orchestrates the deployment of various modules, including the container app environment, storage, and the container app itself. It references an existing ACR and configures environment variables for the application.

```bicep
param appName string
param acrName string
param repositoryImage string
param location string = resourceGroup().location

resource acr 'Microsoft.ContainerRegistry/registries@2021-09-01' existing = {
  name: acrName
}

module env 'environment.bicep' = {
  name: 'containerAppEnvironment'
  params: {
    location: location
    operationalInsightsName: '${appName}-logs'
    appInsightsName: '${appName}-insights'
  }
}

var envVars = [
  {
    name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
    value: env.outputs.appInsightsInstrumentationKey
  }
  {
    name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
    value: env.outputs.appInsightsConnectionString
  }
  {
    name: 'ORLEANS_AZURE_STORAGE_CONNECTION_STRING'
    value: storageModule.outputs.connectionString
  }
  {
    name: 'ASPNETCORE_FORWARDEDHEADERS_ENABLED'
    value: 'true'
  }
]

module storageModule 'storage.bicep' = {
  name: 'orleansStorageModule'
  params: {
    name: '${appName}storage'
    location: location
  }
}

module siloModule 'container-app.bicep' = {
  name: 'orleansSiloModule'
  params: {
    appName: appName
    location: location
    containerAppEnvironmentId: env.outputs.id
    repositoryImage: repositoryImage
    registry: acr.properties.loginServer
    registryPassword: acr.listCredentials().passwords[0].value
    registryUsername: acr.listCredentials().username
    envVars: envVars
  }
}

output acaUrl string = siloModule.outputs.acaUrl

```

--------------------------------

### Implement IGrainStateAccessor<T> Extension

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-extensions

Implement the IGrainStateAccessor<T> interface to create a reusable extension for managing grain state. The constructor accepts getter and setter delegates for state access.

```csharp
public sealed class GrainStateAccessor<T> : IGrainStateAccessor<T>
{
    private readonly Func<T> _getter;
    private readonly Action<T> _setter;

    public GrainStateAccessor(Func<T> getter, Action<T> setter)
    {
        _getter = getter;
        _setter = setter;
    }

    public Task<T> GetState()
    {
        return Task.FromResult(_getter.Invoke());
    }

    public Task SetState(T state)
    {
        _setter.Invoke(state);
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Configure Client with Custom TelemetryConfiguration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

Configures the client to use a custom TelemetryConfiguration, enabling advanced telemetry processing and sinking options.

```csharp
var clientBuilder = new ClientBuilder();
var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
clientBuilder.AddApplicationInsightsTelemetryConsumer(telemetryConfiguration);

```

--------------------------------

### Register IGrainStorage and ILifecycleParticipant

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Registers two keyed singleton services for IGrainStorage and ILifecycleParticipant<ISiloLifecycle> to implement custom grain storage.

```csharp
services.AddKeyedSingleton<IGrainStorage>(
    providerName,
    (sp, key) => FileGrainStorageFactory.Create(sp, key?.ToString() ?? providerName));

services.AddKeyedSingleton<ILifecycleParticipant<ISiloLifecycle>>(
    providerName,
    (sp, key) => (ILifecycleParticipant<ISiloLifecycle>)sp.GetRequiredKeyedService<IGrainStorage>(key));
```

--------------------------------

### Implementing State Transitions with Apply Methods

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/journaledgrain-basics

Define `Apply` methods within your `GrainState` class to specify how the state should be updated in response to different event types. The runtime selects the most appropriate `Apply` overload based on the event's runtime type.

```csharp
class GrainState
{
    Apply(E1 @event)
    {
        // code that updates the state
    }

    Apply(E2 @event)
    {
        // code that updates the state
    }
}
```

--------------------------------

### Configure Grain Collection Settings in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/activation-collection

Configure global and class-specific activation collection ages using GrainCollectionOptions. Set the default CollectionAge for all grains and override it for specific grain implementations.

```csharp
siloBuilder.Configure<GrainCollectionOptions>(options =>
{
    // Set the value of CollectionAge to 10 minutes for all grain
    options.CollectionAge = TimeSpan.FromMinutes(10);

    // Override the value of CollectionAge to 5 minutes for MyGrainImplementation
    options.ClassSpecificCollectionAge[typeof(MyGrainImplementation).FullName] =
        TimeSpan.FromMinutes(5);
});
```

```csharp
mySiloHostBuilder.Configure<GrainCollectionOptions>(options =>
{
    // Set the value of CollectionAge to 10 minutes for all grain
    options.CollectionAge = TimeSpan.FromMinutes(10);

    // Override the value of CollectionAge to 5 minutes for MyGrainImplementation
    options.ClassSpecificCollectionAge[typeof(MyGrainImplementation).FullName] =
        TimeSpan.FromMinutes(5);
});
```

--------------------------------

### Implement IGrainMigrationParticipant Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Define the IGrainMigrationParticipant interface for grains that need custom state preservation during migration.

```csharp
public interface IGrainMigrationParticipant
{
    void OnDehydrate(IDehydrationContext dehydrationContext);
    void OnRehydrate(IRehydrationContext rehydrationContext);
}
```

--------------------------------

### Monitor Orleans Counters with dotnet-counters

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Use the dotnet-counters tool to monitor Orleans performance metrics. Ensure the 'Microsoft.Orleans' namespace is specified.

```bash
dotnet counters monitor -n MyApp --counters Microsoft.Orleans
```

--------------------------------

### Unreliable Client Deployment on Dedicated Servers

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure clients to connect to a cluster of dedicated servers for testing. This uses static clustering with a list of gateway endpoints.

```csharp
var gateways = new IPEndPoint[]
{
    new IPEndPoint(PRIMARY_SILO_IP_ADDRESS, 30_000),
    new IPEndPoint(OTHER_SILO__IP_ADDRESS_1, 30_000),
    // ...
    new IPEndPoint(OTHER_SILO__IP_ADDRESS_N, 30_000),
};

var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.UseStaticClustering(gateways)
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "Cluster42";
            options.ServiceId = "MyAwesomeService";
        });
});

using var host = builder.Build();
await host.StartAsync();

```

--------------------------------

### Implement Observer Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/observers

Implement the observer interface on the client to handle incoming messages. This class will be instantiated on the client and passed to the grain.

```csharp
public class Chat : IChat
{
    public Task ReceiveMessage(string message)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Configure Redis Clustering with Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Use the `UseRedisClustering` extension method to configure Redis as the clustering provider. Specify connection details and other options via `ConfigurationOptions`.

```csharp
using StackExchange.Redis;

var builder = Host.CreateApplicationBuilder(args);

builder.UseOrleans(siloBuilder =>
{
    siloBuilder.UseRedisClustering(options =>
    {
        options.ConfigurationOptions = new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" },
            AbortOnConnectFail = false
        };
    });
});

```

--------------------------------

### Configure GC in app.config (.NET Framework)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/configuring-garbage-collection

Configure server and concurrent garbage collection for .NET Framework applications by adding these elements to your app.config file. This method is applicable for SDK-style projects compiling against the full .NET Framework.

```xml
<configuration>
    <runtime>
        <gcServer enabled="true"/>
        <gcConcurrent enabled="true"/>
    </runtime>
</configuration>
```

--------------------------------

### Define URL Shortener Endpoints in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Sets up HTTP endpoints for the URL shortener application. The `/shorten` endpoint creates and stores shortened URLs, while the `/go/{shortenedRouteSegment}` endpoint handles redirection.

```csharp
app.MapGet("/", static () => "Welcome to the URL shortener, powered by Orleans!");

app.MapGet("/shorten",
    static async (IGrainFactory grains, HttpRequest request, string url) =>
    {
        var host = $"{request.Scheme}://{request.Host.Value}";

        // Validate the URL query string.
        if (string.IsNullOrWhiteSpace(url) ||
            Uri.IsWellFormedUriString(url, UriKind.Absolute) is false)
        {
            return Results.BadRequest($"""
                The URL query string is required and needs to be well formed.
                Consider, ${host}/shorten?url=https://www.microsoft.com.
                """);
        }

        // Create a unique, short ID
        var shortenedRouteSegment = Guid.NewGuid().GetHashCode().ToString("X");

        // Create and persist a grain with the shortened ID and full URL
        var shortenerGrain =
            grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);

        await shortenerGrain.SetUrl(url);

        // Return the shortened URL for later use
        var resultBuilder = new UriBuilder(host)
        {
            Path = $"/go/{shortenedRouteSegment}"
        };

        return Results.Ok(resultBuilder.Uri);
    });

app.MapGet("/go/{shortenedRouteSegment:required}",
    static async (IGrainFactory grains, string shortenedRouteSegment) =>
    {
        // Retrieve the grain using the shortened ID and url to the original URL
        var shortenerGrain =
            grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);

        var url = await shortenerGrain.GetUrl();

        // Handles missing schemes, defaults to "http://".
        var redirectBuilder = new UriBuilder(url);

        return Results.Redirect(redirectBuilder.Uri.ToString());
    });

app.Run();

```

--------------------------------

### Client Configuration with ADO.NET Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure an Orleans client to use SQL Server for cluster membership with ADO.NET. Specify the correct invariant based on the Orleans version.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";

var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAdoNetClustering(options =>
    {
        options.ConnectionString = connectionString;
        // Use "Microsoft.Data.SqlClient" for Orleans 10.0+
        // Use "System.Data.SqlClient" for Orleans 7.0-9.x
        options.Invariant = "Microsoft.Data.SqlClient";
    });
});

using var host = builder.Build();

```

--------------------------------

### Configure Orleans with Redis using .NET Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

This code configures .NET Aspire to use Redis for Orleans clustering and grain storage. It assumes Redis is already added as a resource. Ensure the Orleans silo project is referenced and configured with a reference to the Orleans service.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Add Redis for clustering and grain storage
var redis = builder.AddRedis("redis");

// Configure Orleans to use Redis
var orleans = builder.AddOrleans("cluster")
    .WithClustering(redis)
    .WithGrainStorage("urls", redis);

// Add the Orleans silo project
builder.AddProject<Projects.OrleansURLShortener>("silo")
    .WithReference(orleans)
    .WithReference(redis);

builder.Build().Run();

```

--------------------------------

### Resume Explicit Subscriptions with Delegate (Alternative)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

An alternative way to resume explicit subscriptions in OnActivateAsync. It retrieves all subscription handles and resumes them using a provided OnNextAsync delegate.

```csharp
public async override Task OnActivateAsync()
{
    var streamProvider = this.GetStreamProvider(PROVIDER_NAME);
    var stream = 
        streamProvider.GetStream<string>(this.GetPrimaryKey(), "MyStreamNamespace");

    var subscriptionHandles = await stream.GetAllSubscriptionHandles();
    if (!subscriptionHandles.IsNullOrEmpty())
    {
        subscriptionHandles.ForEach(
            async x => await x.ResumeAsync(OnNextAsync));
    }
}
```

--------------------------------

### Configure Default Log Consistency and Storage Providers

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/event-sourcing-configuration

Use the special name 'Default' for log consistency and storage providers in configuration to set them as defaults. This allows omitting the LogConsistencyProvider and StorageProviderAttribute attributes in grain classes.

```xml
<LogConsistencyProviders>
    <Provider Name="Default"
        Type="Orleans.EventSourcing.LogStorage.LogConsistencyProvider"/>
</LogConsistencyProviders>
<StorageProviders>
    <Provider Name="Default"
        Type="Orleans.Storage.MemoryStorage" />
</StorageProviders>

```

--------------------------------

### Helper Method for Azure Storage Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/client-configuration

A static helper method to configure Azure Table storage clustering for an IClientBuilder using a provided connection string.

```csharp
public static void ConfigureAzureClustering(IClientBuilder clientBuilder, string connectionString)
{
    clientBuilder.UseAzureStorageClustering(
        options => options.ConfigureTableServiceClient(connectionString));
}
```

--------------------------------

### Configure Orleans with Code Generation and Logging

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/code-generation

Enables logging during code generation by passing an ILoggerFactory instance to the WithCodeGeneration method.

```csharp
public static void ConfigureWithCodeGenerationLogging(ISiloHostBuilder builder)
{
    ILoggerFactory codeGenLoggerFactory = new LoggerFactory();
    codeGenLoggerFactory.AddProvider(new ConsoleLoggerProvider());
    builder.ConfigureApplicationParts(
        parts => parts
            .AddApplicationPart(typeof(IRuntimeCodeGenGrain).Assembly)
            .WithCodeGeneration(codeGenLoggerFactory));
}
```

--------------------------------

### Configure Orleans with External Assembly Code Generation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/code-generation

Adds a generated assembly to the client/silo during initialization by loading the assembly and configuring application parts.

```csharp
public static void ConfigureWithExternalAssembly(ISiloHostBuilder builder)
{
    var assembly = Assembly.Load("CodeGenAssembly");
    builder.ConfigureApplicationParts(
        parts => parts.AddApplicationPart(assembly));
}
```

--------------------------------

### Configure external serializers in ClientConfiguration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-configuration

Specify external serializer providers implementing IExternalSerializer using the SerializationProviderOptions.SerializationProviders property in ClientConfiguration. Ensure serialization configuration is identical on all clients and silos.

```csharp
// Client configuration
var clientConfiguration = new ClientConfiguration();
clientConfiguration.SerializationProviders.Add(
    typeof(FantasticSerializer).GetTypeInfo());
```

--------------------------------

### Configure Custom Grain Storage Serializer

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/relational-storage

Register a custom serializer in the DI container and configure the storage provider to use it. Ensure your custom serializer implements IGrainStorageSerializer.

```csharp
// Register your custom serializer in DI
siloBuilder.Services.AddSingleton<IGrainStorageSerializer, MyCustomSerializer>();

// Configure the storage provider to use your serializer
siloBuilder.AddAdoNetGrainStorage(
    "OrleansStorage",
    (OptionsBuilder<AdoNetGrainStorageOptions> optionsBuilder) =>
    {
        optionsBuilder.Configure<IGrainStorageSerializer>(
            (options, serializer) =>
            {
                options.Invariant = "<Invariant>";
                options.ConnectionString = "<ConnectionString>";
                options.GrainStorageSerializer = serializer;
            });
    });

```

--------------------------------

### Configure Redis Clustering with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

An alternative way to configure Redis clustering is by providing a connection string directly to the `UseRedisClustering` method.

```csharp
siloBuilder.UseRedisClustering("localhost:6379");

```

--------------------------------

### Configure Memory Streams on Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

Add memory-based stream provider and in-memory grain storage for local development and testing on the silo.

```csharp
silo.AddMemoryStreams("StreamProvider")
    .AddMemoryGrainStorage("PubSubStore");

```

--------------------------------

### Separate Silo and Client Projects in Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configure an Aspire AppHost to define separate silo and client projects, referencing the Orleans cluster and using `.AsClient()` for the client-only reference.

```csharp
public static void SeparateSiloAndClient(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    var redis = builder.AddRedis("orleans-redis");

    var orleans = builder.AddOrleans("cluster")
        .WithClustering(redis)
        .WithGrainStorage("Default", redis);

    // Backend Orleans silo cluster
    var silo = builder.AddProject<Projects.Silo>("backend")
        .WithReference(orleans)
        .WaitFor(redis)
        .WithReplicas(5);

    // Frontend web project as Orleans client
    builder.AddProject<Projects.Client>("frontend")
        .WithReference(orleans.AsClient())  // Client-only reference
        .WaitFor(silo);

    builder.Build().Run();
}

```

--------------------------------

### Configure Application Parts for Orleans Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Use this method to configure application parts for an Orleans silo. This helps Orleans load user assemblies and discover Grains, Grain Interfaces, and Serializers. Although optional, explicit configuration is encouraged.

```csharp
public static void ConfigureApplicationParts(ISiloHostBuilder siloBuilder)
{
    siloBuilder.ConfigureApplicationParts(
        parts => parts.AddApplicationPart(
            typeof(ValueGrain).Assembly) 
            .WithReferences());
}
```

--------------------------------

### Configure Silo with Host Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/overview-helloworld

Configures the Orleans silo using the default host builder. This is suitable for local development with localhost clustering and console logging.

```csharp
public static async Task SiloMain(string[] args)
{
    await Host.CreateDefaultBuilder(args)
        .UseOrleans(siloBuilder =>
        {
            siloBuilder.UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "HelloWorldApp";
                })
                .Configure<EndpointOptions>(
                    options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureLogging(logging => logging.AddConsole());
        })
        .RunConsoleAsync();
}
```

--------------------------------

### Configure Multiple Stream Providers with Azure Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Sets up memory streams, Azure Queue streams with managed identity, and Azure Table storage for Pub-Sub. This configuration enables different types of stream handling and persistent storage.

```csharp
var tableEndpoint = new Uri(configuration["AZURE_TABLE_STORAGE_ENDPOINT"]!);
var queueEndpoint = new Uri(configuration["AZURE_QUEUE_STORAGE_ENDPOINT"]!);
var credential = new DefaultAzureCredential();

hostBuilder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddMemoryStreams("StreamProvider")
        .AddAzureQueueStreams("AzureQueueProvider",
            optionsBuilder => optionsBuilder.ConfigureAzureQueue(
                options => options.Configure(
                    opt => opt.QueueServiceClient = new QueueServiceClient(queueEndpoint, credential))))
        .AddAzureTableGrainStorage("PubSubStore",
            options => options.TableServiceClient = new TableServiceClient(tableEndpoint, credential));
});

```

--------------------------------

### Define a custom placement filter strategy

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Create a strategy derived from PlacementFilterStrategy to manage configuration values for the placement filter. This strategy includes an optional order parameter and a default constructor.

```csharp
public class ExamplePreferLocalPlacementFilterStrategy(int order)
    : PlacementFilterStrategy(order)
    {
        public ExamplePreferLocalPlacementFilterStrategy() : this(0) { }
    }

```

--------------------------------

### Configure Orleans to use System.Text.Json

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-configuration

Use this to configure Orleans to serialize specific types using System.Text.Json. Ensure the Microsoft.Orleans.Serialization.SystemTextJson NuGet package is referenced.

```csharp
siloBuilder.Services.AddSerializer(serializerBuilder =>
{
    serializerBuilder.AddJsonSerializer(
        isSupported: type => type.Namespace.StartsWith("Example.Namespace"));
});
```

--------------------------------

### Configure ADO.NET Grain Directory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/grain-directory

Configure the ADO.NET grain directory as the default or a named directory, specifying the invariant and connection string.

```csharp
// Use as the default grain directory
builder.UseAdoNetGrainDirectoryAsDefault(options =>
{
    options.Invariant = "System.Data.SqlClient"; // or "Npgsql", "MySql.Data.MySqlClient"
    options.ConnectionString = "Server=localhost;Database=Orleans;..." ;
});

// Or add as a named directory
builder.AddAdoNetGrainDirectory("MyAdoNetDirectory", options =>
{
    options.Invariant = "Npgsql";
    options.ConnectionString = "Host=localhost;Database=Orleans;..." ;
});

```

--------------------------------

### ClusterFixture with Custom Silo Configuration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

Shows how to configure silos within a `TestCluster` using `ISiloConfigurator`. This is useful for registering custom services or applying specific configurations to the test environment.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Orleans.TestingHost;

namespace Tests;

public sealed class ClusterFixtureWithConfig : IDisposable
{
    public TestCluster Cluster { get; }

    public ClusterFixtureWithConfig()
    {
        Cluster = new TestClusterBuilder()
            .AddSiloBuilderConfigurator<TestSiloConfigurations>()
            .Build();
        Cluster.Deploy();
    }

    void IDisposable.Dispose() => Cluster.StopAllSilos();
}

file sealed class TestSiloConfigurations : ISiloConfigurator
{
    public void Configure(ISiloBuilder siloBuilder)
    {
        // TODO: Call required service registrations here.
        // siloBuilder.Services.AddSingleton<T, Impl>(/* ... */);
    }
}
```

--------------------------------

### Configure Orleans Client with Cosmos DB Gateway Discovery

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Use `UseCosmosGatewayListProvider` to configure gateway discovery for Orleans clients when using Azure Cosmos DB. Ensure you have the necessary Azure credentials configured.

```csharp
builder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.UseCosmosGatewayListProvider(options =>
    {
        options.ConfigureCosmosClient(
            "https://myaccount.documents.azure.com:443/",
            new DefaultAzureCredential());
    });
});

```

--------------------------------

### Obtain IClusterClient from ISiloHost

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

When co-hosting client code in the same process as grain code, you can obtain an IClusterClient instance from the ISiloHost's dependency injection container. This client can then be used to interact with grains.

```csharp
var client = host.Services.GetService<IClusterClient>();
await client.GetGrain<IMyGrain>(0).Ping();

```

--------------------------------

### Revert to Random Placement Strategy

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

To revert to the previous random placement behavior, register RandomPlacement as a singleton service during silo configuration.

```csharp
siloBuilder.Services.AddSingleton<PlacementStrategy, RandomPlacement>();
```

--------------------------------

### Configure Azure Table Storage Clustering with .NET Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Configure Azure Table Storage for Orleans clustering within a .NET Aspire AppHost project. Use `RunAsEmulator()` for local development with Azurite.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator();  // Use Azurite for local development
var tables = storage.AddTables("clustering");

var orleans = builder.AddOrleans("cluster")
    .WithClustering(tables);

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans)
    .WaitFor(storage);

builder.Build().Run();

```

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedAzureTableServiceClient("clustering");
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### Configure Redis Grain Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

This C# code demonstrates how to configure a named Redis grain storage provider using `AddRedisGrainStorage`. It specifies connection details and options for the Redis client.

```csharp
var builder = Host.CreateApplicationBuilder();
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddRedisGrainStorage(
        name: "redis",
        configureOptions: options =>
        {
            options.ConfigurationOptions = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                AbortOnConnectFail = false
            };
        });
});

using var host = builder.Build();

```

--------------------------------

### Implement Observer with Cancellation Support

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/observers

Implement the observer by checking for cancellation before processing data and using the cancellation token with asynchronous operations. This ensures that observer operations can be stopped gracefully.

```csharp
public class DataObserver : IDataObserver
{
    public async Task OnDataReceivedAsync(DataPayload data, CancellationToken cancellationToken = default)
    {
        // Check cancellation before processing
        cancellationToken.ThrowIfCancellationRequested();

        // Process data with cancellation-aware operations
        await ProcessDataAsync(data, cancellationToken);
    }

    private async Task ProcessDataAsync(DataPayload data, CancellationToken cancellationToken)
    {
        // Use cancellation token with async operations
        await Task.Delay(100, cancellationToken);
        Console.WriteLine($"Processed: {data.Id}");
    }
}
```

--------------------------------

### Configure Orleans for Client-Coordinated Transactions

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Enable the necessary services for client-coordinated transactions by calling UseTransactions() on the client builder. This must be done during application configuration.

```csharp
clientBuilder.UseTransactions();
```

--------------------------------

### Configure Azure Cosmos DB Clustering with .NET Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Set up Azure Cosmos DB for Orleans clustering in a .NET Aspire AppHost. The `RunAsEmulator()` method is available for local testing.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var cosmos = builder.AddAzureCosmosDB("cosmos")
    .RunAsEmulator();  // Use emulator for local development
var database = cosmos.AddCosmosDatabase("orleans");

var orleans = builder.AddOrleans("cluster")
    .WithClustering(database);

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans)
    .WaitFor(cosmos);

builder.Build().Run();

```

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedAzureCosmosClient("cosmos");
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### Configure Azure Table Grain Directory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/grain-directory

Configure the Azure Table grain directory by providing a name and connection string.

```csharp
siloBuilder.AddAzureTableGrainDirectory(
    "my-grain-directory",
    options => options.ConnectionString = azureConnectionString);

```

--------------------------------

### Configure Silo project with .NET Aspire and Redis

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

This code configures the Silo project to integrate with .NET Aspire and use Redis. It enables service defaults, adds a keyed Redis client, and configures Orleans. The Orleans configuration is automatically picked up from environment variables injected by the AppHost.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedRedisClient("redis");
builder.UseOrleans();

var app = builder.Build();
// ... rest of app configuration

```

--------------------------------

### Register Timer and Reminder with POCO Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Implement IGrainBase and inject ITimerRegistry and IReminderRegistry to register timers and reminders. The timer is set to fire after 3 seconds and then every 10 seconds. The reminder is registered or updated when the Ping method is called, firing every hour immediately.

```csharp
using Orleans.Timers;

namespace Timers;

public sealed class PingGrain : IGrainBase, IPingGrain, IDisposable
{
    private const string ReminderName = "ExampleReminder";

    private readonly IReminderRegistry _reminderRegistry;

    private IGrainReminder? _reminder;

    public  IGrainContext GrainContext { get; }

    public PingGrain(
        ITimerRegistry timerRegistry,
        IReminderRegistry reminderRegistry,
        IGrainContext grainContext)
    {
        // Register timer
        timerRegistry.RegisterGrainTimer(
            grainContext,
            callback: static async (state, cancellationToken) =>
            {
                // Omitted for brevity...
                // Use state

                await Task.CompletedTask;
            },
            state: this,
            options: new GrainTimerCreationOptions
            {
                DueTime = TimeSpan.FromSeconds(3),
                Period = TimeSpan.FromSeconds(10)
            });

        _reminderRegistry = reminderRegistry;

        GrainContext = grainContext;
    }

    public async Task Ping()
    {
        _reminder = await _reminderRegistry.RegisterOrUpdateReminder(
            callingGrainId: GrainContext.GrainId,
            reminderName: ReminderName,
            dueTime: TimeSpan.Zero,
            period: TimeSpan.FromHours(1));
    }

    void IDisposable.Dispose()
    {
        if (_reminder is not null)
        {
            _reminderRegistry.UnregisterReminder(
                GrainContext.GrainId, _reminder);
        }
    }
}
```

--------------------------------

### Configure Grain Storage Serializer with Azure Blob Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Demonstrates how to configure a custom serializer for Azure Blob Storage grain persistence using OptionsBuilder. This replaces the default Newtonsoft.Json serializer.

```csharp
public static void ConfigureGrainStorageSerializer(ISiloBuilder siloBuilder)
{
    siloBuilder.AddAzureBlobGrainStorage(
        "MyGrainStorage",
        (OptionsBuilder<AzureBlobStorageOptions> optionsBuilder) =>
        {
            optionsBuilder.Configure<MyCustomSerializer>(
                (options, serializer) => options.GrainStorageSerializer = serializer);
        });
}
```

--------------------------------

### Component Participation in Grain Lifecycle (IGrainContext)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

A component can participate in a grain's lifecycle by accessing the `IGrainContext.ObservableLifecycle`. The `Create` factory method ensures the component is properly subscribed before the grain is fully constructed.

```csharp
public class MyComponent : ILifecycleParticipant<IGrainLifecycle>
{
    public static MyComponent Create(IGrainContext context)
    {
        var component = new MyComponent();
        component.Participate(context.ObservableLifecycle);
        return component;
    }

    public void Participate(IGrainLifecycle lifecycle)
    {
        lifecycle.Subscribe<MyComponent>(GrainLifecycleStage.Activate, OnActivate);
    }

    private Task OnActivate(CancellationToken ct)
    {
        // Do stuff
    }
}
```

--------------------------------

### Configure Advanced Endpoint Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Customize endpoint configuration by explicitly setting silo and gateway ports, the advertised IP address for cluster membership, and specific listening endpoints for gateway and silo sockets. This allows for fine-grained control over network communication.

```csharp
public static void ConfigureEndpointOptions(ISiloHostBuilder siloBuilder)
{
    siloBuilder.Configure<EndpointOptions>(options =>
    {
        // Port to use for silo-to-silo
        options.SiloPort = 11111;
        // Port to use for the gateway
        options.GatewayPort = 30000;
        // IP Address to advertise in the cluster
        options.AdvertisedIPAddress = IPAddress.Parse("172.16.0.42");
        // The socket used for client-to-silo will bind to this endpoint
        options.GatewayListeningEndpoint = new IPEndPoint(IPAddress.Any, 40000);
        // The socket used by the gateway will bind to this endpoint
        options.SiloListeningEndpoint = new IPEndPoint(IPAddress.Any, 50000);
    });
}
```

--------------------------------

### Orleans Client Project Package References

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Add these package references to your Orleans client project if it's separate from the silo, for Aspire and Redis clustering integration.

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Orleans.Client" Version="10.1.0" />
  <PackageReference Include="Microsoft.Orleans.Clustering.Redis" Version="10.1.0" />
  <PackageReference Include="Aspire.StackExchange.Redis" Version="13.2.3" />
</ItemGroup>
```

--------------------------------

### Configure MessagePack Serialization with Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Configure MessagePack serialization with custom options, including compression and enabling DataContract attributes. The `isSerializable` delegate is set to include types with the MessagePackObject attribute.

```csharp
public static void ConfigureMessagePackWithOptions(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.Services.AddSerializer(serializerBuilder => serializerBuilder.AddMessagePackSerializer(
            isSerializable: type => type.GetCustomAttribute<MessagePackObjectAttribute>() != null,
            isCopyable: type => false,
            configureOptions: options => options.Configure(opts =>
            {
                opts.SerializerOptions = MessagePackSerializerOptions.Standard
                    .WithCompression(MessagePackCompression.Lz4BlockArray);
                opts.AllowDataContractAttributes = true;
            })
        ));
    });
}
```

--------------------------------

### Using ClusterFixture in Test Cases

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

Demonstrates how to use the `ClusterFixture` and `ClusterCollection` to reuse a `TestCluster` across test cases. The fixture is injected via the constructor, and tests access the cluster through it.

```csharp
using Orleans.TestingHost;

namespace Tests;

[Collection(ClusterCollection.Name)]
public class HelloGrainTestsWithFixture(ClusterFixture fixture)
{
    private readonly TestCluster _cluster = fixture.Cluster;

    [Fact]
    public async Task SaysHelloCorrectly()
    {
        var hello = _cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());
        var greeting = await hello.SayHello("World");

        Assert.Equal("Hello, World!", greeting);
    }
}
```

--------------------------------

### Bicep: App Service Deployment Template

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

This Bicep template defines and configures Azure App Service resources, including the App Service Plan, the main App Service, and a staging slot. It sets up application settings for Application Insights, storage connection strings, and cluster IDs.

```bicep
param appName string
param location string
param vnetSubnetId string
param stagingSubnetId string
param appInsightsInstrumentationKey string
param appInsightsConnectionString string
param storageConnectionString string

resource appServicePlan 'Microsoft.Web/serverfarms@2021-03-01' = {
  name: '${appName}-plan'
  location: location
  kind: 'app'
  sku: {
    name: 'S1'
    capacity: 1
  }
}

resource appService 'Microsoft.Web/sites@2021-03-01' = {
  name: appName
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    virtualNetworkSubnetId: vnetSubnetId
    httpsOnly: true
    siteConfig: {
      vnetPrivatePortsCount: 2
      webSocketsEnabled: true
      netFrameworkVersion: 'v8.0'
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsightsInstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsightsConnectionString
        }
        {
          name: 'ORLEANS_AZURE_STORAGE_CONNECTION_STRING'
          value: storageConnectionString
        }
        {
          name: 'ORLEANS_CLUSTER_ID'
          value: 'Default'
        }
      ]
      alwaysOn: true
    }
  }
}

resource stagingSlot 'Microsoft.Web/sites/slots@2022-03-01' = {
  name: '${appName}stg'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    virtualNetworkSubnetId: stagingSubnetId
    siteConfig: {
      http20Enabled: true
      vnetPrivatePortsCount: 2
      webSocketsEnabled: true
      netFrameworkVersion: 'v8.0'
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsightsInstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsightsConnectionString
        }
        {
          name: 'ORLEANS_AZURE_STORAGE_CONNECTION_STRING'
          value: storageConnectionString
        }
        {
          name: 'ORLEANS_CLUSTER_ID'
          value: 'Staging'
        }
      ]
      alwaysOn: true
    }
  }
}

resource slotConfig 'Microsoft.Web/sites/config@2021-03-01' = {
  name: 'slotConfigNames'
  parent: appService
  properties: {
    appSettingNames: [
      'ORLEANS_CLUSTER_ID'
    ]
  }
}

resource appServiceConfig 'Microsoft.Web/sites/config@2021-03-01' = {
  parent: appService
  name: 'metadata'
  properties: {
    CURRENT_STACK: 'dotnet'
  }
}

```

--------------------------------

### Instantiate GrainCancellationTokenSource

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Create a GrainCancellationTokenSource object to manage and send cancellation notifications. This is the first step in using the legacy cancellation mechanism.

```csharp
var tcs = new GrainCancellationTokenSource();
```

--------------------------------

### Consume Streaming Grain Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Use this pattern to consume streaming data from a grain. The `await foreach` loop processes items as they arrive, allowing for immediate processing.

```csharp
var grain = client.GetGrain<IDataGrain>("mydata");

await foreach (var item in grain.GetAllItemsAsync())
{
    Console.WriteLine($"Received item: {item.Id}");

    // Process each item as it arrives
    await ProcessItemAsync(item);
}
```

--------------------------------

### Enable Silo Metadata with Default Configuration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/silo-metadata

Apply silo metadata configured in appsettings.json by calling the `UseSiloMetadata()` extension method on the silo builder. This method automatically reads the 'Orleans:Metadata' configuration section.

```csharp
Host.CreateApplicationBuilder(args).UseOrleans(siloBuilder =>
{
    // Configuration section Orleans:Metadata is used by default
    siloBuilder.UseSiloMetadata();
});

```

--------------------------------

### Configure Silo TLS with Windows Certificate Store

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/transport-layer-security

Use this configuration to enable TLS on an Orleans silo using a certificate from the Windows certificate store. Ensure the certificate is valid and accessible.

```csharp
using IHost host = Host.CreateDefaultBuilder()
    .UseOrleans(builder =>
    {
        builder
            .UseLocalhostClustering()
            .UseTls(StoreName.My, "my-certificate-subject", allowInvalid: false, StoreLocation.CurrentUser, options =>
            {
                options.OnAuthenticateAsClient = (connection, sslOptions) =>
                {
                    sslOptions.TargetHost = "my-certificate-subject";
                };
            });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.RunAsync();

```

--------------------------------

### Enable Build-Time Code Generation with OrleansCodeGenerator.Build

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/code-generation

Use Microsoft.Orleans.OrleansCodeGenerator.Build for C# projects to enable build-time code generation using Roslyn for analysis and code generation.

```xml
<PackageReference Include="Microsoft.Orleans.OrleansCodeGenerator.Build" />
```

--------------------------------

### Configure Development Client with Relaxed TLS Validation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/transport-layer-security

Use this configuration for development environments to bypass strict TLS certificate validation. It allows for easier local testing without needing a fully trusted certificate.

```csharp
var hostBuilder = Host.CreateDefaultBuilder()
    .UseEnvironment(Environments.Development);

using IHost host = hostBuilder
    .UseOrleansClient((context, builder) =>
    {
        var isDevelopment = context.HostingEnvironment.IsDevelopment();

        builder
            .UseLocalhostClustering()
            .UseTls(StoreName.My, "localhost", allowInvalid: isDevelopment, StoreLocation.CurrentUser, options =>
            {
                if (isDevelopment)
                {
                    options.AllowAnyRemoteCertificate();
                }

                options.OnAuthenticateAsClient = (connection, sslOptions) =>
                {
                    sslOptions.TargetHost = "localhost";
                };
            });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.RunAsync();

```

--------------------------------

### Co-host Orleans Dashboard with Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/dashboard

Recommended deployment pattern for hosting the Orleans Dashboard directly with your Orleans silo. This is the simplest approach for most use cases.

```csharp
using Orleans.Dashboard;

var builder = WebApplication.CreateBuilder(args);

builder.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.UseInMemoryReminderService();
    siloBuilder.AddMemoryGrainStorageAsDefault();
    siloBuilder.AddDashboard();
});

var app = builder.Build();

app.MapOrleansDashboard();

app.Run();

```

--------------------------------

### Graceful Silo Shutdown in Console App

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/shutting-down-orleans

This code demonstrates how to configure an Orleans silo within a .NET Generic Host and run it as a console application. The `RunConsoleAsync` method handles process termination signals for graceful shutdown.

```csharp
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        // Use the siloBuilder instance to configure the Orleans silo.
    })
    .RunConsoleAsync();

```

--------------------------------

### Build Orleans Client with ClientBuilder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Builds an Orleans client using the ClientBuilder API, configuring cluster options, Azure Storage clustering, and application parts. This approach is useful for more granular control over client configuration.

```csharp
var client = new ClientBuilder()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "my-first-cluster";
        options.ServiceId = "MyOrleansService";
    })
    .UseAzureStorageClustering(
        options => options.ConfigureTableServiceClient(connectionString))
    .ConfigureApplicationParts(
        parts => parts.AddApplicationPart(typeof(IValueGrain).Assembly))
    .Build();


```

--------------------------------

### Configure Orleans to use Newtonsoft.Json

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-configuration

Use this to configure Orleans to serialize specific types using Newtonsoft.Json. Ensure the Microsoft.Orleans.Serialization.NewtonsoftJson NuGet package is referenced.

```csharp
siloBuilder.Services.AddSerializer(serializerBuilder =>
{
    serializerBuilder.AddNewtonsoftJsonSerializer(
        isSupported: type => type.Namespace.StartsWith("Example.Namespace"));
});
```

--------------------------------

### Configure Orleans Logging Level Declaratively

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/troubleshooting-deployments

Add the `<Tracing>` element to Orleans configuration files to set the default trace level to 'Info'. This is an alternative to programmatic configuration.

```xml
<OrleansConfiguration>
  <Tracing DefaultTraceLevel="Info" />
</OrleansConfiguration>
```

```xml
<ClientConfiguration>
  <Tracing DefaultTraceLevel="Info" />
</ClientConfiguration>
```

--------------------------------

### Configure Orleans Client with Azure Storage Clustering and Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Configures an Orleans client using Azure Storage for clustering with a provided connection string. Ensure the connection string is securely managed.

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "my-first-cluster";
        options.ServiceId = "MyOrleansService";
    });

    clientBuilder.UseAzureStorageClustering(
        options => options.ConfigureTableServiceClient(connectionString));
});

using var host = builder.Build();
await host.StartAsync();

```

--------------------------------

### Configure In-Memory Reminders in Silo with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Enable Orleans in your Silo project without external reminder storage for development. This snippet assumes service defaults are added.

```csharp
public static void RemindersInMemorySilo(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.AddServiceDefaults();
    builder.UseOrleans();

    builder.Build().Run();
}

```

--------------------------------

### Configure OpenTelemetry for Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configures OpenTelemetry logging, metrics, and tracing for Orleans applications. Includes instrumentation for ASP.NET Core, HttpClient, and Orleans runtime.

```csharp
public static IHostApplicationBuilder AddServiceDefaults(
    this IHostApplicationBuilder builder)
{
    builder.ConfigureOpenTelemetry();
    builder.AddDefaultHealthChecks();
    
    return builder;
}

public static IHostApplicationBuilder ConfigureOpenTelemetry(
    this IHostApplicationBuilder builder)
{
    builder.Logging.AddOpenTelemetry(logging =>
    {
        logging.IncludeFormattedMessage = true;
        logging.IncludeScopes = true;
    });

    builder.Services.AddOpenTelemetry()
        .WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddMeter("Microsoft.Orleans");  // Orleans metrics
        })
        .WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("Microsoft.Orleans.Runtime")
                .AddSource("Microsoft.Orleans.Application");
        });

    return builder;
}
```

--------------------------------

### Enable Activation Rebalancing

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

Use the `AddActivationRebalancer` extension method to enable the activation rebalancing feature. This code should be placed in your silo host configuration. Remember to suppress the experimental warning using `#pragma warning disable ORLEANSEXP002`.

```csharp
#pragma warning disable ORLEANSEXP002
siloBuilder.AddActivationRebalancer();
#pragma warning restore ORLEANSEXP002

```

--------------------------------

### Configure Silo with Application Insights Telemetry Consumer

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

Adds the Application Insights telemetry consumer to the silo configuration. Replace 'INSTRUMENTATION_KEY' with your actual Application Insights instrumentation key.

```csharp
var siloHostBuilder = new HostBuilder()
    .UseOrleans(c =>
    {
        c.AddApplicationInsightsTelemetryConsumer("INSTRUMENTATION_KEY");
    });

```

--------------------------------

### Automatic Migration with IPersistentState<T>

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Grains using IPersistentState<T> also automatically support migration. Their state is serialized and restored without additional code.

```csharp
// Automatic migration support via IPersistentState<T>
public class MyOtherGrain : Grain, IMyGrain
{
    private readonly IPersistentState<MyGrainState> _state;

    public MyOtherGrain(
        [PersistentState("state", "storage")] IPersistentState<MyGrainState> state)
    {
        _state = state;
    }
}
```

--------------------------------

### Bicep template for Azure Container App

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Deploys an Azure Container App with specified parameters including image, environment, secrets, and ingress configuration. Outputs the fully qualified domain name (FQDN) of the container app.

```bicep
param appName string
param location string
param containerAppEnvironmentId string
param repositoryImage string = 'mcr.microsoft.com/azuredocs/containerapps-helloworld:latest'
param envVars array = []
param registry string
param registryUsername string
@secure()
param registryPassword string

resource containerApp 'Microsoft.App/containerApps@2022-03-01' = {
  name: appName
  location: location
  properties: {
    managedEnvironmentId: containerAppEnvironmentId
    configuration: {
      activeRevisionsMode: 'multiple'
      secrets: [
        {
          name: 'container-registry-password'
          value: registryPassword
        }
      ]
      registries: [
        {
          server: registry
          username: registryUsername
          passwordSecretRef: 'container-registry-password'
        }
      ]
      ingress: {
        external: true
        targetPort: 80
      }
    }
    template: {
      revisionSuffix: uniqueString(repositoryImage, appName)
      containers: [
        {
          image: repositoryImage
          name: appName
          env: envVars
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 1
      }
    }
  }
}

output acaUrl string = containerApp.properties.configuration.ingress.fqdn
```

--------------------------------

### Hosted Service for Orleans Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Implement IHostedService to manage the Orleans cluster client lifecycle. Inject IClusterClient to consume grains in your application services.

```csharp
using Microsoft.Extensions.Hosting;

namespace Client;

public sealed class ClusterClientHostedService : IHostedService
{
    private readonly IClusterClient _client;

    public ClusterClientHostedService(IClusterClient client)
    {
        _client = client;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Use the _client to consume grains...

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
```

--------------------------------

### Marking Method as Obsolete in V2 Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/backward-compatibility-guidelines

Demonstrates marking a method as obsolete in Version 2 of a grain interface as a step towards its eventual removal.

```csharp
[Version(2)]
public interface IMyGrain : IGrainWithIntegerKey
{
    // Method inherited from V1
    [Obsolete]
    Task MyMethod(int arg);

    // New method added in V2
    Task MyNewMethod(int arg, obj o);
}
```

--------------------------------

### Configure Redis Clustering with .NET Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

When using .NET Aspire, configure Redis clustering declaratively in the AppHost project. The silo project then automatically receives the configuration.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var orleans = builder.AddOrleans("cluster")
    .WithClustering(redis);

builder.AddProject<Projects.MySilo>("silo")
    .WithReference(orleans)
    .WithReference(redis);

builder.Build().Run();

```

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedRedisClient("redis");
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### FileGrainStorageOptions Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Defines the options for the file-based grain storage provider, including the root directory for storing state files and the grain storage serializer.

```csharp
using Orleans.Storage;

namespace GrainStorage;

public sealed class FileGrainStorageOptions : IStorageProviderSerializerOptions
{
    public required string RootDirectory { get; set; }

    public required IGrainStorageSerializer GrainStorageSerializer { get; set; }
}

```

--------------------------------

### Configure Silo Metadata for Filtering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Configure silo metadata to enable metadata-based filtering for grain placement. This can be done using environment variables, IConfiguration, or a dictionary.

```csharp
builder.UseSiloMetadata();
```

```csharp
builder.UseSiloMetadata(configuration.GetSection("Orleans:Metadata"));
```

```csharp
builder.UseSiloMetadata(new Dictionary<string, string>
{
    ["zone"] = "us-east-1a",
    ["tier"] = "premium",
    ["rack"] = "rack-42"
});
```

--------------------------------

### Making Grain Calls from Non-Grain Contexts

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

Illustrates how to make grain calls from code running on a thread pool thread, outside the Orleans grain context. Grain calls can be made from non-grain contexts without extra ceremony.

```csharp
public async Task MyGrainMethod()
{
    // Grab the Orleans task scheduler
    var orleansTS = TaskScheduler.Current;
    var fooGrain = this.GrainFactory.GetGrain<IFooGrain>(0);
    Task<int> t1 = Task.Run(async () =>
    {
        // This code runs on the thread pool scheduler,
        // not on Orleans task scheduler
        Assert.AreNotEqual(orleansTS, TaskScheduler.Current);
        int res = await fooGrain.MakeGrainCall();

        // This code continues on the thread pool scheduler,
        // not on the Orleans task scheduler
        Assert.AreNotEqual(orleansTS, TaskScheduler.Current);
        return res;
    });

    int result = await t1;

    // We are back to the Orleans task scheduler.
    // Since await was executed in the Orleans task scheduler context,
    // we are now back to that context.
    Assert.AreEqual(orleansTS, TaskScheduler.Current);
}

```

--------------------------------

### Implement Custom Grain Storage Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Implement this interface to create a custom storage provider for Orleans grain state. It defines methods for reading, writing, and clearing grain state.

```csharp
/// <summary>
/// Interface to be implemented for a storage able to read and write Orleans grain state data.
/// </summary>
public interface ICustomGrainStorage
{
    /// <summary>Read data function for this storage instance.</summary>
    /// <param name="stateName">Name of the state for this grain</param>
    /// <param name="grainId">Grain ID</param>
    /// <param name="grainState">State data object to be populated for this grain.</param>
    /// <typeparam name="T">The grain state type.</typeparam>
    /// <returns>Completion promise for the Read operation on the specified grain.</returns>
    Task ReadStateAsync<T>(
        string stateName, GrainId grainId, IGrainState<T> grainState);

    /// <summary>Write data function for this storage instance.</summary>
    /// <param name="stateName">Name of the state for this grain</param>
    /// <param name="grainId">Grain ID</param>
    /// <param name="grainState">State data object to be written for this grain.</param>
    /// <typeparam name="T">The grain state type.</typeparam>
    /// <returns>Completion promise for the Write operation on the specified grain.</returns>
    Task WriteStateAsync<T>(
        string stateName, GrainId grainId, IGrainState<T> grainState);

    /// <summary>Delete / Clear data function for this storage instance.</summary>
    /// <param name="stateName">Name of the state for this grain</param>
    /// <param name="grainId">Grain ID</param>
    /// <param name="grainState">Copy of last-known state data object for this grain.</param>
    /// <typeparam name="T">The grain state type.</typeparam>
    /// <returns>Completion promise for the Delete operation on the specified grain.</returns>
    Task ClearStateAsync<T>(
        string stateName, GrainId grainId, IGrainState<T> grainState);
}
```

--------------------------------

### Basic Client TLS Configuration in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/transport-layer-security

Configure TLS for an Orleans client to securely connect to TLS-enabled silos. This snippet shows how to use the `UseTls` method with client certificate options, including setting the `TargetHost` for authentication.

```csharp
using IHost host = Host.CreateDefaultBuilder()
    .UseOrleansClient(builder =>
    {
        builder
            .UseLocalhostClustering()
            .UseTls(StoreName.My, "my-certificate-subject", allowInvalid: false, StoreLocation.CurrentUser, options =>
            {
                options.OnAuthenticateAsClient = (connection, sslOptions) =>
                {
                    sslOptions.TargetHost = "my-certificate-subject";
                };
            });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.RunAsync();

```

--------------------------------

### Serialize C# Record Types

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Demonstrates how to define a serializable C# record type with primary constructor parameters and additional fields. Ensure parameter order is not changed after deployment to maintain compatibility.

```csharp
[GenerateSerializer]
public record MyRecord(string A, string B)
{
    // ID 0 won't clash with A in primary constructor as they don't share identities
    [Id(0)]
    public string C { get; init; }
}
```

--------------------------------

### Client Configuration with Azure Table Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure an Orleans client to use Azure Table storage for cluster membership. Ensure the connection string is correctly set.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";

var client = new ClientBuilder()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAzureStorageClustering(
        options => options.ConnectionString = connectionString)
    .Build();

```

--------------------------------

### Integrate Redis Grain Storage with .NET Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Configure Redis grain storage within a .NET Aspire AppHost project. This involves adding a Redis resource and referencing it when configuring Orleans.

```csharp
// In your AppHost project
var redis = builder.AddRedis("orleans-redis");

var orleans = builder.AddOrleans("cluster")
    .WithGrainStorage("Default", redis);

builder.AddProject<Projects.OrleansServer>("silo")
    .WithReference(orleans)
    .WaitFor(redis);

```

--------------------------------

### Grain Interface with Single Optional CancellationToken Parameter (Correct)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

This code demonstrates the correct way to define a grain interface method with a single, optional CancellationToken parameter, ensuring compatibility with older clients.

```csharp
public interface IMyGrain : IGrainWithGuidKey
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}
```

--------------------------------

### Add Orleans to ASP.NET Core Project (CLI)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Add the Microsoft.Orleans.Server NuGet package to your ASP.NET Core project using the .NET CLI. For .NET 10+, use 'dotnet package add'.

```bash
dotnet add package Microsoft.Orleans.Server
```

```bash
dotnet package add Microsoft.Orleans.Server
```

--------------------------------

### Bicep template for Storage Account

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Defines a storage account resource with specified name, location, kind, and SKU. It also outputs the connection string for the storage account.

```bicep
resource storage 'Microsoft.Storage/storageAccounts@2021-08-01' = {
  name: name
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

var key = listKeys(storage.name, storage.apiVersion).keys[0].value
var protocol = 'DefaultEndpointsProtocol=https'
var accountBits = 'AccountName=${storage.name};AccountKey=${key}'
var endpointSuffix = 'EndpointSuffix=${environment().suffixes.storage}'

output connectionString string = '${protocol};${accountBits};${endpointSuffix}'
```

--------------------------------

### Bicep Storage Module

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

This Bicep file defines a storage account resource. It outputs the connection string, which is used by other modules to connect to the storage account.

```bicep
param name string
param location string

resource storage 'Microsoft.Storage/storageAccounts@2021-08-01' = {
  name: name
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

var key = listKeys(storage.name, storage.apiVersion).keys[0].value
var protocol = 'DefaultEndpointsProtocol=https'
var accountBits = 'AccountName=${storage.name};AccountKey=${key}'
var endpointSuffix = 'EndpointSuffix=${environment().suffixes.storage}'

output connectionString string = '${protocol};${accountBits};${endpointSuffix}'

```

--------------------------------

### Configure Cassandra Clustering with Custom Session Factory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Provide a custom session factory for advanced Cassandra clustering scenarios, allowing full control over cluster and session creation.

```csharp
using Cassandra;

siloBuilder.UseCassandraClustering(async serviceProvider =>
{
    var cluster = Cluster.Builder()
        .AddContactPoints("cassandra-node1", "cassandra-node2")
        .WithPort(9042)
        .WithCredentials("username", "password")
        .WithQueryOptions(new QueryOptions().SetConsistencyLevel(ConsistencyLevel.Quorum))
        .Build();

    return await cluster.ConnectAsync("orleans");
});

```

--------------------------------

### Configure Silo with Azure Storage Clustering - Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Sets up a SiloHostBuilder with Azure Table storage for clustering, defines cluster and service IDs, configures endpoints, and adds application parts. Requires a connection string for Azure Storage.

```csharp
public static async Task ConfigureSilo(string connectionString)
{
    var siloHostBuilder = new SiloHostBuilder()
        .UseAzureStorageClustering(
            options => options.ConfigureTableServiceClient(connectionString))
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "my-first-cluster";
            options.ServiceId = "AspNetSampleApp";
        })
        .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
        .ConfigureApplicationParts(
            parts => parts.AddApplicationPart(typeof(ValueGrain).Assembly).WithReferences());

    var silo = siloHostBuilder.Build();
    await silo.StartAsync();
}
```

--------------------------------

### Implement the custom placement filter director logic

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Implement the IPlacementFilterDirector interface to define the filtering logic. The Filter method receives candidate silos and returns a filtered list, prioritizing the local silo if available.

```csharp
internal class ExamplePreferLocalPlacementFilterDirector(
    ILocalSiloDetails localSiloDetails)
    : IPlacementFilterDirector
{
    public IEnumerable<SiloAddress> Filter(PlacementFilterStrategy filterStrategy, PlacementTarget target, IEnumerable<SiloAddress> silos)
    {
        var siloList = silos.ToList();
        var localSilo = siloList.FirstOrDefault(s => s == localSiloDetails.SiloAddress);
        if (localSilo is not null)
        {
            return [localSilo];
        }
        return siloList;
    }
}

```

--------------------------------

### GrainBase Persistence Methods

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

These abstract methods are defined in the GrainBaseExample<TState> class and correspond to persistence operations. Subclasses can override these to implement custom logic for reading, writing, or clearing grain state.

```csharp
public abstract class GrainBaseExample<TState>
{
    protected virtual Task ReadStateAsync() { return Task.CompletedTask; }
    protected virtual Task WriteStateAsync() { return Task.CompletedTask; }
    protected virtual Task ClearStateAsync() { return Task.CompletedTask; }
}
```

--------------------------------

### Configure Basic MessagePack Serialization

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Configure Orleans to use MessagePack as the serializer for specific types defined by a namespace predicate. Types not matching the predicate will fall back to the default Orleans serializer.

```csharp
public static void ConfigureMessagePackBasic(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.Services.AddSerializer(serializerBuilder => serializerBuilder.AddMessagePackSerializer(
            isSerializable: type => type.Namespace?.StartsWith("MyApp.Messages") == true,
            isCopyable: type => false,
            messagePackSerializerOptions: null
        ));
    });
}
```

--------------------------------

### Define Surrogate for Type Hierarchy Serialization

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Implement `IPopulator` alongside `IConverter` for foreign types in a type hierarchy. This allows Orleans to serialize derived types correctly.

```csharp
// The foreign type is not sealed, allowing other types to inherit from it.
public class MyForeignLibraryType
{
    public MyForeignLibraryType() { }

    public MyForeignLibraryType(int num, string str, DateTimeOffset dto)
    {
        Num = num;
        String = str;
        DateTimeOffset = dto;
    }

    public int Num { get; set; }
    public string String { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
}

// The surrogate is defined as it was in the previous example.
[GenerateSerializer]
public struct MyForeignLibraryTypeSurrogate
{
    [Id(0)]
    public int Num;

    [Id(1)]
    public string String;

    [Id(2)]
    public DateTimeOffset DateTimeOffset;
}

// Implement the IConverter and IPopulator interfaces on the converter.
[RegisterConverter]
public sealed class MyForeignLibraryTypeSurrogateConverter :
    IConverter<MyForeignLibraryType, MyForeignLibraryTypeSurrogate>,
    IPopulator<MyForeignLibraryType, MyForeignLibraryTypeSurrogate>
{
    public MyForeignLibraryType ConvertFromSurrogate(
        in MyForeignLibraryTypeSurrogate surrogate) =>
        new(surrogate.Num, surrogate.String, surrogate.DateTimeOffset);

    public MyForeignLibraryTypeSurrogate ConvertToSurrogate(
        in MyForeignLibraryType value) =>
        new()
    {
        Num = value.Num,
        String = value.String,
        DateTimeOffset = value.DateTimeOffset
    };

    public void Populate(
        in MyForeignLibraryTypeSurrogate surrogate, MyForeignLibraryType value)
    {
        value.Num = surrogate.Num;
        value.String = surrogate.String;
        value.DateTimeOffset = surrogate.DateTimeOffset;
    }
}

// Application types can inherit from the foreign type, assuming they're not sealed
// since Orleans knows how to serialize it.
[GenerateSerializer]
public sealed class DerivedFromMyForeignLibraryType : MyForeignLibraryType
{
    public DerivedFromMyForeignLibraryType() { }

    public DerivedFromMyForeignLibraryType(
        int intValue, int num, string str, DateTimeOffset dto) : base(num, str, dto)
    {
        IntValue = intValue;
    }

    [Id(0)]
    public int IntValue { get; set; }
}

```

--------------------------------

### Configure Azure Table Storage for Pub-Sub with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Configures the Pub-Sub component to use Azure Table Storage with a connection string for storing subscription data. This method is less secure than managed identity.

```csharp
hostBuilder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddAzureTableGrainStorage("PubSubStore",
        options => options.TableServiceClient = new TableServiceClient(connectionString));
});

```

--------------------------------

### Call Grain with Automatic and Manual Cancellation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Demonstrates creating a `CancellationTokenSource`, setting a timeout for automatic cancellation, and manually cancelling a long-running grain operation. Catches `OperationCanceledException` to handle cancellation.

```csharp
var grain = grainFactory.GetGrain<IProcessingGrain>(Guid.NewGuid());

using var cts = new CancellationTokenSource();

// Set a timeout for automatic cancellation
cts.CancelAfter(TimeSpan.FromSeconds(30));

try
{
    var result = await grain.ProcessDataAsync("sample data", 20, cts.Token);
    Console.WriteLine($"Result: {result}");
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation was canceled");
}

// Manual cancellation example
var grain2 = grainFactory.GetGrain<IProcessingGrain>(Guid.NewGuid());
using var cts2 = new CancellationTokenSource();

// Start a long-running task
var task = grain2.ProcessDataAsync("large dataset", 1000, cts2.Token);

// Cancel after 5 seconds
await Task.Delay(5000);
cts2.Cancel();

try
{
    await task;
}
catch (OperationCanceledException)
{
    Console.WriteLine("Long processing was canceled");
}
```

--------------------------------

### Create Azure Service Principal with Azure CLI

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

Use the Azure CLI to create a service principal with Contributor role and specify the scope of permissions. This command outputs JSON credentials required for authentication.

```bash
az ad sp create-for-rbac --sdk-auth --role Contributor \
  --name "<display-name>"  --scopes /subscriptions/<your-subscription-id>
```

--------------------------------

### Monitor Orleans Counters with dotnet counters

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

Use the `dotnet counters monitor` command to observe Orleans ActivitySource counters for a specific process. Replace `<ProcessName>` with the actual name of your application's process.

```bash
dotnet counters monitor -n <ProcessName> --counters Microsoft.Orleans
```

--------------------------------

### AdoNetGrainStorageOptions Class Definition

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/relational-storage

Defines the configuration options for the ADO.NET grain storage provider, including connection string, initialization stage, invariant name, and serializer.

```csharp
/// <summary>
/// Options for AdoNetGrainStorage
/// </summary>
public class AdoNetGrainStorageOptions
{
    /// <summary>
    /// Connection string for AdoNet storage.
    /// </summary>
    [Redact]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Stage of silo lifecycle where storage should be initialized.
    /// Storage must be initialized prior to use.
    /// </summary>
    public int InitStage { get; set; } = DEFAULT_INIT_STAGE;

    /// <summary>
    /// Default init stage in silo lifecycle.
    /// </summary>
    public const int DEFAULT_INIT_STAGE =
        ServiceLifecycleStage.ApplicationServices;

    /// <summary>
    /// The default ADO.NET invariant used for storage if none is given.
    /// </summary>
    public const string DEFAULT_ADONET_INVARIANT =
        AdoNetInvariants.InvariantNameSqlServer;

    /// <summary>
    /// The invariant name for storage.
    /// </summary>
    public string Invariant { get; set; } =
        DEFAULT_ADONET_INVARIANT;

    /// <summary>
    /// The grain storage serializer.
    /// </summary>
    public IGrainStorageSerializer GrainStorageSerializer { get; set; }

    /// <summary>
    /// Gets or sets the hasher picker to use for this storage provider.
    /// </summary>
    public IStorageHasherPicker HashPicker { get; set; }
}

```

--------------------------------

### Client Call to AtmGrain.Transfer Transactionally

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

This client code demonstrates how to initiate a transactional transfer of funds between two accounts using the `AtmGrain`. It retrieves account balances after the transfer to verify the operation.

```csharp
IAtmGrain atmOne = client.GetGrain<IAtmGrain>(0);

Guid from = Guid.NewGuid();
Guid to = Guid.NewGuid();

await atmOne.Transfer(from, to, 100);

uint fromBalance = await client.GetGrain<IAccountGrain>(from).GetBalance();
uint toBalance = await client.GetGrain<IAccountGrain>(to).GetBalance();
```

--------------------------------

### Invoke a Grain Call Sequence

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-scheduling

Demonstrates a simple grain call sequence where one grain calls another. This illustrates the default non-reentrant request scheduling.

```csharp
var a = grainFactory.GetGrain("A");
var b = grainFactory.GetGrain("B");
await a.CallOther(b);
```

--------------------------------

### FileGrainStorageOptions Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Defines options for the file-based grain storage, primarily specifying the root directory for storing grain state files.

```csharp
public class FileGrainStorageOptions
{
    public string RootDirectory { get; set; }
}
```

--------------------------------

### Migrate Grain to a Specific Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Specify a preferred target silo for migration using placement hints by setting RequestContext.Set(IPlacementDirector.PlacementHintKey, targetSilo) before calling MigrateOnIdle().

```csharp
public Task MigrateToSilo(SiloAddress targetSilo)
{
    RequestContext.Set(IPlacementDirector.PlacementHintKey, targetSilo);
    this.MigrateOnIdle();
    return Task.CompletedTask;
}
```

--------------------------------

### Configure Azure Cosmos DB Clustering with Client Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Configure Orleans to use Azure Cosmos DB for clustering using client options and Azure identity.

```csharp
using Azure.Identity;

var builder = Host.CreateApplicationBuilder(args);

builder.UseOrleans(siloBuilder =>
{
    siloBuilder.UseCosmosClustering(options =>
    {
        options.ConfigureCosmosClient(
            "https://myaccount.documents.azure.com:443/",
            new DefaultAzureCredential());
        options.DatabaseName = "Orleans";
        options.ContainerName = "OrleansCluster";
        options.IsResourceCreationEnabled = true;
    });
});

```

--------------------------------

### Enable Azure Table Storage Deployment

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Set the DEPLOY_AZURE_TABLE_STORAGE environment variable to true to enable the deployment of Azure Table Storage. This is a prerequisite for using Azure data services for grain persistence.

```bash
azd env set DEPLOY_AZURE_TABLE_STORAGE true

```

--------------------------------

### Custom Grain Migration with IGrainMigrationParticipant

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Implement IGrainMigrationParticipant in your grain to manually save and restore custom in-memory state during migration.

```csharp
public class MyMigratableGrain : Grain, IMyGrain, IGrainMigrationParticipant
{
    private int _cachedValue;
    private string _sessionData;

    public void OnDehydrate(IDehydrationContext dehydrationContext)
    {
        // Save state before migration
        dehydrationContext.TryAddValue("cached", _cachedValue);
        dehydrationContext.TryAddValue("session", _sessionData);
    }

    public void OnRehydrate(IRehydrationContext rehydrationContext)
    {
        // Restore state after migration
        rehydrationContext.TryGetValue("cached", out _cachedValue);
        rehydrationContext.TryGetValue("session", out _sessionData);
    }
}
```

--------------------------------

### Orleans Silo Configuration with Explicit Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configure Orleans silo clustering and grain storage using an explicit Redis connection string read from configuration. Ensure the backing resource is registered with `AddKeyedRedisClient`.

```csharp
public static void ExplicitConnectionConfiguration(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.AddServiceDefaults();
    builder.AddKeyedRedisClient("orleans-redis");

    builder.UseOrleans(siloBuilder =>
    {
        var redisConnectionString = builder.Configuration.GetConnectionString("orleans-redis");

        siloBuilder.UseRedisClustering(options =>
        {
            options.ConfigurationOptions =
                ConfigurationOptions.Parse(redisConnectionString!);
        });

        siloBuilder.AddRedisGrainStorageAsDefault(options =>
        {
            options.ConfigurationOptions =
                ConfigurationOptions.Parse(redisConnectionString!);
        });
    });

    builder.Build().Run();
}

```

--------------------------------

### Register File Grain Storage with ISiloBuilder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

An extension method for ISiloBuilder to register the file grain storage. It uses .NET 8+ keyed DI API for registration.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans.Runtime;
using Orleans.Storage;

namespace GrainStorage;

public static class FileSiloBuilderExtensions
{
    public static ISiloBuilder AddFileGrainStorage(
        this ISiloBuilder builder,
        string providerName,
        Action<FileGrainStorageOptions> options)
    {
        builder.Services.AddFileGrainStorage(providerName, options);
        return builder;
    }

    public static IServiceCollection AddFileGrainStorage(
        this IServiceCollection services,
        string providerName,
        Action<FileGrainStorageOptions> options)
    {
        services.AddOptions<FileGrainStorageOptions>(providerName)
            .Configure(options);

        services.AddTransient<
            IPostConfigureOptions<FileGrainStorageOptions>,
            DefaultStorageProviderSerializerOptionsConfigurator<FileGrainStorageOptions>>();

        // <KeyedRegistrations>
        services.AddKeyedSingleton<IGrainStorage>(
            providerName,
            (sp, key) => FileGrainStorageFactory.Create(sp, key?.ToString() ?? providerName));

        services.AddKeyedSingleton<ILifecycleParticipant<ISiloLifecycle>>(
            providerName,
            (sp, key) => (ILifecycleParticipant<ISiloLifecycle>)sp.GetRequiredKeyedService<IGrainStorage>(key));
        // </KeyedRegistrations>

        return services;
    }
}
```

--------------------------------

### Configure Azure Cosmos DB Clustering Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-cosmos-db

Use the `UseCosmosClustering` extension method to configure the clustering provider. Customize database/container names, throughput, and client credentials.

```csharp
siloBuilder.UseCosmosClustering(
    configureOptions: static options =>
    {
        options.IsResourceCreationEnabled = true;
        options.DatabaseName = "OrleansAlternativeDatabase";
        options.ContainerName = "OrleansClusterAlternativeContainer";
        options.ContainerThroughputProperties = ThroughputProperties.CreateAutoscaleThroughput(1000);
        options.ConfigureCosmosClient("<azure-cosmos-db-nosql-connection-string>");
    });

```

--------------------------------

### Injecting Named Persistent States into a Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Demonstrates how to inject two named persistent states, 'profile' and 'cart', into a grain's constructor using the PersistentStateAttribute. This allows the grain to manage different pieces of state, each potentially using a different storage provider.

```csharp
public class UserGrain : Grain, IUserGrain
{
    private readonly IPersistentState<ProfileState> _profile;
    private readonly IPersistentState<CartState> _cart;

    public UserGrain(
        [PersistentState("profile", "profileStore")] IPersistentState<ProfileState> profile,
        [PersistentState("cart", "cartStore")] IPersistentState<CartState> cart)
    {
        _profile = profile;
        _cart = cart;
    }

    public Task<string> GetNameAsync() => Task.FromResult(_profile.State.Name);

    public async Task SetNameAsync(string name)
    {
        _profile.State.Name = name;
        await _profile.WriteStateAsync();
    }
}
```

--------------------------------

### Configure Orleans Client with Azure Storage Clustering and DefaultAzureCredential

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Configures an Orleans client using Azure Storage for clustering and DefaultAzureCredential for authentication. This is recommended for seamless local development and production environments.

```csharp
using Azure.Identity;

var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "my-first-cluster";
        options.ServiceId = "MyOrleansService";
    });

    clientBuilder.UseAzureStorageClustering(options =>
    {
        options.ConfigureTableServiceClient(
            new Uri("https://<your-storage-account>.table.core.windows.net"),
            new DefaultAzureCredential());
    });
});

using var host = builder.Build();
await host.StartAsync();

```

--------------------------------

### Make GrainFactory Public and Virtual

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

To enable mocking of GrainFactory, it needs to be declared as public and virtual. This allows mocking frameworks to intercept calls to it.

```csharp
public new virtual IGrainFactory GrainFactory
{
    get => base.GrainFactory;
}
```

--------------------------------

### Call a Grain Method from Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Obtain a grain reference using the client's IGrainFactory and await the result of a grain method call. This pattern is identical to calling grains from within other grains.

```csharp
IPlayerGrain player = client.GetGrain<IPlayerGrain>(playerId);
Task joinGameTask = player.JoinGame(game)

await joinGameTask;
```

--------------------------------

### Create FileGrainStorage Factory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

A factory method to create an instance of FileGrainStorage, scoping options to the provider name for registration with the service collection.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans.Configuration.Overrides;
using Orleans.Storage;

namespace GrainStorage;

internal static class FileGrainStorageFactory
{
    internal static IGrainStorage Create(
        IServiceProvider services, string name)
    {
        var optionsMonitor =
            services.GetRequiredService<IOptionsMonitor<FileGrainStorageOptions>>();

        return ActivatorUtilities.CreateInstance<FileGrainStorage>(
            services,
            name,
            optionsMonitor.Get(name),
            services.GetProviderClusterOptions(name));
    }
}
```

--------------------------------

### Reliable Silo Deployment using SQL Server

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure silos for a reliable production deployment using SQL Server for clustering. Requires a SQL Server connection string.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";
var silo = new SiloHostBuilder()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAdoNetClustering(options =>
    {
      options.ConnectionString = connectionString;
      options.Invariant = "System.Data.SqlClient";
    })
    .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
    .ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Information).AddConsole())
    .Build();


```

--------------------------------

### Configure Azure Storage Clustering - Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Configures the ISiloHostBuilder to use Azure Table storage for cluster membership. Requires a connection string for Azure Storage.

```csharp
public static void ConfigureAzureClustering(ISiloHostBuilder siloBuilder, string connectionString)
{
    siloBuilder.UseAzureStorageClustering(
        options => options.ConfigureTableServiceClient(connectionString));
}
```

--------------------------------

### Configure Orleans Silo to Use Consul Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/consul-deployment

Configure the Orleans silo to use Consul as its cluster membership provider by referencing the appropriate NuGet package and setting the Consul client options.

```csharp
IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        silo.UseConsulSiloClustering(options =>
        {
            // The address of the Consul server
            var address = new Uri("http://localhost:8500");
            options.ConfigureConsulClient(address);
        });
    })
    .UseConsoleLifetime();

using IHost host = builder.Build();
host.Run();

```

--------------------------------

### Configure Fallback Serialization Provider in XML

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Specify the fallback serialization provider in XML configuration. This method is an alternative to using C# code for configuration.

```xml
<Messaging>
    <FallbackSerializationProvider
        Type="GreatCompany.FantasticFallbackSerializer, GreatCompany.SerializerAssembly"/>
</Messaging>

```

--------------------------------

### Configure Broadcast Channel in Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/broadcast-channel

Use this to enable broadcast channels during silo configuration. Ensure Orleans Streams are configured first.

```csharp
siloBuilder.AddBroadcastChannel(OrleansBroadcastChannelNames.ReadmodelChanges);

```

--------------------------------

### Requesting Grain Migration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

A grain can request migration to a new silo by calling MigrateOnIdle().

```csharp
public class MyGrain : Grain, IMyGrain
{
    public Task RequestMigration()
    {
        // Request migration when the grain becomes idle
        this.MigrateOnIdle();
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Configure Orleans Grain Method Timeouts with TimeSpan

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Demonstrates setting response timeouts for grain methods using `TimeSpan` constructor parameters. This allows for flexible timeout configurations, such as 2 minutes or 500 milliseconds.

```csharp
public interface IDataProcessingGrain : IGrainWithGuidKey
{
    // 2 minute timeout using hours, minutes, seconds
    [ResponseTimeout(0, 2, 0)]
    Task<ProcessingResult> ProcessLargeDatasetAsync(Dataset data, CancellationToken cancellationToken = default);

    // 500ms timeout using TimeSpan.FromMilliseconds equivalent
    [ResponseTimeout("00:00:00.500")]
    Task<HealthStatus> GetHealthAsync(CancellationToken cancellationToken = default);
}
```

--------------------------------

### Configure Azure Table Storage Reminders in Silo with Aspire

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Register Azure Table Storage as a keyed client in your Silo's Program.cs to enable Orleans reminders. This requires adding service defaults and Orleans.

```csharp
public static void RemindersAzureTableSilo(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.AddServiceDefaults();
    builder.AddKeyedAzureTableClient("reminders");
    builder.UseOrleans();

    builder.Build().Run();
}

```

--------------------------------

### Configure Silo and Gateway Ports - Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Override default silo and gateway ports using ConfigureEndpoints. Ensure ports are valid integers.

```csharp
siloBuilder.ConfigureEndpoints(siloPort: 17_256, gatewayPort: 34_512)
```

--------------------------------

### Configure Cluster Membership Options in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Customize cluster membership protocol settings using `ClusterMembershipOptions`. Adjust parameters like `NumProbedSilos`, `NumVotesForDeathDeclaration`, `DeathVoteExpirationTimeout`, `ProbeTimeout`, and `NumMissedProbesLimit` to fine-tune failure detection and silo monitoring behavior.

```csharp
siloBuilder.Configure<ClusterMembershipOptions>(options =>
{
    // Number of silos each silo monitors (default: 10)
    options.NumProbedSilos = 10;

    // Number of suspicions required to declare a silo dead (default: 2)
    options.NumVotesForDeathDeclaration = 2;

    // Time window for suspicions to be valid (default: 180 seconds)
    options.DeathVoteExpirationTimeout = TimeSpan.FromSeconds(180);

    // Interval between probes (default: 10 seconds)
    options.ProbeTimeout = TimeSpan.FromSeconds(10);

    // Number of missed probes before suspecting a silo (default: 3)
    options.NumMissedProbesLimit = 3;
});

```

--------------------------------

### Fan-out asynchronous operations with Task.WhenAll

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/best-practices

Execute multiple asynchronous operations concurrently and wait for all of them to complete using Task.WhenAll. This is useful for parallelizing independent tasks.

```csharp
var tasks = new List<Task>();

foreach (var grain in grains)
{
    tasks.Add(grain.Foo());
}
await Task.WhenAll(tasks);

DoMoreWork();

```

--------------------------------

### Configure Azure Storage Clustering with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Configure Azure Table Storage for clustering using a connection string. Avoid connection strings in production; prefer Microsoft Entra ID authentication.

```csharp
using IHost host = Host.CreateDefaultBuilder(args)
    .UseOrleans(builder =>
    {
        builder.UseAzureStorageClustering(
            options => options.ConfigureTableServiceClient(connectionString));
    })
    .UseConsoleLifetime()
    .Build();

```

--------------------------------

### Enable Azure Cosmos DB for NoSQL Deployment

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Set the DEPLOY_AZURE_COSMOS_DB_NOSQL environment variable to true to enable the deployment of Azure Cosmos DB for NoSQL. This allows your application to use Cosmos DB for data persistence.

```bash
azd env set DEPLOY_AZURE_COSMOS_DB_NOSQL true

```

--------------------------------

### Configure ADO.NET Reminder Service

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Configure the ADO.NET provider for Orleans reminders. Ensure you provide your database connection string and the correct invariant for your SQL provider.

```csharp
public static async Task ConfigureAdoNetAsync(string[] args)
{
    const string connectionString = "YOUR_CONNECTION_STRING_HERE";
    const string invariant = "YOUR_INVARIANT";

    var builder = Host.CreateApplicationBuilder(args);
    builder.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseAdoNetReminderService(options =>
        {
            options.ConnectionString = connectionString; // Redacted
            options.Invariant = invariant;
        });
    });

    using var host = builder.Build();
    await host.RunAsync();
}
```

--------------------------------

### Configure Orleans Client with Azure Storage Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/client-configuration

Configures an Orleans client using Azure Storage for clustering, retrieving the connection string from configuration. This method is suitable when the connection string is managed externally.

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "my-first-cluster";
        options.ServiceId = "MyOrleansService";
    })
    .UseAzureStorageClustering(
        options => options.ConfigureTableServiceClient(
            builder.Configuration["ORLEANS_AZURE_STORAGE_CONNECTION_STRING"]));
});

using var host = builder.Build();
await host.StartAsync();

```

--------------------------------

### Configure Orleans Host Networking for Azure App Service

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

Configure an ISiloBuilder instance to consume environment variables like WEBSITE_PRIVATE_IP and WEBSITE_PRIVATE_PORTS. This ensures the Orleans host listens on the correct internal addresses and ports provided by Azure App Service for VNet integration.

```csharp
var endpointAddress = IPAddress.Parse(builder.Configuration["WEBSITE_PRIVATE_IP"]!);\nvar strPorts = builder.Configuration["WEBSITE_PRIVATE_PORTS"]!.Split(',');\nif (strPorts.Length < 2)\n{\n    throw new Exception("Insufficient private ports configured.");\n}\n\nvar (siloPort, gatewayPort) = (int.Parse(strPorts[0]), int.Parse(strPorts[1]));\n\nsiloBuilder\n    .ConfigureEndpoints(endpointAddress, siloPort, gatewayPort, listenOnAnyHostAddress: true)
```

--------------------------------

### Inject IPersistentState into Grain Constructor

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Inject `IPersistentState<ProfileState>` into a grain's constructor using the `[PersistentState]` attribute. This allows the grain to access and manage its persistent state.

```csharp
public class UserGrainSimple : Grain, IUserGrain
{
    private readonly IPersistentState<ProfileState> _profile;

    public UserGrainSimple(
        [PersistentState("profile", "profileStore")]
        IPersistentState<ProfileState> profile)
    {
        _profile = profile;
    }

    public Task<string> GetNameAsync() => Task.FromResult(_profile.State.Name);

    public async Task SetNameAsync(string name)
    {
        _profile.State.Name = name;
        await _profile.WriteStateAsync();
    }
}


```

--------------------------------

### Configure Fallback Serialization Provider in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Set the fallback serialization provider for client and global configurations using C# code. This is useful when you need to specify a custom serializer like FantasticSerializer.

```csharp
// Client configuration
var clientConfiguration = new ClientConfiguration();
clientConfiguration.FallbackSerializationProvider =
    typeof(FantasticSerializer).GetTypeInfo();

// Global configuration
var globalConfiguration = new GlobalConfiguration();
globalConfiguration.FallbackSerializationProvider =
    typeof(FantasticSerializer).GetTypeInfo();

```

--------------------------------

### Implement a Copier Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-customization

Use the CopierMethodAttribute to flag a static method responsible for creating a semantically-equivalent copy of an object. This method should return a copy of the input object.

```csharp
[CopierMethod]
static private object Copy(object input, ICopyContext context)
{
    // ...
}
```

--------------------------------

### Add Strongly-Consistent Grain Directory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/grain-directory

Use AddDistributedGrainDirectory to configure the strongly-consistent grain directory. This can be set as the default or as a named directory.

```csharp
// Use as the default grain directory
builder.AddDistributedGrainDirectory();

// Or add as a named directory
builder.AddDistributedGrainDirectory("MyDistributedDirectory");

```

--------------------------------

### Configure Redis Grain Storage in Orleans Server

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Register the Redis client with keyed services and configure Orleans to use Redis grain storage. This snippet shows how to use the Aspire-provided connection string.

```csharp
// Register the Redis client with keyed services.
// Orleans providers look up resources by their keyed service name.
// builder.AddKeyedRedisClient("orleans-redis");

builder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddRedisGrainStorage(
        name: "redis",
        configureOptions: options =>
        {
            // Use the Aspire-provided connection string
            var connectionString = builder.Configuration.GetConnectionString("orleans-redis");
            options.ConfigurationOptions = ConfigurationOptions.Parse(connectionString!);
        });
});

```

--------------------------------

### Define a Grain Service Client Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

Create an interface for the GrainServiceClient that other grains will use to connect to the GrainService. This interface should inherit from IGrainServiceClient and the target GrainService interface.

```csharp
public interface IDataServiceClient : IGrainServiceClient<IDataService>, IDataService
{
}
```

--------------------------------

### Add Orleans Dashboard to Silo Builder

Source: https://learn.microsoft.com/en-us/dotnet/orleans/dashboard

Configure Orleans to include the dashboard services by calling AddDashboard() on the silo builder.

```csharp
using Orleans.Dashboard;

var builder = WebApplication.CreateBuilder(args);

// Configure Orleans with the dashboard
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorageAsDefault();

    // Add the dashboard services
    siloBuilder.AddDashboard();
});

var app = builder.Build();

// Map dashboard endpoints at the root path
app.MapOrleansDashboard();

app.Run();

```

--------------------------------

### Valid grain implementation for heterogeneous silos

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/heterogeneous-silos

This C# code shows a valid grain implementation that can be hosted on multiple silos. Ensure the grain class is referenced by the silos that host it.

```csharp
public class C: Grain, IMyGrainInterface
{
   public Task SomeMethod() { /* ... */ }
}
```

--------------------------------

### Define Hello Grain Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/overview-helloworld

Defines the interface for the Hello grain, specifying the methods it exposes. This interface uses IIntegerKey for grain identification.

```csharp
public interface IHello : Orleans.IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting);
}

```

--------------------------------

### Register Grain State Extension in Grain.OnActivateAsync

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-extensions

Register the IGrainStateAccessor<T> extension within the Grain.OnActivateAsync method by creating an instance with appropriate getter and setter delegates and setting it as a component in the grain's context.

```csharp
public override Task OnActivateAsync()
{
    // Retrieve the IGrainStateAccessor<T> extension
    var accessor = new GrainStateAccessor<int>(
        getter: () => this.Value,
        setter: value => this.Value = value);

    // Set the extension as a component of the target grain's context
    ((IGrainBase)this).GrainContext.SetComponent<IGrainStateAccessor<int>>(accessor);

    return base.OnActivateAsync();
}
```

--------------------------------

### Configure Silo with Custom TelemetryConfiguration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/monitoring

Configures the silo to use a custom TelemetryConfiguration, allowing for advanced settings like TelemetryProcessors and TelemetrySinks.

```csharp
var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
var siloHostBuilder = new HostBuilder()
    .UseOrleans(c =>
    {
        c.AddApplicationInsightsTelemetryConsumer(telemetryConfiguration);
    });

```

--------------------------------

### Silo Configuration with ADO.NET Clustering (Orleans 7.0-9.x)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure an Orleans silo to use SQL Server for cluster membership with ADO.NET. Use 'System.Data.SqlClient' for Orleans 7.0-9.x.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";

var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAdoNetClustering(options =>
    {
        options.ConnectionString = connectionString;
        options.Invariant = "System.Data.SqlClient";
    })
    .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000);
});

builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();

using var host = builder.Build();

```

--------------------------------

### Create an Immutable<T> Instance

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-immutability

Instantiate an Immutable<T> object by passing the underlying value to its constructor.

```csharp
Immutable<byte[]> immutable = new(buffer);
```

--------------------------------

### Serialization Configuration Properties

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/relational-storage

Properties to configure JSON and XML serialization for storage payloads. Use these to control formatting, type handling, and custom serializer settings.

```csharp
public bool UseJsonFormat { get; set; }
    public bool UseFullAssemblyNames { get; set; }
    public bool IndentJson { get; set; }
    public TypeNameHandling? TypeNameHandling { get; set; }

    public Action<JsonSerializerSettings> ConfigureJsonSerializerSettings { get; set; }

    /// <summary>
    /// Determine whether storage string payload should be formatted in Xml.
    /// <remarks>If neither <see cref="UseJsonFormat"/> nor <see cref="UseXmlFormat"/> is set to true, then BinaryFormatSerializer will be configured to format storage string payload.</remarks>
    /// </summary>
    public bool UseXmlFormat { get; set; }
```

--------------------------------

### Verify URL Shortening Endpoint

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Test the `shorten` endpoint by providing a URL. The output shows the original URL and the expected format of the shortened URL, including placeholders for container app details.

```json
{
  "original": "https://learn.microsoft.com/dotnet/orleans",
 "shortened": "http://<container-app-name>.<deployment-name>.<region>.azurecontainerapps.io:<port>/go/<generated-id>"
}

```

--------------------------------

### Configure Client Messaging Options for Cancellation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Configure client messaging options to control cancellation behavior, such as sending cancellation signals on request timeouts and waiting for acknowledgements.

```csharp
clientBuilder.Configure<ClientMessagingOptions>(options =>
{
    options.CancelRequestOnTimeout = true;
    options.WaitForCancellationAcknowledgement = false;
});
```

--------------------------------

### Configure Persistent Stream Provider with Generator Adapter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/streams-implementation

Configures a persistent stream provider to use a custom generator queue adapter for testing purposes. This involves providing a factory function to create the adapter.

```csharp
hostBuilder.AddPersistentStreams(
    StreamProviderName, GeneratorAdapterFactory.Create);

```

--------------------------------

### Share Test Cluster with xUnit Fixtures

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

Use `IAsyncLifetime` fixtures to share a single `InProcessTestCluster` across multiple xUnit tests for improved performance. Ensure `AddMemoryGrainStorageAsDefault` is configured.

```C#
public class ClusterFixture : IAsyncLifetime
{
    public InProcessTestCluster Cluster { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var builder = new InProcessTestClusterBuilder();
        builder.ConfigureSilo ((options, siloBuilder) =>
        {
            siloBuilder.AddMemoryGrainStorageAsDefault();
        });

        Cluster = builder.Build();
        await Cluster.DeployAsync();
    }

    public async Task DisposeAsync()
    {
        await Cluster.DisposeAsync();
    }
}

[CollectionDefinition(nameof(ClusterCollection))]
public class ClusterCollection : ICollectionFixture<ClusterFixture>
{
}

[Collection(nameof(ClusterCollection))]
public class HelloGrainTests
{
    private readonly ClusterFixture _fixture;

    public HelloGrainTests(ClusterFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SaysHello()
    {
        var grain = _fixture.Cluster.Client.GetGrain<IHelloGrain>(0);
        var result = await grain.SayHello("World");
        Assert.Equal("Hello, World!", result);
    }
}
```

--------------------------------

### Configure ASP.NET Binding Redirects for Orleans Compatibility

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/troubleshooting-deployments

Add binding redirects for `Microsoft.CodeAnalysis.CSharp` and `Microsoft.CodeAnalysis` in your configuration file to resolve version compatibility issues with ASP.NET's Razor view engine.

```xml
<dependentAssembly>
    <assemblyIdentity name="Microsoft.CodeAnalysis.CSharp"
        publicKeyToken="31bf3856ad364e35" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-2.0.0.0" newVersion="1.3.1.0" />
</dependentAssembly>
<dependentAssembly>
    <assemblyIdentity name="Microsoft.CodeAnalysis"
        publicKeyToken="31bf3856ad364e35" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-2.0.0.0" newVersion="1.3.1.0" />
</dependentAssembly>
```

--------------------------------

### Configure Azure Table Storage with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-storage

Configure Azure Table Storage grain persistence using a connection string. Avoid using connection strings in production environments; Microsoft Entra ID authentication is recommended.

```csharp
siloBuilder.AddAzureTableGrainStorage(
    name: "profileStore",
    configureOptions =>
    {
        options.ConfigureTableServiceClient(
            "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1");
    });


```

--------------------------------

### Silo Configuration with ADO.NET Clustering (Orleans 10.0+)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure an Orleans silo to use SQL Server for cluster membership with ADO.NET. Use 'Microsoft.Data.SqlClient' for Orleans 10.0 and later.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";

var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAdoNetClustering(options =>
    {
        options.ConnectionString = connectionString;
        options.Invariant = "Microsoft.Data.SqlClient"; // Orleans 10.0+
    })
    .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000);
});

builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();

using var host = builder.Build();

```

--------------------------------

### Configure Azure Cosmos DB Clustering with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/cluster-management

Configure Orleans to use Azure Cosmos DB for clustering by providing an account endpoint and key in the connection string.

```csharp
siloBuilder.UseCosmosClustering(options =>
{
    options.ConfigureCosmosClient("AccountEndpoint=https://myaccount.documents.azure.com:443/;AccountKey=...");
});

```

--------------------------------

### Read Grain State from File

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Reads grain state from a file, deserializes it, and sets the ETag based on the file's last write time. If the file does not exist, it initializes the state with a default instance.

```csharp
public async Task ReadStateAsync<T>(
    string stateName,
    GrainId grainId,
    IGrainState<T> grainState)
{
    var fName = GetKeyString(stateName, grainId);
    var path = Path.Combine(_options.RootDirectory, fName!);
    var fileInfo = new FileInfo(path);
    if (fileInfo is { Exists: false })
    {
        grainState.State = (T)Activator.CreateInstance(typeof(T))!;
        return;
    }

    using var stream = fileInfo.OpenText();
    var storedData = await stream.ReadToEndAsync();
    
    grainState.State = _options.GrainStorageSerializer.Deserialize<T>(new BinaryData(storedData));
    grainState.ETag = fileInfo.LastWriteTimeUtc.ToString();
}

```

--------------------------------

### Define custom grain lifecycle stages

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Defines constants for custom grain lifecycle stages. Use these to hook into specific points during grain activation and deactivation.

```csharp
public static class GrainLifecycleStage
{
    public const int First = int.MinValue;
    public const int SetupState = 1_000;
    public const int Activate = 2_000;
    public const int Last = int.MaxValue;
}
```

--------------------------------

### Client Configuration with Azure Table Clustering and Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configures an Orleans client to use Azure Table for cluster membership with a connection string. This method is not recommended for production due to security concerns.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";

var builder = Host.CreateApplicationBuilder(args);
clientBuilder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAzureStorageClustering(
        options => options.ConfigureTableServiceClient(connectionString));
});

using var host = builder.Build();


```

--------------------------------

### Orleans App Configuration and Endpoints

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Sets up the Orleans host with local clustering and memory storage, and defines API endpoints for the root, URL shortening, and redirection. This code forms the core of the Orleans application.

```csharp
// <configuration>
using Orleans.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans(static siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorage("urls");
});

using var app = builder.Build();
// </configuration>

// <endpoints>
app.MapGet("/", static () => "Welcome to the URL shortener, powered by Orleans!");

app.MapGet("/shorten",
    static async (IGrainFactory grains, HttpRequest request, string url) =>
    {
        var host = $"{request.Scheme}://{request.Host.Value}";

        // Validate the URL query string.
        if (string.IsNullOrWhiteSpace(url) ||
            Uri.IsWellFormedUriString(url, UriKind.Absolute) is false)
        {
            return Results.BadRequest($"""
                The URL query string is required and needs to be well formed.
                Consider, ${host}/shorten?url=https://www.microsoft.com.
                """);
        }

        // Create a unique, short ID
        var shortenedRouteSegment = Guid.NewGuid().GetHashCode().ToString("X");

        // Create and persist a grain with the shortened ID and full URL
        var shortenerGrain =
            grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);

        await shortenerGrain.SetUrl(url);

        // Return the shortened URL for later use
        var resultBuilder = new UriBuilder(host)
        {
            Path = $"/go/{shortenedRouteSegment}"
        };

        return Results.Ok(resultBuilder.Uri);
    });

app.MapGet("/go/{shortenedRouteSegment:required}",
    static async (IGrainFactory grains, string shortenedRouteSegment) =>
    {
        // Retrieve the grain using the shortened ID and url to the original URL
        var shortenerGrain =
            grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);

        var url = await shortenerGrain.GetUrl();

        // Handles missing schemes, defaults to "http://".
        var redirectBuilder = new UriBuilder(url);

        return Results.Redirect(redirectBuilder.Uri.ToString());
    });

app.Run();
// </endpoints>
```

--------------------------------

### Custom Logging for Cancellation Tracking

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Implement custom logging within a grain method to track operation duration and log cancellation events, including the time elapsed before cancellation.

```csharp
// Example: Custom logging for cancellation tracking
public async Task MonitoredOperationAsync(CancellationToken cancellationToken = default)
{
    var stopwatch = Stopwatch.StartNew();

    try
    {
        await DoWorkAsync(cancellationToken);
        logger.LogInformation("Operation completed in {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
    }
    catch (OperationCanceledException)
    {
        logger.LogInformation("Operation canceled after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
        throw;
    }
}
```

--------------------------------

### Update ADO.NET Provider References

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Switch from `System.Data.SqlClient` to `Microsoft.Data.SqlClient` for ADO.NET providers. Update project references and the invariant name accordingly.

```xml
<!-- Remove -->
<PackageReference Include="System.Data.SqlClient" Version="..." />

<!-- Add -->
<PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.0" />

```

```csharp
// Orleans 7.x
options.Invariant = "System.Data.SqlClient";

// Orleans 10.0
options.Invariant = "Microsoft.Data.SqlClient";

```

--------------------------------

### Register Custom Placement Director

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

Register the custom placement director with the ISiloHost during configuration. This links the custom strategy to its director implementation.

```csharp
private static async Task<ISiloHost> StartSilo()
{
    var builder = new HostBuilder(c =>
    {
        // normal configuration methods omitted for brevity
        c.ConfigureServices(ConfigureServices);
    });

    var host = builder.Build();
    await host.StartAsync();

    return host;
}

private static void ConfigureServices(IServiceCollection services)
{
    services.AddPlacementDirector<SamplePlacementStrategy, SamplePlacementStrategyFixedSiloDirector>();
}
```

--------------------------------

### Orleans Silo Project Package References

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Include these package references in your Orleans silo project for integration with Aspire and Redis clustering.

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Orleans.Server" Version="10.1.0" />
  <PackageReference Include="Microsoft.Orleans.Clustering.Redis" Version="10.1.0" />
  <PackageReference Include="Aspire.StackExchange.Redis" Version="13.2.3" />
</ItemGroup>
```

--------------------------------

### ReadStateAsync Implementation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Reads grain state from a file. If the file does not exist, it initializes the state with a default instance. Otherwise, it deserializes the state from the file content.

```csharp
public async Task ReadStateAsync<T>(
    string stateName,
    GrainId grainId,
    IGrainState<T> grainState)
{
    var fName = GetKeyString(stateName, grainId);
    var path = Path.Combine(_options.RootDirectory, fName!);
    var fileInfo = new FileInfo(path);
    if (fileInfo is { Exists: false })
    {
        grainState.State = (T)Activator.CreateInstance(typeof(T))!;
        return;
    }

    using var stream = fileInfo.OpenText();
    var storedData = await stream.ReadToEndAsync();
    
    grainState.State = _options.GrainStorageSerializer.Deserialize<T>(new BinaryData(storedData));
    grainState.ETag = fileInfo.LastWriteTimeUtc.ToString();
}
```

--------------------------------

### Migrate Grain Call Filters from Orleans 7.x to 10.0

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Replace `AddGrainCallFilter` with `AddIncomingGrainCallFilter` for silo builders or `AddOutgoingGrainCallFilter` for client builders. Use `AddIncomingGrainCallFilter` for incoming calls and `AddOutgoingGrainCallFilter` for outgoing calls.

```csharp
// Orleans 7.x (no longer works)
services.AddGrainCallFilter(new MyFilter());
services.AddGrainCallFilter<MyFilter>();

// Orleans 10.0
siloBuilder.AddIncomingGrainCallFilter(new MyFilter());
siloBuilder.AddIncomingGrainCallFilter<MyFilter>();

// Or using a delegate
siloBuilder.AddIncomingGrainCallFilter(async context =>
{
    // Before grain call
    await context.Invoke();
    // After grain call
});

```

```csharp
siloBuilder.AddOutgoingGrainCallFilter<MyOutgoingFilter>();
clientBuilder.AddOutgoingGrainCallFilter<MyOutgoingFilter>();

```

--------------------------------

### Production Orleans Configuration with Redis Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configures Orleans to use Redis for clustering in a production environment by referencing an existing Redis connection string.

```csharp
public static void ProductionConfig(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    // Use existing Azure Cache for Redis
    var redis = builder.AddConnectionString("orleans-redis");

    var orleans = builder.AddOrleans("cluster")
        .WithClustering(redis);

    // ...
}
```

--------------------------------

### Orleans Service Lifecycle Stages

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/silo-lifecycle

Defines the standard stages used in the Orleans service lifecycle for both silos and cluster clients. These stages represent different phases of initialization and operation.

```csharp
public static class ServiceLifecycleStage
{
    public const int First = int.MinValue;
    public const int RuntimeInitialize = 2_000;
    public const int RuntimeServices = 4_000;
    public const int RuntimeStorageServices = 6_000;
    public const int RuntimeGrainServices = 8_000;
    public const int ApplicationServices = 10_000;
    public const int BecomeActive = Active - 1;
    public const int Active = 20_000;
    public const int Last = int.MaxValue;
}
```

--------------------------------

### Staging Environment Configuration in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/deploying-new-versions-of-grains

Configures Orleans for a staging environment deployment by setting the default compatibility strategy to BackwardCompatible and the default version selector strategy to MinimumVersion. This approach is used when deploying new silo versions to a separate staging environment before a full production rollout.

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.Configure<GrainVersioningOptions>(options =>
    {
        options.DefaultCompatibilityStrategy = nameof(BackwardCompatible);
        options.DefaultVersionSelectorStrategy = nameof(MinimumVersion);
    });
});

using var host = builder.Build();
await host.RunAsync();

```

--------------------------------

### Configure Azure Table and Blob Storage with Managed Identity

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Configure Azure Table and Blob storage providers using `DefaultAzureCredential` for authentication. This is the recommended approach for production environments.

```csharp
var tableEndpoint = new Uri(configuration["AZURE_TABLE_STORAGE_ENDPOINT"]!);
var blobEndpoint = new Uri(configuration["AZURE_BLOB_STORAGE_ENDPOINT"]!);
var credential = new DefaultAzureCredential();

var builder = Host.CreateApplicationBuilder();
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddAzureTableGrainStorage(
        name: "profileStore",
        configureOptions: options =>
        {
            options.TableServiceClient = new TableServiceClient(tableEndpoint, credential);
        })
        .AddAzureBlobGrainStorage(
            name: "cartStore",
            configureOptions: options =>
            {
                options.BlobServiceClient = new BlobServiceClient(blobEndpoint, credential);
            });
});

using var host = builder.Build();


```

--------------------------------

### Call Asynchronous Library Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

Call an asynchronous library method directly using the await keyword. This is the standard and recommended approach for asynchronous operations in .NET.

```csharp
await the library call
```

--------------------------------

### Specify Custom Log Consistency Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/event-sourcing-configuration

Use the `LogConsistencyProviderAttribute` to specify a custom log-consistency provider for a grain. The `ProviderName` should match the configuration.

```csharp
[LogConsistencyProvider(ProviderName = "CustomStorage")]
public class ChatGrain :
    JournaledGrain<XDocument, IChatEvent>, IChatGrain, ICustomStorage
{
    // ...
}
```

--------------------------------

### Inject IConnectionMultiplexer for Redis Grain Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

For advanced scenarios, inject the IConnectionMultiplexer directly using the CreateMultiplexer delegate. This allows resolving the IConnectionMultiplexer from dependency injection.

```csharp
// Register the Redis client with keyed services.
// Orleans providers look up resources by their keyed service name.
// builder.AddKeyedRedisClient("orleans-redis");

siloBuilder.AddRedisGrainStorage("redis");
siloBuilder.Services.AddOptions<Orleans.Persistence.RedisStorageOptions>("redis")
    .Configure<IServiceProvider>((options, sp) =>
    {
        options.CreateMultiplexer = _ =>
        {
            // Resolve the IConnectionMultiplexer from DI (provided by Aspire)
            return Task.FromResult(sp.GetRequiredService<IConnectionMultiplexer>());
        };
    });

```

--------------------------------

### AtmGrain Implementation for Transfers

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

This grain implementation handles fund transfers between accounts by calling `Withdraw` and `Deposit` methods on `IAccountGrain` interfaces. It uses `Task.WhenAll` to execute both operations concurrently.

```csharp
namespace TransactionalExample.Grains;

[StatelessWorker]
public class AtmGrain : Grain, IAtmGrain
{
    public Task Transfer(
        string fromId,
        string toId,
        decimal amount) =>
        Task.WhenAll(
            GrainFactory.GetGrain<IAccountGrain>(fromId).Withdraw(amount),
            GrainFactory.GetGrain<IAccountGrain>(toId).Deposit(amount));
}
```

--------------------------------

### Configure Azure Storage Clustering with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/client-configuration

Configure the Azure Table storage clustering provider using a connection string. This is an alternative to using DefaultAzureCredential for specifying the storage account details.

```csharp
.UseAzureStorageClustering(
    options => options.ConfigureTableServiceClient(connectionString));
```

--------------------------------

### Configure Azure Table Clustering with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Configure Azure Table storage for clustering using a direct connection string. This method is suitable when explicit connection string management is preferred.

```csharp
siloBuilder.UseAzureStorageClustering(
    options => options.ConfigureTableServiceClient(connectionString))

```

--------------------------------

### Implement URL Shortener Grain with Persistent State

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Implement the URL shortener grain, inheriting from Grain and the custom interface. It uses IPersistentState to manage the grain's state, persisting URL details to the configured memory storage.

```csharp
public sealed class UrlShortenerGrain(
    [PersistentState(
        stateName: "url",
        storageName: "urls")]
        IPersistentState<UrlDetails> state)
    : Grain, IUrlShortenerGrain
{
    public async Task SetUrl(string fullUrl)
    {
        state.State = new()
        {
            ShortenedRouteSegment = this.GetPrimaryKeyString(),
            FullUrl = fullUrl
        };

        await state.WriteStateAsync();
    }

    public Task<string> GetUrl() =>
        Task.FromResult(state.State.FullUrl);
}

[GenerateSerializer, Alias(nameof(UrlDetails))]
public sealed record class UrlDetails
{
    [Id(0)]
    public string FullUrl { get; set; } = "";

    [Id(1)]
    public string ShortenedRouteSegment { get; set; } = "";
}

```

--------------------------------

### Customize Kubernetes environment properties

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/kubernetes

Optionally customize properties of the Kubernetes environment, such as the Helm chart name, within your AppHost configuration.

```csharp
builder.AddKubernetesEnvironment("k8s")
    .WithProperties(k8s =>
    {
        k8s.HelmChartName = "my-orleans-app";
    });

```

--------------------------------

### Define Lifecycle Observer Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/orleans-lifecycle

The ILifecycleObserver interface defines the methods for performing startup and shutdown operations. These methods are called when the subscribed stage is reached during the component's lifecycle.

```csharp
public interface ILifecycleObserver
{
    Task OnStart(CancellationToken ct);
    Task OnStop(CancellationToken ct);
}

```

--------------------------------

### Configure Gateway Refresh Period

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Configure the GatewayListRefreshPeriod for a client to control how often it refreshes its list of available gateways. The default is 1 minute.

```csharp
var client = new ClientBuilder()
    // ...
    .Configure<GatewayOptions>(
        options =>                         // Default is 1 min.
        options.GatewayListRefreshPeriod = TimeSpan.FromMinutes(10))
    .Build();

```

--------------------------------

### AccountGrain Implementation with Transactional State

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

Implement a grain that uses ITransactionalState to manage its state. Mark the grain with ReentrantAttribute for correct transaction context propagation.

```csharp
namespace TransactionalExample.Grains;

[Reentrant]
public class AccountGrain : Grain, IAccountGrain
{
    private readonly ITransactionalState<Balance> _balance;

    public AccountGrain(
        [TransactionalState(nameof(balance))]
        ITransactionalState<Balance> balance) =>
        _balance = balance ?? throw new ArgumentNullException(nameof(balance));

    public Task Deposit(decimal amount) =>
        _balance.PerformUpdate(
            balance => balance.Value += amount);

    public Task Withdraw(decimal amount) =>
        _balance.PerformUpdate(balance =>
        {
            if (balance.Value < amount)
            {
                throw new InvalidOperationException(
                    $"Withdrawing {amount} credits from account " +
                    $"\"{this.GetPrimaryKeyString()}\" would overdraw it."
                    + $" This account has {balance.Value} credits.");
            }

            balance.Value -= amount;
        });

    public Task<decimal> GetBalance() =>
        _balance.PerformRead(balance => balance.Value);
}
```

--------------------------------

### Define Orleans Grain Interface and Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Defines a grain interface `IPlayerGrain` and its implementation `PlayerGrain`. Use this pattern to establish communication contracts and logic for your grains.

```csharp
public interface IPlayerGrain : IGrainWithGuidKey
{
    Task<IGameGrain> GetCurrentGame(CancellationToken cancellationToken = default);

    Task JoinGame(IGameGrain game, CancellationToken cancellationToken = default);

    Task LeaveGame(IGameGrain game, CancellationToken cancellationToken = default);
}

public class PlayerGrain : Grain, IPlayerGrain
{
    private IGameGrain _currentGame;

    // Game the player is currently in. May be null.
    public Task<IGameGrain> GetCurrentGame(CancellationToken cancellationToken = default)
    {
       return Task.FromResult(_currentGame);
    }

    // Game grain calls this method to notify that the player has joined the game.
    public Task JoinGame(IGameGrain game, CancellationToken cancellationToken = default)
    {
       _currentGame = game;

       Console.WriteLine(
           $"Player {GetPrimaryKey()} joined game {game.GetPrimaryKey()}");

       return Task.CompletedTask;
    }

   // Game grain calls this method to notify that the player has left the game.
   public Task LeaveGame(IGameGrain game, CancellationToken cancellationToken = default)
   {
       _currentGame = null;

       Console.WriteLine(
           $"Player {GetPrimaryKey()} left game {game.GetPrimaryKey()}");

       return Task.CompletedTask;
   }
}
```

--------------------------------

### Configure Orleans Logging Level Programmatically

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/troubleshooting-deployments

Set the logging severity level for Orleans to 'Info' when creating configuration objects. This ensures more detailed logs are captured.

```csharp
var config = new ClusterConfiguration();
config.Defaults.DefaultTraceLevel = Severity.Info;
```

```csharp
var config = new ClientConfiguration();
config.DefaultTraceLevel = Severity.Info;
```

--------------------------------

### Outgoing Logging Call Filter Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

A sample implementation of IOutgoingGrainCallFilter that logs method calls and exceptions. It demonstrates accessing grain type, method name, and result.

```csharp
public class OutgoingLoggingCallFilter : IOutgoingGrainCallFilter
{
    private readonly ILogger<OutgoingLoggingCallFilter> _logger;

    public OutgoingLoggingCallFilter(ILogger<OutgoingLoggingCallFilter> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(IOutgoingGrainCallContext context)
    {
        try
        {
            await context.Invoke();

            _logger.LogInformation(
                "{GrainType}.{MethodName} returned value {Result}",
                context.Grain.GetType(),
                context.MethodName,
                context.Result);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "{GrainType}.{MethodName} threw an exception",
                context.Grain.GetType(),
                context.MethodName);

            // If this exception is not re-thrown, it is considered to be
            // handled by this filter.
            throw;
        }
    }
}
```

--------------------------------

### Configure Silo TLS for Development with Self-Signed Certificates

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/transport-layer-security

This configuration is for development and testing environments, enabling TLS with relaxed validation using self-signed certificates. Avoid using these settings in production.

```csharp
var hostBuilder = Host.CreateDefaultBuilder()
    .UseEnvironment(Environments.Development);

using IHost host = hostBuilder
    .UseOrleans((context, builder) =>
    {
        var isDevelopment = context.HostingEnvironment.IsDevelopment();

        builder
            .UseLocalhostClustering()
            .UseTls(StoreName.My, "localhost", allowInvalid: isDevelopment, StoreLocation.CurrentUser, options =>
            {
                options.OnAuthenticateAsClient = (connection, sslOptions) =>
                {
                    sslOptions.TargetHost = "localhost";
                };

                if (isDevelopment)
                {
                    options.AllowAnyRemoteCertificate();
                }
            });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.RunAsync();

```

--------------------------------

### Implement GrainDeactivateExtension

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-extensions

Implement the IGrainDeactivateExtension interface. The IGrainContext is injected via dependency injection to access the grain's context for deactivation.

```csharp
public sealed class GrainDeactivateExtension : IGrainDeactivateExtension
{
    private IGrainContext _context;

    public GrainDeactivateExtension(IGrainContext context)
    {
        _context = context;
    }

    public Task Deactivate(string msg)
    {
        var reason = new DeactivationReason(DeactivationReasonCode.ApplicationRequested, msg);
        _context.Deactivate(reason);
        return Task.CompletedTask;
    }
}
```

--------------------------------

### Alternative Unreliable Silo Deployment on Dedicated Servers

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

An alternative configuration for testing silos on dedicated servers, using `SiloHostBuilder` and `UseDevelopmentClustering`.

```csharp
var primarySiloEndpoint = new IPEndPoint(PRIMARY_SILO_IP_ADDRESS, 11_111);
var silo = new SiloHostBuilder()
    .UseDevelopmentClustering(primarySiloEndpoint)
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .ConfigureEndpoints(siloPort: 11_111, gatewayPort: 30_000)
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();


```

--------------------------------

### User Grain with Persistent State

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Implement a grain that uses `IPersistentState` to manage user profile data. This pattern is useful for storing mutable state that needs to be persisted across grain activations.

```csharp
public class UserGrainComplete : Grain, IUserGrain
{
    private readonly IPersistentState<ProfileState> _profile;

    public UserGrainComplete(
        [PersistentState("profile", "profileStore")]
        IPersistentState<ProfileState> profile)
    {
        _profile = profile;
    }

    public Task<string> GetNameAsync() => Task.FromResult(_profile.State.Name);

    public async Task SetNameAsync(string name)
    {
        _profile.State.Name = name;
        await _profile.WriteStateAsync();
    }
}
```

--------------------------------

### Access Silo Metadata at Runtime with ISiloMetadataCache

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Inject and use ISiloMetadataCache within a grain to retrieve metadata for any silo, such as its zone, at runtime. This allows for dynamic decision-making based on silo properties.

```csharp
public class MyGrain : Grain, IMyGrain
{
    private readonly ISiloMetadataCache _metadataCache;

    public MyGrain(ISiloMetadataCache metadataCache)
    {
        _metadataCache = metadataCache;
    }

    public Task<string?> GetCurrentSiloZone()
    {
        var siloAddress = this.GetSiloAddress();
        var metadata = _metadataCache.GetMetadata(siloAddress);
        return Task.FromResult(metadata?.Metadata.GetValueOrDefault("zone"));
    }
```

--------------------------------

### Configure Cosmos DB as Default Grain Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Configure Azure Cosmos DB as the default grain storage provider by using the AddCosmosGrainStorageAsDefault method. This simplifies configuration when Cosmos DB is the primary storage.

```csharp
siloBuilder.AddCosmosGrainStorageAsDefault(options =>
{
    options.ConfigureCosmosClient(
        "https://myaccount.documents.azure.com:443/",
        new DefaultAzureCredential());
    options.IsResourceCreationEnabled = true;
});

```

--------------------------------

### Register Azure Queue Storage Client in Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/stream-providers

In the Silo project, register the keyed Azure Queue Storage client. This is crucial for Orleans to resolve the queue client for streaming.

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedAzureQueueServiceClient("streaming");
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### Correct LeaseAcquisitionPeriod Typo in Orleans 10.0

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

The misspelled `LeaseAquisitionPeriod` property in `LeaseBasedQueueBalancerOptions` has been corrected to `LeaseAcquisitionPeriod` in Orleans 10.0.

```csharp
// Orleans 7.x (typo)
options.LeaseAquisitionPeriod = TimeSpan.FromSeconds(30);

// Orleans 10.0 (corrected)
options.LeaseAcquisitionPeriod = TimeSpan.FromSeconds(30);

```

--------------------------------

### Disambiguating Grains with Grain Class Name Prefix

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

Provide a prefix matching the grain class name to the `GetGrain` method to specify which implementation to use when ambiguity exists. This is useful when you don't want to introduce new interfaces.

```csharp
ICounterGrain myUpCounter = grainFactory.GetGrain<ICounterGrain>("my-counter", grainClassNamePrefix: "Up");
ICounterGrain myDownCounter = grainFactory.GetGrain<ICounterGrain>("my-counter", grainClassNamePrefix: "Down");
```

--------------------------------

### View AWS credentials file

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/dynamodb-storage

Display the content of the AWS credentials file to identify profile names and keys. This is useful for configuring DynamoDB persistence with named profiles.

```bash
cat ~/.aws/credentials

```

--------------------------------

### Configure Random Placement Strategy in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

If your application relies on the RandomPlacement strategy, explicitly configure it in your silo builder services or on specific grains.

```csharp
siloBuilder.Services.AddSingleton<PlacementStrategy, RandomPlacement>();
```

```csharp
// Or on specific grains
[RandomPlacement]
public class MyGrain : Grain, IMyGrain { }
```

--------------------------------

### Resume Explicit Subscriptions in OnActivateAsync

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

When a grain reactivates, it needs to resume its explicit subscriptions. This code retrieves all existing subscription handles and calls ResumeAsync on each to re-attach processing logic.

```csharp
public override async Task OnActivateAsync(CancellationToken cancellationToken)
{
    var streamProvider = this.GetStreamProvider(PROVIDER_NAME);
    var streamId = StreamId.Create("MyStreamNamespace", this.GetPrimaryKey());
    var stream = streamProvider.GetStream<string>(streamId);

    var subscriptionHandles = await stream.GetAllSubscriptionHandles();
    foreach (var handle in subscriptionHandles)
    {
        await handle.ResumeAsync(this);
    }
}
```

--------------------------------

### Registering Component with IGrainContext Factory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Register a component in the service container using its factory function that takes `IGrainContext`. This allows grains to depend on the component and have it automatically participate in their lifecycle.

```csharp
services.AddTransient<MyComponent>(sp =>
    MyComponent.Create(sp.GetRequiredService<IGrainContext>()));
```

--------------------------------

### GetKeyString Method for Generating Storage Keys

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Generates a unique string key for storing grain state based on service ID, grain ID, and grain type.

```csharp
private string GetKeyString(string grainType, GrainId grainId) =>
    $"{_clusterOptions.ServiceId}.{grainId.Key}.{grainType}";
```

--------------------------------

### Configure Azure Cosmos DB Grain Storage Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-cosmos-db

Use the `AddCosmosGrainStorage` extension method to configure the grain persistence provider. This allows customization of database/container names, throughput, and client credentials.

```csharp
siloBuilder.AddCosmosGrainStorage(
    name: "profileStore",
    configureOptions: static options =>
    {
        options.IsResourceCreationEnabled = true;
        options.DatabaseName = "OrleansAlternativeDatabase";
        options.ContainerName = "OrleansStorageAlternativeContainer";
        options.ContainerThroughputProperties = ThroughputProperties.CreateAutoscaleThroughput(1000);
        options.ConfigureCosmosClient("<azure-cosmos-db-nosql-connection-string>");
    });

```

--------------------------------

### Map Orleans Dashboard to Custom Route

Source: https://learn.microsoft.com/en-us/dotnet/orleans/dashboard

Host the dashboard at a custom path by specifying a route prefix when mapping the dashboard endpoints.

```csharp
// Map dashboard endpoints at /dashboard
app.MapOrleansDashboard(routePrefix: "/dashboard");

```

--------------------------------

### Automatic Migration with Grain<TState>

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Grains inheriting from Grain<TState> automatically support migration. Their state is serialized and restored without additional code.

```csharp
// Automatic migration support via Grain<TState>
public class MyStatefulGrain : Grain<MyGrainState>, IMyGrain
{
    public Task UpdateValue(int value)
    {
        State.Value = value;
        return WriteStateAsync();
    }
}
```

--------------------------------

### Register Logging Filter with Extension Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Register the LoggingCallFilter class as a silo-wide grain call filter using the AddIncomingGrainCallFilter extension method.

```csharp
siloHostBuilder.AddIncomingGrainCallFilter<LoggingCallFilter>();

```

--------------------------------

### Configure Grain Profiler Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/dashboard

Configure the grain profiler to always collect profiling data or set a deactivation time for when the dashboard is inactive. This ensures continuous performance data collection.

```csharp
builder.Services.Configure<GrainProfilerOptions>(options =>
{
    // Always collect profiling data, even when dashboard is inactive
    options.TraceAlways = true;

    // Time after which profiling stops if dashboard is inactive
    options.DeactivationTime = TimeSpan.FromMinutes(5);
});

```

--------------------------------

### Configure Azure Table Reminder Service

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Use this snippet to configure the Azure Table Storage provider for Orleans reminders. Replace 'YOUR_CONNECTION_STRING_HERE' with your actual Azure Table Storage connection string.

```csharp
public static async Task ConfigureAzureTableAsync(string[] args)
{
    // TODO replace with your connection string
    const string connectionString = "YOUR_CONNECTION_STRING_HERE";

    var builder = Host.CreateApplicationBuilder(args);
    builder.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseAzureTableReminderService(connectionString);
    });

    using var host = builder.Build();
    await host.RunAsync();
}
```

--------------------------------

### Minimal Azure Queue Streams Configuration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/streams-implementation/azure-queue-streams

Configures Azure Queue Streams with a connection string and specifies queue names. Also configures cache size and pulling agent timer period. A PubSubStore, like Azure Table Storage, might be required.

```csharp
hostBuilder
    .AddAzureQueueStreams("AzureQueueProvider", configurator =>
    {
        configurator.ConfigureAzureQueue(
            ob => ob.Configure(options =>
            {
                options.ConnectionString = "[PLACEHOLDER]";
                options.QueueNames = new List<string> { "yourprefix-azurequeueprovider-0" };
            }));
    configurator.ConfigureCacheSize(1024);
    configurator.ConfigurePullingAgent(ob => ob.Configure(options =>
    {
      options.GetQueueMsgsTimerPeriod = TimeSpan.FromMilliseconds(200);
    }));
  })
  // a PubSubStore could be needed, as example Azure Table Storage
  .AddAzureTableGrainStorage("PubSubStore", options => {
    options.ConnectionString = "[PLACEHOLDER]";
  })

```

--------------------------------

### Registering Component with IGrainActivationContext Factory

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Register a component in the service container using its factory function that takes `IGrainActivationContext`. This ensures the component is correctly integrated into the grain's lifecycle.

```csharp
services.AddTransient<MyComponent>(sp =>
    MyComponent.Create(sp.GetRequiredService<IGrainActivationContext>()));
```

--------------------------------

### Grain Interface with Multiple CancellationToken Parameters (Incorrect)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

This code demonstrates an incorrect way to define a grain interface method with multiple CancellationToken parameters, which will result in a build error (ORLEANS0109).

```csharp
public interface IMyGrain : IGrainWithGuidKey
{
    // This will cause ORLEANS0109 error
    Task ProcessAsync(CancellationToken token1, CancellationToken token2);
}
```

--------------------------------

### Read Grain State from File

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Reads a grain's state from a file, deserializing it using configured JSON settings. If the file doesn't exist, it initializes the state.

```csharp
public async Task ReadStateAsync(
    string grainType,
    GrainReference grainReference,
    IGrainState grainState)
{
    var fName = GetKeyString(grainType, grainReference);
    var path = Path.Combine(_options.RootDirectory, fName);

    var fileInfo = new FileInfo(path);
    if (!fileInfo.Exists)
    {
        grainState.State = Activator.CreateInstance(grainState.State.GetType());
        return;
    }

    using (var stream = fileInfo.OpenText())
    {
        var storedData = await stream.ReadToEndAsync();
        grainState.State = JsonConvert.DeserializeObject(storedData, _jsonSettings);
    }

    grainState.ETag = fileInfo.LastWriteTimeUtc.ToString();
}
```

--------------------------------

### Configure Azure Table Storage for Pub-Sub with Managed Identity

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Configures the Pub-Sub component to use Azure Table Storage with managed identity for storing subscription data. Requires an endpoint URI and Azure credentials.

```csharp
var endpoint = new Uri(configuration["AZURE_TABLE_STORAGE_ENDPOINT"]!);
var credential = new DefaultAzureCredential();

hostBuilder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddAzureTableGrainStorage("PubSubStore",
        options => options.TableServiceClient = new TableServiceClient(endpoint, credential));
});

```

--------------------------------

### IAsyncObserver and IAsyncObservable Interfaces

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

These interfaces define the contract for observing and being observed by streams. IAsyncObserver handles incoming events, completion, and errors, while IAsyncObservable provides the method to subscribe.

```csharp
public interface IAsyncObserver<in T>
{
    Task OnNextAsync(T item, StreamSequenceToken token = null);
    Task OnCompletedAsync();
    Task OnErrorAsync(Exception ex);
}

public interface IAsyncObservable<T>
{
    Task<StreamSubscriptionHandle<T>> SubscribeAsync(IAsyncObserver<T> observer);
}
```

--------------------------------

### IPersistentState<TState> and IStorage Interfaces

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Defines the interfaces for interacting with persistent grain state. IPersistentState<TState> extends IStorage<TState>, which in turn extends IStorage. These interfaces outline methods for reading, writing, clearing state, and accessing state properties like Etag and RecordExists.

```csharp
public interface IPersistentState<TState> : IStorage<TState>
{
}

public interface IStorage<TState> : IStorage
{
    TState State { get; set; }
}

public interface IStorage
{
    string Etag { get; }

    bool RecordExists { get; }

    Task ClearStateAsync();

    Task WriteStateAsync();

    Task ReadStateAsync();
}
```

```csharp
public interface IPersistentState<TState> where TState : new()
{
    TState State { get; set; }

    string Etag { get; }

    Task ClearStateAsync();

    Task WriteStateAsync();

    Task ReadStateAsync();
}
```

--------------------------------

### Use RegisterGrainTimer Extension Methods

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Replace the obsolete `Grain.RegisterTimer` method with the new `RegisterGrainTimer` extension methods for better timer control. Set `Interleave = true` to maintain the old behavior where timer callbacks could interleave with other grain calls.

```csharp
// Orleans 7.x
public override Task OnActivateAsync(CancellationToken cancellationToken)
{
    RegisterTimer(
        callback: DoWork,
        state: null,
        dueTime: TimeSpan.FromSeconds(1),
        period: TimeSpan.FromSeconds(10));
    return Task.CompletedTask;
}

// Orleans 10.0
public override Task OnActivateAsync(CancellationToken cancellationToken)
{
    this.RegisterGrainTimer(
        callback: DoWork,
        state: (object?)null,
        options: new GrainTimerCreationOptions
        {
            DueTime = TimeSpan.FromSeconds(1),
            Period = TimeSpan.FromSeconds(10),
            Interleave = true // Set to true for same behavior as old RegisterTimer
        });
    return Task.CompletedTask;
}

```

--------------------------------

### ConfigureAwait(false) Usage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

Do not use ConfigureAwait(false) inside grain code. It is only allowed within libraries to prevent potential deadlocks and ensure proper context flow.

```csharp
ConfigureAwait(false)
```

--------------------------------

### Consume Async Stream with WithCancellation Extension Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Consumes an `IAsyncEnumerable<T>` stream using the `WithCancellation` extension method. This is useful for applying a cancellation token to an existing async enumerable or when the grain method doesn't explicitly accept a token.

```csharp
var grain = grainFactory.GetGrain<IDataStreamGrain>(Guid.NewGuid());
var asyncEnumerable = grain.StreamDataAsync(1000);

using var cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(10));

try
{
    // WithCancellation passes the token to GetAsyncEnumerator()
    await foreach (var dataPoint in asyncEnumerable.WithCancellation(cts.Token))
    {
        Console.WriteLine($"Received: {dataPoint}");
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Streaming was canceled");
}
```

--------------------------------

### Configure Orleans Client with Azure Storage and DefaultAzureCredential

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/client-configuration

Configures an Orleans client using Azure Storage for clustering. It utilizes DefaultAzureCredential for authentication, which works across local development and production environments. This approach is recommended for secure authentication without storing secrets in configuration.

```csharp
using Azure.Identity;

var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "my-first-cluster";
        options.ServiceId = "MyOrleansService";
    })
    .UseAzureStorageClustering(options =>
    {
        options.ConfigureTableServiceClient(
            new Uri("https://<your-storage-account>.table.core.windows.net"),
            new DefaultAzureCredential());
    });
});

using var host = builder.Build();
await host.StartAsync();

```

--------------------------------

### Server-Side Grain with ObserverManager

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/observers

A server-side grain that uses ObserverManager to handle client subscriptions and unsubscriptions. ObserverManager requires a TimeSpan for cleanup and an ILogger.

```csharp
class HelloGrain : Grain, IHello
{
    private readonly ObserverManager<IChat> _subsManager;

    public HelloGrain(ILogger<HelloGrain> logger)
    {
        _subsManager =
            new ObserverManager<IChat>(
                TimeSpan.FromMinutes(5), logger);
    }

    // Clients call this to subscribe.
    public Task Subscribe(IChat observer)
    {
        _subsManager.Subscribe(observer, observer);

        return Task.CompletedTask;
    }

    //Clients use this to unsubscribe and no longer receive messages.
    public Task UnSubscribe(IChat observer)
    {
        _subsManager.Unsubscribe(observer);

        return Task.CompletedTask;
    }
}
```

--------------------------------

### Specify Grain Directory Plugin Name

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/grain-directory

Use the GrainDirectoryAttribute on a grain class to specify the name of the grain directory plugin to use.

```csharp
[GrainDirectory(GrainDirectoryName = "my-grain-directory")]
public class MyGrain : Grain, IMyGrain
{
    // ...
}

```

--------------------------------

### Configure Dashboard Options

Source: https://learn.microsoft.com/en-us/dotnet/orleans/dashboard

Customize dashboard behavior using DashboardOptions, such as disabling live log streaming or setting update intervals and history length.

```csharp
siloBuilder.AddDashboard(options =>
{
    // Disable the live log streaming endpoint
    options.HideTrace = true;

    // Set the counter update interval (minimum 1000ms)
    options.CounterUpdateIntervalMs = 2000;

    // Set the history buffer length for metrics
    options.HistoryLength = 200;
});

```

--------------------------------

### Subclass StatelessService for Orleans Hosting

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/service-fabric

This class extends Service Fabric's StatelessService to integrate the Orleans host. It overrides CreateServiceInstanceListeners to yield an instance of HostedServiceCommunicationListener.

```csharp
using System.Fabric;
using Microsoft.Extensions.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ServiceFabric.HostingExample;

public sealed class OrleansHostedStatelessService : StatelessService
{
    private readonly Func<StatelessServiceContext, Task<IHost>> _createHost;

    public OrleansHostedStatelessService(
        Func<StatelessServiceContext, Task<IHost>> createHost, StatelessServiceContext serviceContext)
        : base(serviceContext) =>
        _createHost = createHost ?? throw new ArgumentNullException(nameof(createHost));  

    /// <inheritdoc/>
    protected sealed override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
    {
        // Create a listener which creates and runs an IHost
        yield return new ServiceInstanceListener(
            context => new HostedServiceCommunicationListener(() => _createHost(context)),
            nameof(HostedServiceCommunicationListener));
    }
}

```

--------------------------------

### Simulate Potential Deadlock Scenario

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-scheduling

Shows a scenario where two grains call each other simultaneously, which can lead to a deadlock due to the default non-reentrant scheduling.

```csharp
var a = grainFactory.GetGrain("A");
var b = grainFactory.GetGrain("B");

// A calls B at the same time as B calls A.
// This might deadlock, depending on the non-deterministic timing of events.
await Task.WhenAll(a.CallOther(b), b.CallOther(a));
```

--------------------------------

### Connect Orleans Client to Cluster

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Connects a pre-configured Orleans client to the Orleans cluster. This is an asynchronous operation that requires awaiting its completion.

```csharp
await client.Connect();


```

--------------------------------

### IIncomingGrainCallContext Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

The `IIncomingGrainCallContext` interface provides access to the details of an incoming grain call and allows for invocation control. Use this context within your `IIncomingGrainCallFilter` implementation.

```APIDOC
## Interface: IIncomingGrainCallContext

### Description
Provides context and control for an incoming grain call.

### Properties
- **Grain** (IAddressable) - Gets the grain being invoked.
- **InterfaceMethod** (MethodInfo) - Gets the `MethodInfo` for the interface method being invoked.
- **ImplementationMethod** (MethodInfo) - Gets the `MethodInfo` for the implementation method being invoked.
- **Arguments** (object[]) - Gets the arguments for this method invocation.
- **Result** (object) - Gets or sets the result of the invocation.

### Methods
#### Task Invoke()

- **Returns**: A `Task` representing the asynchronous invocation of the next filter or the grain method.

### Usage
Access properties to inspect call details and use `Invoke()` to proceed with the call chain. Modify the `Result` property after `Invoke()` completes.
```

--------------------------------

### Local Development Orleans Configuration with Redis

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/aspire-integration

Configures Orleans to use Redis for clustering in a local development environment. Assumes Redis is running, potentially via a container.

```csharp
public static void LocalDevelopment(string[] args)
{
    var builder = DistributedApplication.CreateBuilder(args);

    var redis = builder.AddRedis("orleans-redis");
    // Redis container runs automatically during development

    var orleans = builder.AddOrleans("cluster")
        .WithClustering(redis);

    // ...
}
```

--------------------------------

### Configure BroadcastChannel

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Configure BroadcastChannel with specified options. FireAndForgetDelivery can be set to false to ensure delivery.

```csharp
builder.AddBroadcastChannel(
    "my-provider",
    options => options.FireAndForgetDelivery = false);

```

--------------------------------

### Return a Task of the same type

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/best-practices

Return a Task directly when the method already returns a Task, such as calling another async method.

```csharp
return foo.Bar();
```

--------------------------------

### Configure Silo Metadata via appsettings.json

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/silo-metadata

Define silo metadata by adding a 'Metadata' section under 'Orleans' in your appsettings.json file. This approach is useful for setting default metadata values.

```json
{
  "Orleans": {
    "Metadata": {
      "cloud.region": "us-east1",
      "compute.reservation.type": "spot",
      "role": "worker"
    }
  }
}

```

--------------------------------

### Define a PingGrain Interface and Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-scheduling

Defines the contract for a grain that can be pinged and can call other grains. This is the default non-reentrant behavior.

```csharp
public interface IPingGrain : IGrainWithStringKey
{
    Task Ping();
    Task CallOther(IPingGrain other);
}

public class PingGrain : Grain, IPingGrain
{
    private readonly ILogger<PingGrain> _logger;

    public PingGrain(ILogger<PingGrain> logger) => _logger = logger;

    public Task Ping() => Task.CompletedTask;

    public async Task CallOther(IPingGrain other)
    {
        _logger.LogInformation("1");
        await other.Ping();
        _logger.LogInformation("2");
    }
}
```

--------------------------------

### Configure Azure Blob Storage with Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-storage

Configure Azure Blob Storage grain persistence using a connection string. It is recommended to use Microsoft Entra ID authentication instead of connection strings in production.

```csharp
siloBuilder.AddAzureBlobGrainStorage(
    name: "profileStore",
    configureOptions =>
    {
        options.ConfigureBlobServiceClient(
             "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1");
    });


```

--------------------------------

### Injecting Persistent State into a Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Demonstrates how to inject named persistent states into a grain using the PersistentStateAttribute. This allows a grain to manage multiple distinct states, each potentially using a different storage provider.

```APIDOC
## Injecting Persistent State into a Grain

### Description
Injects named persistent states into a grain's constructor using the `PersistentStateAttribute`. This allows grains to manage multiple distinct states, each potentially using a different storage provider.

### Example
```csharp
public class UserGrain : Grain, IUserGrain
{
    private readonly IPersistentState<ProfileState> _profile;
    private readonly IPersistentState<CartState> _cart;

    public UserGrain(
        [PersistentState("profile", "profileStore")] IPersistentState<ProfileState> profile,
        [PersistentState("cart", "cartStore")] IPersistentState<CartState> cart)
    {
        _profile = profile;
        _cart = cart;
    }

    public Task<string> GetNameAsync() => Task.FromResult(_profile.State.Name);

    public async Task SetNameAsync(string name)
    {
        _profile.State.Name = name;
        await _profile.WriteStateAsync();
    }
}
```

### Parameters
- **profileStore**: The name of the storage provider configuration for the `profile` state.
- **cartStore**: The name of the storage provider configuration for the `cart` state.
```

--------------------------------

### Inject Grain Service Client into Grains

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

Inject the grain service client into other grains that require access to the grain service. The client handles the communication, potentially across silos.

```csharp
public class MyNormalGrain: Grain<NormalGrainState>, INormalGrain
{
    readonly IDataServiceClient _dataServiceClient;

    public MyNormalGrain(
        IGrainActivationContext grainActivationContext,
        IDataServiceClient dataServiceClient) =>
            _dataServiceClient = dataServiceClient;
}
```

--------------------------------

### Configure DynamoDB grain storage with profile and token

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/dynamodb-storage

Configure the DynamoDB grain persistence provider using a specific profile name and token from your AWS credentials file. This allows for more secure or customized authentication.

```csharp
siloBuilder.AddDynamoDBGrainStorage(
  name: "profileStore",
  configureOptions: options =>
  {
      options.AccessKey = "***";
      options.SecretKey = "***";
      options.Service = "***";
      options.ProfileName = "***";
      options.Token = "***";
  });


```

--------------------------------

### Orleans Silo Lifecycle Participation Interfaces

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/silo-lifecycle

Defines the interfaces `ISiloLifecycle` and `ILifecycleParticipant<TLifecycleObservable>` that allow custom application logic to integrate with the Orleans silo's observable lifecycle.

```csharp
public interface ISiloLifecycle : ILifecycleObservable
{
}

public interface ILifecycleParticipant<TLifecycleObservable>
    where TLifecycleObservable : ILifecycleObservable
{
    void Participate(TLifecycleObservable lifecycle);
}
```

--------------------------------

### ClusterFixture for Reusable TestCluster

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

Implements an xUnit `IDisposable` fixture to manage a `TestCluster` instance, allowing it to be reused across multiple test cases. The cluster is deployed in the constructor and stopped in `Dispose`.

```csharp
using Orleans.TestingHost;

public sealed class ClusterFixture : IDisposable
{
    public TestCluster Cluster { get; }

    public ClusterFixture()
    {
        Cluster = new TestClusterBuilder().Build();
        Cluster.Deploy();
    }

    void IDisposable.Dispose() => Cluster.StopAllSilos();
}
```

--------------------------------

### Configure Orleans Endpoints with Service Fabric

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/service-fabric

This code snippet shows how to retrieve endpoint information from Service Fabric's CodePackageActivationContext and use it to configure Orleans endpoints. Ensure that the endpoint names 'OrleansSiloEndpoint' and 'OrleansProxyEndpoint' are defined in your ServiceManifest.xml.

```csharp
            // configuration using those ports. Gather configuration from 
            // Service Fabric.
            var activation = context.CodePackageActivationContext;
            var endpoints = activation.GetEndpoints();

            // These endpoint names correspond to TCP endpoints 
            // specified in ServiceManifest.xml
            var siloEndpoint = endpoints["OrleansSiloEndpoint"];
            var gatewayEndpoint = endpoints["OrleansProxyEndpoint"];
            var hostname = context.NodeContext.IPAddressOrFQDN;
            builder.ConfigureEndpoints(hostname,
                siloEndpoint.Port, gatewayEndpoint.Port);
        })
        .Build();
}
```

--------------------------------

### Configure Azure Table Storage in Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-storage

Registers the keyed Azure Table Storage client for Orleans in the Silo project's dependency injection container.

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedAzureTableServiceClient("grainstate");
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### Azure Queue Streams Configuration with Custom Data Adapter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/streams-implementation/azure-queue-streams

Configures Azure Queue Streams using a specific data adapter (AzureQueueDataAdapterV2) and provides connection string and queue names.

```csharp
hostBuilder
    .AddAzureQueueStreams<AzureQueueDataAdapterV2>(
        "AzureQueueProvider",
        optionsBuilder => optionsBuilder.Configure(
            options =>
            {
                options.ConnectionString = "[PLACEHOLDER]";
                options.QueueNames =
                    new List<string>
                    {
                        "yourprefix-azurequeueprovider-0"
                    };
            }))

```

--------------------------------

### Client Call with Parameter Renaming Issue

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/backward-compatibility-guidelines

Shows a client call that results in unexpected output when interacting with different versions of a grain due to parameter renaming.

```csharp
var grain = client.GetGrain<IMyGrain>(0);
var result = await grain.Subtract(5, 4); // Will return "-1" instead of expected "1"
```

--------------------------------

### Joining Multiple Grain Method Tasks

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Use `Task.WhenAll` to concurrently invoke multiple grain methods and wait for all of them to complete. This is useful when a grain needs to gather results from several independent operations before proceeding.

```csharp
List<Task> tasks = new List<Task>();
Message notification = CreateNewMessage(text);

foreach (ISubscriber subscriber in subscribers)
{
    tasks.Add(subscriber.Notify(notification));
}

// WhenAll joins a collection of tasks, and returns a joined
// Task that will be resolved when all of the individual notification Tasks are resolved.
Task joinedTask = Task.WhenAll(tasks);

await joinedTask;

// Execution of the rest of the method will continue
// asynchronously after joinedTask is resolve.
```

--------------------------------

### Call Transactional Grain Methods with ITransactionClient

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

Use ITransactionClient to create a transaction context and call transactional grain methods within that context. Ensure Orleans client is configured with UseTransactions().

```csharp
using IHost host = Host.CreateDefaultBuilder(args)
    .UseOrleansClient((_, client) =>
    {
        client.UseLocalhostClustering()
            .UseTransactions();
    })
    .Build();

await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();
var transactionClient= host.Services.GetRequiredService<ITransactionClient>();

var accountNames = new[] { "Xaawo", "Pasqualino", "Derick", "Ida", "Stacy", "Xiao" };
var random = Random.Shared;

while (!Console.KeyAvailable)
{
    // Choose some random accounts to exchange money
    var fromIndex = random.Next(accountNames.Length);
    var toIndex = random.Next(accountNames.Length);
    while (toIndex == fromIndex)
    {
        // Avoid transferring to/from the same account, since it would be meaningless
        toIndex = (toIndex + 1) % accountNames.Length;
    }

    var fromKey = accountNames[fromIndex];
    var toKey = accountNames[toIndex];
    var fromAccount = client.GetGrain<IAccountGrain>(fromKey);
    var toAccount = client.GetGrain<IAccountGrain>(toKey);

    // Perform the transfer and query the results
    try
    {
        var transferAmount = random.Next(200);

        await transactionClient.RunTransaction(
            TransactionOption.Create, 
            async () =>
            {
                await fromAccount.Withdraw(transferAmount);
                await toAccount.Deposit(transferAmount);
            });

        var fromBalance = await fromAccount.GetBalance();
        var toBalance = await toAccount.GetBalance();

        Console.WriteLine(
            $"We transferred {transferAmount} credits from {fromKey} to " +
            $"{toKey}.\n{fromKey} balance: {fromBalance}\n{toKey} balance: {toBalance}\n");
    }
    catch (Exception exception)
    {
        Console.WriteLine(
            $"Error transferring credits from " +
            $ירת{fromKey} to {toKey}: {exception.Message}");

        if (exception.InnerException is { } inner)
        {
            Console.WriteLine($"\tInnerException: {inner.Message}\n");
        }

        Console.WriteLine();
    }

    // Sleep and run again
    await Task.Delay(TimeSpan.FromMilliseconds(200));
}
```

--------------------------------

### Configure Cluster and Service IDs

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/client-configuration

Set the ClusterId for unique cluster identification and ServiceId for application-specific identification. ClusterId allows direct communication between clients and silos, while ServiceId is used by providers like persistence.

```csharp
.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "orleans-docker";
        options.ServiceId = "AspNetSampleApp";
    })
```

--------------------------------

### Implement Timeouts for Work Items

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

Use Task.Delay combined with Task.WhenAny to implement timeouts for executing work items. This pattern allows for graceful handling of long-running operations.

```csharp
Task.Delay + Task.WhenAny
```

--------------------------------

### Cancellation Timing in Async Iterators

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Demonstrates how to effectively use `CancellationToken` within an `IAsyncEnumerable<T>` to ensure operations are responsive to cancellation requests. Checks are placed before processing items, after long-running operations, and used with cancellation-aware methods like `Task.Delay`.

```csharp
public async IAsyncEnumerable<DataPoint> StreamDataAsync(
    int count,
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
{
    for (int i = 0; i < count; i++)
    {
        // Good: Check before each item
        cancellationToken.ThrowIfCancellationRequested();

        var data = await ProcessItemAsync(i);

        // Good: Check after long-running operations
        cancellationToken.ThrowIfCancellationRequested();

        yield return data;

        // Good: Use cancellation-aware operations
        await Task.Delay(100, cancellationToken);
    }
}
```

--------------------------------

### Define Observer Interface with CancellationToken

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/observers

Define an observer interface by adding a CancellationToken parameter as the last parameter in your observer interface method. This enables grains to signal cancellation to observers.

```csharp
public interface IDataObserver : IGrainObserver
{
    Task OnDataReceivedAsync(DataPayload data, CancellationToken cancellationToken = default);
}
```

--------------------------------

### IGrainStorage Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

The primary interface that storage providers must implement to handle Orleans grain state persistence.

```APIDOC
## IGrainStorage Interface

### Description
Interface to be implemented for a storage able to read and write Orleans grain state data.

### Methods

#### ReadStateAsync

*   **Description**: Read data function for this storage instance.
*   **Parameters**:
    *   `grainType` (string) - Type of this grain [fully qualified class name]
    *   `grainReference` (GrainReference) - Grain reference object for this grain.
    *   `grainState` (IGrainState) - State data object to be populated for this grain.
*   **Returns**: Completion promise for the Read operation on the specified grain.

#### WriteStateAsync

*   **Description**: Write data function for this storage instance.
*   **Parameters**:
    *   `grainType` (string) - Type of this grain [fully qualified class name]
    *   `grainReference` (GrainReference) - Grain reference object for this grain.
    *   `grainState` (IGrainState) - State data object to be written for this grain.
*   **Returns**: Completion promise for the Write operation on the specified grain.

#### ClearStateAsync

*   **Description**: Delete / Clear data function for this storage instance.
*   **Parameters**:
    *   `grainType` (string) - Type of this grain [fully qualified class name]
    *   `grainReference` (GrainReference) - Grain reference object for this grain.
    *   `grainState` (IGrainState) - Copy of last-known state data object for this grain.
*   **Returns**: Completion promise for the Delete operation on the specified grain.
```

--------------------------------

### Raising and Confirming Events in JournaledGrain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/journaledgrain-basics

To ensure an event is persisted before proceeding, call `RaiseEvent` followed by `await ConfirmEvents()`. Events are eventually confirmed automatically in the background even without explicit confirmation.

```csharp
RaiseEvent(new DepositTransaction()
{
    DepositAmount = amount,
    Description = description
});
await ConfirmEvents();
```

--------------------------------

### Define Version 2 Grain Interface with New Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/backward-compatibility-guidelines

Defines a subsequent version of a grain interface, inheriting from the previous version and adding a new method.

```csharp
[Version(2)]
public interface IMyGrain : IGrainWithIntegerKey
{
    // Method inherited from V1
    Task MyMethod(int arg);

    // New method added in V2
    Task MyNewMethod(int arg, obj o);
}
```

--------------------------------

### Return Task<TResult> from Grain Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Use this when a grain method returns a value. For non-async methods, wrap the return value in Task.FromResult.

```csharp
public Task<SomeType> GrainMethod1()
{
    return Task.FromResult(GetSomeType());
}
```

--------------------------------

### Implement Broadcast Channel Consumer Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/broadcast-channel

Implements a grain that subscribes to a broadcast channel for stock updates. It caches the latest stock prices and handles incoming messages and potential errors.

```csharp
using System.Collections.Concurrent;
using BroadcastChannel.GrainInterfaces;
using Orleans.BroadcastChannel;

namespace BroadcastChannel.Silo;

[ImplicitChannelSubscription]
public sealed class LiveStockGrain :
    Grain,
    ILiveStockGrain,
    IOnBroadcastChannelSubscribed
{
    private readonly IDictionary<StockSymbol, Stock> _stockCache =
        new ConcurrentDictionary<StockSymbol, Stock>();

    public ValueTask<Stock> GetStock(StockSymbol symbol) =>
        _stockCache.TryGetValue(symbol, out Stock? stock) is false
            ? new ValueTask<Stock>(Task.FromException<Stock>(new KeyNotFoundException()))
            : new ValueTask<Stock>(stock);

    public Task OnSubscribed(IBroadcastChannelSubscription subscription) =>
        subscription.Attach<Stock>(OnStockUpdated, OnError);

    private Task OnStockUpdated(Stock stock)
    {
        if (stock is { GlobalQuote: { } })
        {
            _stockCache[stock.GlobalQuote.Symbol] = stock;
        }

        return Task.CompletedTask;
    }

    private static Task OnError(Exception ex)
    {
        Console.Error.WriteLine($"An error occurred: {ex}");

        return Task.CompletedTask;
    }
}

```

--------------------------------

### Configure Azure Blob Storage in Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/azure-storage

Registers the keyed Azure Blob Storage client for Orleans in the Silo project's dependency injection container.

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedAzureBlobServiceClient("grainstate");
builder.UseOrleans();

builder.Build().Run();

```

--------------------------------

### WriteStateAsync Implementation with Version Conflict Check

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Writes grain state to a file. It checks for version conflicts using the ETag (last write time) before saving the new state. If a conflict is detected, it throws an InconsistentStateException.

```csharp
public async Task WriteStateAsync<T>(
    string stateName,
    GrainId grainId,
    IGrainState<T> grainState)
{
    var storedData = _options.GrainStorageSerializer.Serialize(grainState.State);
    var fName = GetKeyString(stateName, grainId);
    var path = Path.Combine(_options.RootDirectory, fName!);
    var fileInfo = new FileInfo(path);
    if (fileInfo.Exists && fileInfo.LastWriteTimeUtc.ToString() != grainState.ETag)
    {
        throw new InconsistentStateException($"""
            Version conflict (WriteState): ServiceId={_clusterOptions.ServiceId}
            ProviderName={_storageName} GrainType={typeof(T)}
            GrainReference={grainId}.
            """);
    }

    await File.WriteAllBytesAsync(path, storedData.ToArray());

    fileInfo.Refresh();
    grainState.ETag = fileInfo.LastWriteTimeUtc.ToString();
}
```

--------------------------------

### Configure Cluster and Service IDs

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Set the ClusterId and ServiceId for Orleans cluster configuration. ClusterId identifies the Orleans cluster, while ServiceId uniquely identifies the application, crucial for providers like persistence.

```csharp
siloBuilder.Configure<ClusterOptions>(options =>
{
    options.ClusterId = "my-first-cluster";
    options.ServiceId = "SampleApp";
})

```

--------------------------------

### Configure Custom Grain Storage Serializer in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Replace the default Newtonsoft.Json serializer for Azure Blob Storage with a custom serializer using OptionsBuilder. This is done by configuring the GrainStorageSerializer property on the provider's options.

```csharp
siloBuilder.AddAzureBlobGrainStorage(
    "MyGrainStorage",
    (OptionsBuilder<AzureBlobStorageOptions> optionsBuilder) =>
    {
        optionsBuilder.Configure<IMySerializer>(
            (options, serializer) => options.GrainStorageSerializer = serializer);
    });

```

--------------------------------

### Resume Stream Subscription in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Resumes a stream subscription with new observer logic. This is crucial for consumer recovery after deactivation.

```csharp
StreamSubscriptionHandle<int> newHandle =
    await subscriptionHandle.ResumeAsync(IAsyncObserver);

```

--------------------------------

### Configure external serializers in XML

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-configuration

Specify external serializer providers in XML configuration under the <SerializationProviders /> property of <Messaging>. The collection is ordered, affecting which provider is used if multiple can handle a type.

```xml
<Messaging>
    <SerializationProviders>
        <Provider type="GreatCompany.FantasticSerializer, GreatCompany.SerializerAssembly" />
    </SerializationProviders>
</Messaging>
```

--------------------------------

### Produce Events to a Stream

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Use the OnNextAsync method on an IAsyncStream handle to publish new events to the stream. This is how producers send data.

```csharp
await stream.OnNextAsync<T>(event)
```

--------------------------------

### Subscribe with Generic Type Arguments

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/orleans-lifecycle

These extension functions allow subscribing to an observable lifecycle using generic type arguments for the observer, simplifying the subscription process when the observer type is known.

```csharp
IDisposable Subscribe<TObserver>(
    this ILifecycleObservable observable,
    int stage,
    Func<CancellationToken, Task> onStart,
    Func<CancellationToken, Task> onStop);

IDisposable Subscribe<TObserver>(
    this ILifecycleObservable observable,
    int stage,
    Func<CancellationToken, Task> onStart);

```

--------------------------------

### Reference Grain by String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-identity

Use this when referencing a grain that uses a string as its primary key from client code.

```csharp
var grain = grainFactory.GetGrain<IExample>("myGrainKey");
```

--------------------------------

### Kubernetes Deployment for Orleans Silos

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/kubernetes

Configure a Kubernetes Deployment to run Orleans silos. Ensure silo names match pod names and set necessary labels and environment variables for Orleans to identify its service, cluster, and running pod.

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: dictionary-app
  labels:
    orleans/serviceId: dictionary-app
spec:
  selector:
    matchLabels:
      orleans/serviceId: dictionary-app
  replicas: 3
  template:
    metadata:
      labels:
        # This label identifies the service to Orleans
        orleans/serviceId: dictionary-app

        # This label identifies an instance of a cluster to Orleans.
        # Typically, this is the same value as the previous label, or any
        # fixed value.
        # In cases where you don't use rolling deployments (for example,
        # blue/green deployments),
        # this value can allow for distinct clusters that don't communicate
        # directly with each other,
        # but still share the same storage and other resources.
        orleans/clusterId: dictionary-app
    spec:
      containers:
        - name: main
          image: my-registry.azurecr.io/my-image
          imagePullPolicy: Always
          ports:
          # Define the ports Orleans uses
          - containerPort: 11111
          - containerPort: 30000
          env:
          # The Azure Storage connection string for clustering is injected as an
          # environment variable.
          # You must create it separately using a command such as:
          # > kubectl create secret generic az-storage-acct `
          #     --from-file=key=./az-storage-acct.txt
          - name: STORAGE_CONNECTION_STRING
            valueFrom:
              secretKeyRef:
                name: az-storage-acct
                key: key
          # Configure settings to let Orleans know which cluster it belongs to
          # and which pod it's running in.
          - name: ORLEANS_SERVICE_ID
            valueFrom:
              fieldRef:
                fieldPath: metadata.labels['orleans/serviceId']
          - name: ORLEANS_CLUSTER_ID
            valueFrom:
              fieldRef:
                fieldPath: metadata.labels['orleans/clusterId']
          - name: POD_NAMESPACE
            valueFrom:
              fieldRef:
                fieldPath: metadata.namespace
          - name: POD_NAME
            valueFrom:
              fieldRef:
                fieldPath: metadata.name
          - name: POD_IP
            valueFrom:
              fieldRef:
                fieldPath: status.podIP
          - name: DOTNET_SHUTDOWNTIMEOUTSECONDS
            value: "120"
          request:
            # Set resource requests
      terminationGracePeriodSeconds: 180
      imagePullSecrets:
        - name: my-image-pull-secret
  minReadySeconds: 60
  strategy:
    rollingUpdate:
      maxUnavailable: 0
      maxSurge: 1

```

--------------------------------

### Replace OrleansConstructorAttribute with GeneratedActivatorConstructorAttribute

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Obsolete `OrleansConstructorAttribute` should be replaced with `GeneratedActivatorConstructorAttribute` or `ActivatorUtilitiesConstructorAttribute` for constructors requiring dependency injection.

```csharp
public interface IMyDependency
{
}

// Orleans 7.x
[GenerateSerializer]
public class MyClass
{
    [Id(0)]
    public string Value { get; set; }

    [OrleansConstructor] // Obsolete and ignored
    public MyClass(IMyDependency dependency)
    {
        Dependency = dependency;
    }

    [field: NonSerialized]
    public IMyDependency Dependency { get; }
}

// Orleans 10.0
[GenerateSerializer]
public class MyClass
{
    [Id(0)]
    public string Value { get; set; }

    [GeneratedActivatorConstructor]
    public MyClass(IMyDependency dependency)
    {
        Dependency = dependency;
    }

    [field: NonSerialized]
    public IMyDependency Dependency { get; }
}

```

--------------------------------

### ICustomGrainStorage Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Interface for custom storage providers to implement for reading, writing, and clearing grain state data.

```APIDOC
## ICustomGrainStorage Interface

### Description
Interface to be implemented for a storage able to read and write Orleans grain state data.

### Methods

#### ReadStateAsync<T>

*   **Description**: Read data function for this storage instance.
*   **Parameters**:
    *   `stateName` (string) - Name of the state for this grain
    *   `grainId` (GrainId) - Grain ID
    *   `grainState` (IGrainState<T>) - State data object to be populated for this grain.
*   **Returns**: Completion promise for the Read operation on the specified grain.

#### WriteStateAsync<T>

*   **Description**: Write data function for this storage instance.
*   **Parameters**:
    *   `stateName` (string) - Name of the state for this grain
    *   `grainId` (GrainId) - Grain ID
    *   `grainState` (IGrainState<T>) - State data object to be written for this grain.
*   **Returns**: Completion promise for the Write operation on the specified grain.

#### ClearStateAsync<T>

*   **Description**: Delete / Clear data function for this storage instance.
*   **Parameters**:
    *   `stateName` (string) - Name of the state for this grain
    *   `grainId` (GrainId) - Grain ID
    *   `grainState` (IGrainState<T>) - Copy of last-known state data object for this grain.
*   **Returns**: Completion promise for the Delete operation on the specified grain.
```

--------------------------------

### Enabling Call Chain Reentrancy with RequestContext

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Demonstrates how to opt-in to call-chain reentrancy for a specific grain method using `RequestContext.AllowCallChainReentrancy()`. This allows downstream calls to reenter the grain that enabled reentrancy, preventing deadlocks in scenarios like `OuterCall` calling `CallMeBack` which then calls back into `InnerCall`.

```csharp
public Task<int> OuterCall(IMyGrain other)
{
    // Allow call-chain reentrancy for this grain, for the duration of the method.
    using var _ = RequestContext.AllowCallChainReentrancy();
    await other.CallMeBack(this.AsReference<IMyGrain>());
}

public Task CallMeBack(IMyGrain grain)
{
    // Because OuterCall allowed reentrancy back into that grain, this method
    // will be able to call grain.InnerCall() without deadlocking.
    await grain.InnerCall();
}

public Task InnerCall() => Task.CompletedTask;
```

--------------------------------

### Define a surrogate for a foreign type in a hierarchy

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Implement both IConverter and IPopulator interfaces for converters handling foreign types that may appear in a type hierarchy. This allows Orleans to correctly serialize derived types.

```csharp
// The foreign type is not sealed, allowing other types to inherit from it.
public class MyForeignLibraryType
{
    public MyForeignLibraryType() { }

    public MyForeignLibraryType(int num, string str, DateTimeOffset dto)
    {
        Num = num;
        String = str;
        DateTimeOffset = dto;
    }

    public int Num { get; set; }
    public string String { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
}

// The surrogate is defined as it was in the previous example.
[GenerateSerializer]
public struct MyForeignLibraryTypeSurrogate
{
    [Id(0)]
    public int Num;

    [Id(1)]
    public string String;

    [Id(2)]
    public DateTimeOffset DateTimeOffset;
}

// Implement the IConverter and IPopulator interfaces on the converter.
[RegisterConverter]
public sealed class MyForeignLibraryTypeSurrogateConverter :
    IConverter<MyForeignLibraryType, MyForeignLibraryTypeSurrogate>,
    IPopulator<MyForeignLibraryType, MyForeignLibraryTypeSurrogate>
{
    public MyForeignLibraryType ConvertFromSurrogate(
        in MyForeignLibraryTypeSurrogate surrogate) =>
        new(surrogate.Num, surrogate.String, surrogate.DateTimeOffset);

    public MyForeignLibraryTypeSurrogate ConvertToSurrogate(
        in MyForeignLibraryType value) =>
        new()
    {
        Num = value.Num,
        String = value.String,
        DateTimeOffset = value.DateTimeOffset
    };

    public void Populate(
        in MyForeignLibraryTypeSurrogate surrogate, MyForeignLibraryType value)
    {
        value.Num = surrogate.Num;
        value.String = surrogate.String;
        value.DateTimeOffset = surrogate.DateTimeOffset;
    }
}

// Application types can inherit from the foreign type, assuming they're not sealed
// since Orleans knows how to serialize it.
[GenerateSerializer]
public sealed class DerivedFromMyForeignLibraryType : MyForeignLibraryType
{
    public DerivedFromMyForeignLibraryType() { }

    public DerivedFromMyForeignLibraryType(
        int intValue, int num, string str, DateTimeOffset dto) : base(num, str, dto)
    {
        IntValue = intValue;
    }

    [Id(0)]
    public int IntValue { get; set; }
}
```

--------------------------------

### Configure Azure Diagnostics for Logging

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/troubleshooting-azure-cloud-services-deployments

In the `diagnostics.wadcfgx` file for web and worker roles, set the `scheduledTransferLogLevelFilter` attribute in the `Logs` element to `Information`. This filters traces sent to the `WADLogsTable` in Azure Storage.

```xml
<Logs scheduledTransferLogLevelFilter="Information" ... />
```

--------------------------------

### Reference Grain by Compound Key

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-identity

When referencing a grain with a compound key, provide both the primary key and the key extension.

```csharp
var grain = grainFactory.GetGrain<IExample>(0, "a string!", null);
```

--------------------------------

### Registering a Cancellation Callback in a Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Register a callback that executes on the grain's scheduler when a CancellationToken is signaled. This allows safe access to grain state within the callback.

```csharp
public async Task ExampleWithCancellationCallbackAsync(CancellationToken cancellationToken = default)
{
    // Register a callback that will run on the grain's scheduler
    cancellationToken.Register(() =>
    {
        // This runs on the grain's execution context
        // Safe to access grain state here
        logger.LogInformation("Operation was canceled for grain {GrainId}", this.GetPrimaryKey());
    });

    // Continue with work...
    await DoWorkAsync(cancellationToken);
}
```

--------------------------------

### Implement IExternalSerializer Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-customization

This interface defines the contract for custom serializers in Orleans. Implement these methods to provide custom serialization logic for specific types.

```csharp
public interface IExternalSerializer
{
    /// <summary>
    /// Initializes the external serializer. Called once when the serialization manager creates
    /// an instance of this type
    /// </summary>
    void Initialize(Logger logger);

    /// <summary>
    /// Informs the serialization manager whether this serializer supports the type for serialization.
    /// </summary>
    /// <param name="itemType">The type of the item to be serialized</param>
    /// <returns>A value indicating whether the item can be serialized.</returns>
    bool IsSupportedType(Type itemType);

    /// <summary>
    /// Tries to create a copy of source.
    /// </summary>
    /// <param name="source">The item to create a copy of</param>
    /// <param name="context">The context in which the object is being copied.</param>
    /// <returns>The copy</returns>
    object DeepCopy(object source, ICopyContext context);

    /// <summary>
    /// Tries to serialize an item.
    /// </summary>
    /// <param name="item">The instance of the object being serialized</param>
    /// <param name="context">The context in which the object is being serialized.</param>
    /// <param name="expectedType">The type that the deserializer will expect</param>
    void Serialize(object item, ISerializationContext context, Type expectedType);

    /// <summary>
    /// Tries to deserialize an item.
    /// </summary>
    /// <param name="context">The context in which the object is being deserialized.</param>
    /// <param name="expectedType">The type that should be deserialized</param>
    /// <returns>The deserialized object</returns>
    object Deserialize(Type expectedType, IDeserializationContext context);
}
```

--------------------------------

### Implement grain method with cancellation checks

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

In your grain implementation, regularly check the cancellation token during long-running operations. Use cancellation token with async operations when possible.

```csharp
public class ProcessingGrain : Grain, IProcessingGrain
{
    public async Task<string> ProcessDataAsync(string data, int chunks, CancellationToken cancellationToken = default)
    {
        // Check cancellation before starting work
        cancellationToken.ThrowIfCancellationRequested();

        var results = new List<string>();

        for (int i = 0; i < chunks; i++)
        {
            // Check cancellation before each chunk
            cancellationToken.ThrowIfCancellationRequested();

            // Process each chunk
            var chunkResult = await ProcessChunkAsync(data, i);
            results.Add(chunkResult);

            // Use cancellation token with async operations when possible
            await Task.Delay(100, cancellationToken);
        }

        return string.Join(", ", results);
    }

    private async Task<string> ProcessChunkAsync(string data, int chunkIndex)
    {
        // Simulate processing work
        await Task.Delay(50);
        return $"{data}_chunk_{chunkIndex}";
    }
}
```

--------------------------------

### Configure Azure Table Storage for Transactions

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

Configure the silo to use Azure Table Storage for transactional state. Requires a connection string environment variable ORLEANS_STORAGE_CONNECTION_STRING.

```csharp
using Azure.Data.Tables;

await Host.CreateDefaultBuilder(args)
    .UseOrleans((_, silo) =>
    {
        silo.UseLocalhostClustering();

        if (Environment.GetEnvironmentVariable(
                "ORLEANS_STORAGE_CONNECTION_STRING") is { } connectionString)
        {
            silo.AddAzureTableTransactionalStateStorage(
                "TransactionStore", 
                options => options.TableServiceClient = new TableServiceClient(connectionString));
        }
        else
        {
            silo.AddMemoryGrainStorageAsDefault();
        }

        silo.UseTransactions();
    })
    .RunConsoleAsync();

```

--------------------------------

### Apply Custom Placement Strategy Attribute to Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement

Tag a grain class with the custom placement attribute to designate it for that specific placement strategy.

```csharp
[SamplePlacementStrategy]
public class MyGrain : Grain, IMyGrain
{
    // ...
}
```

--------------------------------

### Define a Stateless Worker Grain with Max 1 Activation Per Silo

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/stateless-worker-grains

Configure a stateless worker grain to have a maximum of one activation per silo by providing the desired number to the StatelessWorkerAttribute.

```csharp
[StatelessWorker(1)] // max 1 activation per silo
public class MyLonelyWorkerGrain : ILonelyWorkerGrain
{
    //...
}
```

--------------------------------

### Set Response Timeout for Orleans Grain Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Applies a 5-second response timeout to the `LeaveGame` method by adding the `ResponseTimeoutAttribute` to the interface definition. This ensures the method completes within the specified duration, otherwise a `TimeoutException` is thrown.

```csharp
public interface IPlayerGrain : IGrainWithGuidKey
{
    Task<IGameGrain> GetCurrentGame(CancellationToken cancellationToken = default);

    Task JoinGame(IGameGrain game, CancellationToken cancellationToken = default);

    [ResponseTimeout("00:00:05")] // 5s timeout
    Task LeaveGame(IGameGrain game, CancellationToken cancellationToken = default);
}
```

--------------------------------

### Access Silo Metadata in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/silo-metadata

Retrieve metadata for a specific silo using the SiloMetadataCache. Metadata keys like 'role' can be used to influence application logic.

```csharp
var siloMetadata = siloMetadataCache.GetSiloMetadata(siloAddress);

if (siloMetadata.Metadata.TryGetValue("role", out var role))
{
    Console.WriteLine($"Silo Role for {siloAddress}: {role}");
    // Execute role-specific logic
}

```

--------------------------------

### Raising an Event in JournaledGrain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/journaledgrain-basics

Call the `RaiseEvent` function to initiate an event write to storage. This method does not wait for the write to complete.

```csharp
RaiseEvent(new PostedEvent()
{
    Guid = guid,
    User = user,
    Text = text,
    Timestamp = DateTime.UtcNow
});
```

--------------------------------

### Incorrect Parameter Renaming in Method Signature

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/backward-compatibility-guidelines

Demonstrates how renaming parameters in a method signature across versions can cause unexpected behavior due to Orleans serializer internals.

```csharp
[Version(1)]
public interface IMyGrain : IGrainWithIntegerKey
{
    // return a - b
    Task<int> Subtract(int a, int b);
}
```

```csharp
[Version(2)]
public interface IMyGrain : IGrainWithIntegerKey
{
    // return b - a
    Task<int> Subtract(int b, int a);
}
```

--------------------------------

### Deploy Orleans Dashboard on a Separate Host

Source: https://learn.microsoft.com/en-us/dotnet/orleans/dashboard

Deploy the Orleans Dashboard on a separate Orleans client host for a dedicated monitoring endpoint. Ensure the dashboard is also added to the silo configuration.

```csharp
using Orleans.Configuration;
using Orleans.Dashboard;
using System.Net;

// Start the silo host
var siloHostBuilder = Host.CreateApplicationBuilder(args);
siloHostBuilder.UseOrleans(builder =>
{
    builder.UseDevelopmentClustering(options => 
        options.PrimarySiloEndpoint = new IPEndPoint(IPAddress.Loopback, 11111));
    builder.UseInMemoryReminderService();
    builder.AddMemoryGrainStorageAsDefault();
    builder.ConfigureEndpoints(IPAddress.Loopback, 11111, 30000);

    // Dashboard must also be added to silos
    builder.AddDashboard();
});
using var siloHost = siloHostBuilder.Build();
await siloHost.StartAsync();

// Create a separate web application for the dashboard
var dashboardBuilder = WebApplication.CreateBuilder(args);

// Configure Orleans client
dashboardBuilder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.UseStaticClustering(options => 
        options.Gateways.Add(new IPEndPoint(IPAddress.Loopback, 30000).ToGatewayUri()));

    // Add dashboard services to the client
    clientBuilder.AddDashboard();
});

var dashboardApp = dashboardBuilder.Build();

// Map dashboard endpoints on the client
dashboardApp.MapOrleansDashboard();

await dashboardApp.RunAsync();

await siloHost.StopAsync();

```

--------------------------------

### Implement Reminder Callback

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

Implement the IRemindable.ReceiveReminder method to handle reminder ticks. This method is called when a reminder fires.

```csharp
Task IRemindable.ReceiveReminder(string reminderName, TickStatus status)
{
    Console.WriteLine("Thanks for reminding me-- I almost forgot!");
    return Task.CompletedTask;
}
```

--------------------------------

### Define Version 1 Grain Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/backward-compatibility-guidelines

Defines the initial version of a grain interface with a specific method.

```csharp
[Version(1)]
public interface IMyGrain : IGrainWithIntegerKey
{
    // First method
    Task MyMethod(int arg);
}
```

--------------------------------

### Serialize C# record type with implicit IDs

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Demonstrates serializing a C# record type where primary constructor parameters automatically receive implicit IDs. Ensure parameter order is not changed after deployment to maintain compatibility.

```csharp
[GenerateSerializer]
public record MyRecord(string A, string B)
{
    // ID 0 won't clash with A in primary constructor as they don't share identities
    [Id(0)]
    public string C { get; init; }
}

```

--------------------------------

### Define IHello Grain Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/tutorial-1

Defines the contract for a grain that can respond to a greeting. This interface inherits from IGrainWithIntegerKey, indicating it will be identified by an integer key.

```csharp
namespace GrainInterfaces;

public interface IHello : IGrainWithIntegerKey
{
    ValueTask<string> SayHello(string greeting);
}

```

--------------------------------

### Configure Azure Queue Streams

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/streams-implementation/azure-queue-streams

Configure Azure Queue Streams with connection details, queue names, message visibility timeout, and cache size. Ensure the MessageVisibilityTimeout is set appropriately based on message processing time and cache capacity.

```csharp
hostBuilder
  .AddAzureQueueStreams("AzureQueueProvider", configurator => {
    configurator.ConfigureAzureQueue(
      ob => ob.Configure(options => {
        options.ConnectionString = "[PLACEHOLDER]";
        options.QueueNames = new List<string> {
          "yourprefix-azurequeueprovider-1",
          [...]
          "yourprefix-azurequeueprovider-10",
        };
        options.MessageVisibilityTimeout = TimeSpan.FromSeconds(72);
      }));
    configurator.ConfigureCacheSize(1200);
  })
```

--------------------------------

### Produce Events to Memory Stream

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

Register a timer to periodically send random integer data to the specified stream using OnNextAsync.

```csharp
RegisterTimer(_ =>
{
    return stream.OnNextAsync(Random.Shared.Next());
},
null,
TimeSpan.FromMilliseconds(1_000),
TimeSpan.FromMilliseconds(1_000));

```

--------------------------------

### Define Surrogate for Value Type Serialization

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Use a surrogate type and a converter to serialize foreign value types. Surrogates should use plain fields for better performance.

```csharp
// This is the foreign type, which you do not have control over.
public struct MyForeignLibraryValueType
{
    public MyForeignLibraryValueType(int num, string str, DateTimeOffset dto)
    {
        Num = num;
        String = str;
        DateTimeOffset = dto;
    }

    public int Num { get; }
    public string String { get; }
    public DateTimeOffset DateTimeOffset { get; }
}

// This is the surrogate which will act as a stand-in for the foreign type.
// Surrogates should use plain fields instead of properties for better performance.
[GenerateSerializer]
public struct MyForeignLibraryValueTypeSurrogate
{
    [Id(0)]
    public int Num;

    [Id(1)]
    public string String;

    [Id(2)]
    public DateTimeOffset DateTimeOffset;
}

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class MyForeignLibraryValueTypeSurrogateConverter :
    IConverter<MyForeignLibraryValueType, MyForeignLibraryValueTypeSurrogate>
{
    public MyForeignLibraryValueType ConvertFromSurrogate(
        in MyForeignLibraryValueTypeSurrogate surrogate) =>
        new(surrogate.Num, surrogate.String, surrogate.DateTimeOffset);

    public MyForeignLibraryValueTypeSurrogate ConvertToSurrogate(
        in MyForeignLibraryValueType value) =>
        new()
        {
            Num = value.Num,
            String = value.String,
            DateTimeOffset = value.DateTimeOffset
        };
}

```

--------------------------------

### Retrieve All Confirmed Events in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/journaledgrain-basics

Retrieve all confirmed events up to the latest version by calling RetrieveConfirmedEvents with version 0 and the current Version. It's advisable to save the 'Version' value in a variable before calling this method due to potential updates.

```csharp
await RetrieveConfirmedEvents(0, Version);
```

--------------------------------

### Rolling Upgrade Configuration in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/deploying-new-versions-of-grains

Configures Orleans for a rolling upgrade by setting the default compatibility strategy to BackwardCompatible and the default version selector strategy to AllCompatibleVersions. This allows older clients to communicate with both old and new silo versions.

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.Configure<GrainVersioningOptions>(options =>
    {
        options.DefaultCompatibilityStrategy = nameof(BackwardCompatible);
        options.DefaultVersionSelectorStrategy = nameof(AllCompatibleVersions);
    });
});

using var host = builder.Build();
await host.RunAsync();

```

--------------------------------

### Implement a Serializer Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-customization

Use the SerializerMethodAttribute to flag a static method that serializes an object. This method writes the object's data to the provided ISerializationContext.

```csharp
[SerializerMethod]
static private void Serialize(
    object input,
    ISerializationContext context,
    Type expected)
{
    // ...
}
```

--------------------------------

### Refresh Grain State in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/replicated-instances

Call RefreshNow to confirm all unconfirmed events and load the latest version of the grain state from storage. This ensures the grain is fully caught up with the latest version.

```csharp
await RefreshNow();

```

--------------------------------

### Container App Environment Bicep Template

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Defines the Azure Log Analytics and Application Insights resources. It configures the application insights to link to the Log Analytics workspace and outputs necessary keys and connection strings.

```bicep
param operationalInsightsName string
param appInsightsName string
param location string

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logs.id
  }
}

resource logs 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: operationalInsightsName
  location: location
  properties: {
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource env 'Microsoft.App/managedEnvironments@2022-03-01' = {
  name: '${resourceGroup().name}env'
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logs.properties.customerId
        sharedKey: logs.listKeys().primarySharedKey
      }
    }
  }
}

output id string = env.id
output appInsightsInstrumentationKey string = appInsights.properties.InstrumentationKey
output appInsightsConnectionString string = appInsights.properties.ConnectionString

```

--------------------------------

### Bicep: App Insights Connection String Output

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

This Bicep output retrieves the connection string for Application Insights. It is used later by the App Service module.

```bicep
output appInsightsConnectionString string = appInsights.properties.ConnectionString
```

--------------------------------

### Initiate Cancellation with GrainCancellationTokenSource

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Call the `GrainCancellationTokenSource.Cancel()` method to initiate cancellation. This signals all associated GrainCancellationToken instances.

```csharp
// Request cancellation
await tcs.Cancel();
```

--------------------------------

### Retrieve String Primary Key in Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-identity

Use this within grain activation logic to retrieve its string primary key.

```csharp
public override Task OnActivateAsync()
{
    string primaryKey = this.GetPrimaryKeyString();
    return base.OnActivateAsync();
}
```

--------------------------------

### Configure memory-based activation shedding

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

Configures memory-based activation shedding for grains. This is useful for preventing out-of-memory conditions by automatically deactivating grains when the silo is under memory pressure.

```csharp
builder.Configure<GrainCollectionOptions>(options =>
{
    // Enable memory-based activation shedding
    options.EnableActivationSheddingOnMemoryPressure = true;

    // Memory usage percentage (0-100) at which grain collection triggers
    options.MemoryUsageLimitPercentage = 80; // default: 80

    // Target memory usage percentage to reach after collection
    options.MemoryUsageTargetPercentage = 75; // default: 75

    // How often to poll memory usage
    options.MemoryUsagePollingPeriod = TimeSpan.FromSeconds(5); // default: 5s
});

```

--------------------------------

### Configure Azure Diagnostics for Logging

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/troubleshooting-deployments

Set the `scheduledTransferLogLevelFilter` attribute to 'Information' in the `Logs` element of the `diagnostics.wadcfgx` file. This filters traces sent to Azure Storage.

```xml
<diagnosticsConfiguration xmlns="http://schemas.microsoft.com/ServiceHosting/2011/09/diagnostics">
  <PublicConfig>
    <WADConfigInfo xmlns="http://schemas.microsoft.com/ServiceHosting/2011/09/diagnostics">
      <Logs scheduledTransferLogLevelFilter="Information" />
    </WADConfigInfo>
  </PublicConfig>
</diagnosticsConfiguration>
```

--------------------------------

### Consume Async Stream with Direct Cancellation Token Passing

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Consumes an `IAsyncEnumerable<T>` stream by passing the `CancellationToken` directly to the grain method. This approach is suitable when the grain method is designed with the `[EnumeratorCancellation]` attribute.

```csharp
var grain = grainFactory.GetGrain<IDataStreamGrain>(Guid.NewGuid());

using var cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(10)); // Auto-cancel after 10 seconds

try
{
    // The token is passed directly to the method and will be combined
    // with any token passed to GetAsyncEnumerator() internally
    await foreach (var dataPoint in grain.StreamDataAsync(1000, cts.Token))
    {
        Console.WriteLine($"Received: {dataPoint}");

        // Process the data point
        // Cancellation will stop the enumeration automatically
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Streaming was canceled");
}
```

--------------------------------

### Silo Configuration with Azure Table Clustering

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configure an Orleans silo to use Azure Table storage for cluster membership. Ensure the connection string is correctly set.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";
var silo = new SiloHostBuilder()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAzureStorageClustering(
        options => options.ConnectionString = connectionString)
    .ConfigureEndpoints(siloPort: 11_111, gatewayPort: 30_000)
    .ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Information).AddConsole())
    .Build();

```

--------------------------------

### Define an incoming grain call filter interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Implement this interface to create a filter that intercepts incoming calls to a grain. The Invoke method is executed for each incoming call.

```csharp
public interface IIncomingGrainCallFilter
{
    Task Invoke(IIncomingGrainCallContext context);
}
```

--------------------------------

### Enabling Exception Conversion on Client

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Add an outgoing grain call filter to the client builder to signal the server that exception conversion should be enabled for calls originating from this client. This is done by setting a RequestContext value.

```csharp
clientBuilder.AddOutgoingGrainCallFilter(context =>
{
    RequestContext.Set("IsExceptionConversionEnabled", true);
    return context.Invoke();
});
```

--------------------------------

### Asynchronous Grain Method Invocation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Invoke a grain method asynchronously using the grain reference. The `await` keyword ensures that the current method execution pauses until the `Task` returned by the grain method completes, without blocking the thread.

```csharp
// Invoking a grain method asynchronously
Task joinGameTask = player.JoinGame(this, GrainCancellationToken);

// The await keyword effectively makes the remainder of the
// method execute asynchronously at a later point
// (upon completion of the Task being awaited) without blocking the thread.
await joinGameTask;

// The next line will execute later, after joinGameTask has completed.
players.Add(playerId);
```

--------------------------------

### Custom Serializer for User Type in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-customization

Implement custom serialization logic for the `User` type by defining `DeepCopier`, `Serializer`, and `Deserializer` methods within a class marked with `[Orleans.CodeGeneration.SerializerAttribute(typeof(User))]`. This allows for fine-grained control over how `User` objects are copied, serialized, and deserialized, including handling of cyclic references.

```csharp
public class User
{
    public User BestFriend { get; set; }
    public string NickName { get; set; }
    public int FavoriteNumber { get; set; }
    public DateTimeOffset BirthDate { get; set; }
}

[Orleans.CodeGeneration.SerializerAttribute(typeof(User))]
internal class UserSerializer
{
    [CopierMethod]
    public static object DeepCopier(
        object original, ICopyContext context)
    {
        var input = (User)original;
        var result = new User();

        // Record 'result' as a copy of 'input'. Doing this
        // immediately after construction allows for data
        // structures that have cyclic references or duplicate
        // references. For example, imagine that 'input.BestFriend'
        // is set to 'input'. In that case, failing to record
        // the copy before trying to copy the 'BestFriend' field
        // would result in infinite recursion.
        context.RecordCopy(original, result);

        // Deep-copy each of the fields.
        result.BestFriend =
            (User)context.SerializationManager.DeepCopy(input.BestFriend);

        // strings in .NET are immutable, so they can be shallow-copied.
        result.NickName = input.NickName;
        // ints are primitive value types, so they can be shallow-copied.
        result.FavoriteNumber = input.FavoriteNumber;
        result.BirthDate =
            (DateTimeOffset)context.SerializationManager.DeepCopy(input.BirthDate);

        return result;
    }

    [SerializerMethod]
    public static void Serializer(
        object untypedInput, ISerializationContext context, Type expected)
    {
        var input = (User) untypedInput;

        // Serialize each field.
        SerializationManager.SerializeInner(input.BestFriend, context);
        SerializationManager.SerializeInner(input.NickName, context);
        SerializationManager.SerializeInner(input.FavoriteNumber, context);
        SerializationManager.SerializeInner(input.BirthDate, context);
    }

    [DeserializerMethod]
    public static object Deserializer(
        Type expected, IDeserializationContext context)
    {
        var result = new User();

        // Record 'result' immediately after constructing it.
        // As with the deep copier, this
        // allows for cyclic references and de-duplication.
        context.RecordObject(result);

        // Deserialize each field in the order that they were serialized.
        result.BestFriend =
            SerializationManager.DeserializeInner<User>(context);
        result.NickName =
            SerializationManager.DeserializeInner<string>(context);
        result.FavoriteNumber =
            SerializationManager.DeserializeInner<int>(context);
        result.BirthDate =
            SerializationManager.DeserializeInner<DateTimeOffset>(context);

        return result;
    }
}
```

--------------------------------

### Serializable Types with Inheritance in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Orleans supports inheritance by serializing each layer separately. Ensure distinct member IDs for members at different inheritance levels.

```csharp
[GenerateSerializer]
public class Publication
{
    [Id(0)]
    public string Title { get; set; }
}

[GenerateSerializer]
public class Book : Publication
{
    [Id(0)]
    public string ISBN { get; set; }
}
```

--------------------------------

### Run Background Work on Thread Pool (No Grain Code)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

Use Task.Run to execute background work on .NET thread pool threads when no grain code or grain calls are involved.

```csharp
Task.Run
```

--------------------------------

### ClearStateAsync Method for File Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Implements the ClearStateAsync method for a file-based grain storage provider. It handles deleting the grain state file and includes version conflict checking before deletion.

```C#
public Task ClearStateAsync(
    string grainType,
    GrainReference grainReference,
    IGrainState grainState)
{
    var fName = GetKeyString(grainType, grainReference);
    var path = Path.Combine(_options.RootDirectory, fName);

    var fileInfo = new FileInfo(path);
    if (fileInfo.Exists)
    {
        if (fileInfo.LastWriteTimeUtc.ToString() != grainState.ETag)
        {
            throw new InconsistentStateException(
                $"Version conflict (ClearState): ServiceId={_clusterOptions.ServiceId} " +
                $
```

--------------------------------

### Define URL Shortener Grain Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Define a custom grain interface for the URL shortener, inheriting from IGrainWithStringKey. This interface specifies methods for persisting and retrieving URL mappings.

```csharp
public interface IUrlShortenerGrain : IGrainWithStringKey
{
    Task SetUrl(string fullUrl);

    Task<string> GetUrl();
}

```

--------------------------------

### Bicep Logs and Insights Module

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

This Bicep file defines Azure Log Analytics and Application Insights resources. It outputs the instrumentation key for Application Insights.

```bicep
param operationalInsightsName string
param appInsightsName string
param location string

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logs.id
  }
}

resource logs 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: operationalInsightsName
  location: location
  properties: {
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  }
}

output appInsightsInstrumentationKey string = appInsights.properties.InstrumentationKey

```

--------------------------------

### ClearStateAsync Implementation with Version Conflict Check

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Clears the state of a grain by deleting its corresponding file. It includes a version check using ETag to prevent accidental deletion of outdated state.

```csharp
if (fileInfo.LastWriteTimeUtc.ToString() != grainState.ETag)
{
    throw new InconsistentStateException($"""
        Version conflict (ClearState): ServiceId={_clusterOptions.ServiceId}
        ProviderName={_storageName} GrainType={typeof(T)}
        GrainReference={grainId}.
        """);
}

grainState.ETag = null;
grainState.State = (T)Activator.CreateInstance(typeof(T))!;

fileInfo.Delete();

return Task.CompletedTask;
}
```

--------------------------------

### Return Task for Void Grain Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

For non-async grain methods that don't return a value, return Task.CompletedTask to indicate completion.

```csharp
public Task GrainMethod2()
{
    return Task.CompletedTask;
}
```

--------------------------------

### Serializable Balance State Object

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/transactions

Define a serializable state object for transactional state. Ensure it's decorated with GenerateSerializerAttribute and its members with IdAttribute.

```csharp
namespace TransactionalExample.Abstractions;

[GenerateSerializer]
public record class Balance
{
    [Id(0)]
    public decimal Value { get; set; } = 1_000;
}
```

--------------------------------

### Creating Grain References with Unique Interfaces

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

After defining unique interfaces, you can obtain specific grain references by casting to the appropriate interface type. This ensures you interact with the intended grain implementation.

```csharp
// Get a reference to an UpCounterGrain.
ICounterGrain myUpCounter = grainFactory.GetGrain<IUpCounterGrain>("my-counter");

// Get a reference to a DownCounterGrain.
ICounterGrain myDownCounter = grainFactory.GetGrain<IDownCounterGrain>("my-counter");
```

--------------------------------

### Configure Private Ports using Azure CLI

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-app-service

Use this Azure CLI command to set the number of private ports allocated to app instances in Azure App Service. This is crucial for enabling communication between Orleans silos.

```bash
az webapp config set -g '<resource-group-name>' --subscription '<subscription-id>' -n '<app-service-app-name>' --generic-configurations '{"vnetPrivatePortsCount": "2"}'
```

--------------------------------

### Define grain interface with CancellationToken

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Add a CancellationToken parameter as the last parameter in your grain interface method. Make it optional with a default value for better usability.

```csharp
public interface IProcessingGrain : IGrainWithGuidKey
{
    Task<string> ProcessDataAsync(string data, int chunks, CancellationToken cancellationToken = default);
}
```

--------------------------------

### Add Activity Propagation to Orleans Host

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Enable activity propagation within the Orleans host to ensure distributed tracing context is carried across Orleans calls. This is essential for tracing requests that span multiple Orleans services.

```csharp
builder.Host.UseOrleans((_, clientBuilder) =>
{
    clientBuilder.AddActivityPropagation();
});
```

--------------------------------

### Write Grain State to File with ETag Check

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Serializes and writes the grain state to a file. It checks the ETag against the file's last modified time to prevent concurrent modification conflicts.

```csharp
public async Task WriteStateAsync(
    string grainType,
    GrainReference grainReference,
    IGrainState grainState)
{
    var storedData = JsonConvert.SerializeObject(grainState.State, _jsonSettings);

    var fName = GetKeyString(grainType, grainReference);
    var path = Path.Combine(_options.RootDirectory, fName);

    var fileInfo = new FileInfo(path);

    if (fileInfo.Exists && fileInfo.LastWriteTimeUtc.ToString() != grainState.ETag)
    {
        throw new InconsistentStateException(
            $"Version conflict (WriteState): ServiceId={_clusterOptions.ServiceId} " +
            $
```

--------------------------------

### Customize Endpoint Options - Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Customize Silo and Gateway ports, advertised IP address, and listening endpoints using EndpointOptions. This is useful for advanced network configurations.

```csharp
siloBuilder.Configure<EndpointOptions>(options =>
{
    // Port to use for silo-to-silo
    options.SiloPort = 11_111;
    // Port to use for the gateway
    options.GatewayPort = 30_000;
    // IP Address to advertise in the cluster
    options.AdvertisedIPAddress = IPAddress.Parse("172.16.0.42");
    // The socket used for client-to-silo will bind to this endpoint
    options.GatewayListeningEndpoint = new IPEndPoint(IPAddress.Any, 40_000);
    // The socket used by the gateway will bind to this endpoint
    options.SiloListeningEndpoint = new IPEndPoint(IPAddress.Any, 50_000);
})
```

--------------------------------

### Await a Task and continue execution

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/best-practices

Use await to pause execution until an asynchronous operation completes, then continue with the result. This is the standard way to compose async operations.

```csharp
var x = await bar.Foo();

var y = DoSomething(x);

return y;

```

--------------------------------

### Rename LoadSheddingLimit to CpuThreshold in Orleans 10.0

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

The `LoadSheddingLimit` property in `LoadSheddingOptions` is renamed to `CpuThreshold` in Orleans 10.0 to better reflect its function.

```csharp
// Orleans 7.x
siloBuilder.Configure<LoadSheddingOptions>(options =>
{
    options.LoadSheddingEnabled = true;
    options.LoadSheddingLimit = 95; // No longer works
});

// Orleans 10.0
siloBuilder.Configure<LoadSheddingOptions>(options =>
{
    options.LoadSheddingEnabled = true;
    options.CpuThreshold = 95; // Use this instead
});

```

--------------------------------

### Verify Consul services endpoint

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/consul-deployment

Check the Consul services endpoint to confirm the agent is running and ready to accept membership requests. A successful response will return an empty JSON object indicating no services are registered yet.

```json
{
    "consul": []
}
```

--------------------------------

### Troubleshooting KubeConfigException in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/kubernetes

This exception indicates that the Kubernetes service environment variables are not set within the pod. Ensure KUBERNETES_SERVICE_HOST and KUBERNETES_SERVICE_PORT are defined, and that automountServiceAccountToken is true in your deployment.

```csharp
Unhandled exception. k8s.Exceptions.KubeConfigException: unable to load in-cluster configuration, KUBERNETES_SERVICE_HOST and KUBERNETES_SERVICE_PORT must be defined
at k8s.KubernetesClientConfiguration.InClusterConfig()
```

--------------------------------

### Register Outgoing Call Filter Class with Extension Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Register a custom filter class using the AddOutgoingGrainCallFilter extension method. This is a convenient way to register filters.

```csharp
builder.AddOutgoingGrainCallFilter<OutgoingLoggingCallFilter>();
```

--------------------------------

### Configure Grain Storage Provider

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

This attribute specifies the named storage provider to be used for reading and writing grain state data. It's applied to grains inheriting from Grain<TGrainState>.

```csharp
[StorageProvider(ProviderName = "store1")]
public class MyGrain : Grain<MyGrainState>, IMyGrain
{
    public Task DoSomethingAsync() => Task.CompletedTask;
}
```

--------------------------------

### Disambiguating Grains with Unique Marker Interfaces

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-references

Define unique interfaces for each grain implementation to resolve ambiguity. This allows Orleans to correctly identify the intended grain when creating a reference.

```csharp
public interface ICounterGrain : IGrainWithStringKey
{
    ValueTask<int> UpdateCount();
}

// Define unique interfaces for our implementations
public interface IUpCounterGrain : ICounterGrain, IGrainWithStringKey {}
public interface IDownCounterGrain : ICounterGrain, IGrainWithStringKey {}

public class UpCounterGrain : IUpCounterGrain
{
    private int _count;

    public ValueTask<string> UpdateCount() => new(++_count); // Increment count
}

public class DownCounterGrain : IDownCounterGrain
{
    private int _count;

    public ValueTask<string> UpdateCount() => new(--_count); // Decrement count
}
```

--------------------------------

### Generate Serializer for Base and Sub Classes

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Use GenerateSerializerAttribute and IdAttribute to define serializable types. Member IDs can overlap between base and subclass types.

```csharp
[GenerateSerializer]
public class MyBaseClass
{
    [Id(0)]
    public int MyBaseInt { get; set; }
}

[GenerateSerializer]
public sealed class MySubClass : MyBaseClass
{
    [Id(0)]
    public int MySubInt { get; set; }
}

```

--------------------------------

### Configure Orleans Code Generation Log Level

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/code-generation

Emit additional diagnostics during the build process by setting the OrleansCodeGenLogLevel property in the project's .csproj file. Use 'Trace' for detailed logging.

```xml
<PropertyGroup>
  <OrleansCodeGenLogLevel>Trace</OrleansCodeGenLogLevel>
</PropertyGroup>
```

--------------------------------

### RequestContext.Get

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-context

Use this method to retrieve a value from the current request context.

```APIDOC
## RequestContext.Get

### Description
Retrieves the value associated with the specified key from the current request context.

### Method
```csharp
object Get(string key)
```

### Parameters
#### Path Parameters
- **key** (string) - Required - The key of the metadata to retrieve.

### Response
#### Success Response (object)
- **value** (object) - The retrieved value, or null if the key is not found.

### Response Example
```csharp
var traceId = RequestContext.Get("TraceId");
```
```

--------------------------------

### Register Custom Orleans Serializer

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-customization

This C# code demonstrates how to register a custom serializer with the Orleans runtime by extending ISerializerBuilder. It adds the custom serializer as a singleton service for the relevant interfaces.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Orleans.Serialization;
using Orleans.Serialization.Serializers;
using Orleans.Serialization.Cloning;

public static class SerializationHostingExtensions
{
    public static ISerializerBuilder AddCustomSerializer(
        this ISerializerBuilder builder)
    {
        var services = builder.Services;

        services.AddSingleton<CustomOrleansSerializer>();
        services.AddSingleton<IGeneralizedCodec, CustomOrleansSerializer>();
        services.AddSingleton<IGeneralizedCopier, CustomOrleansSerializer>();
        services.AddSingleton<ITypeFilter, CustomOrleansSerializer>();

        return builder;
    }
}
```

--------------------------------

### Inject Logging in Orleans Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/resources/best-practices

Injecting ILogger into a grain using dependency injection is a common pattern for logging within Orleans applications. Ensure Microsoft.Extensions.Logging is configured.

```csharp
public HelloGrain(ILogger<HelloGrain> logger)
{
    _logger = logger;
}
```

--------------------------------

### Dynamically Manage Silos in Tests

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

The `InProcessTestCluster` allows adding and removing silos during test execution. This is useful for testing cluster behavior under dynamic conditions.

```C#
// Start with 2 silos
var builder = new InProcessTestClusterBuilder(initialSilosCount: 2);
var cluster = builder.Build();
await cluster.DeployAsync();

// Add a third silo
var newSilo = await cluster.StartSiloAsync();

// Stop a silo
await cluster.StopSiloAsync(newSilo);

// Restart all silos
await cluster.RestartAsync();

```

--------------------------------

### Create Azure Service Principal

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Create an Azure service principal with Contributor role using the Azure CLI. This account is used to manage Azure resources on your behalf for automated deployments. The output contains sensitive credentials that should be stored securely.

```azurecli
az ad sp create-for-rbac --sdk-auth --role Contributor \
  --name "<display-name>"  --scopes /subscriptions/<your-subscription-id>

```

--------------------------------

### Define Observer Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/observers

Define the interface that clients must implement to receive messages from an observer grain. The interface must inherit from IGrainObserver.

```csharp
public interface IChat : IGrainObserver
{
    Task ReceiveMessage(string message);
}
```

--------------------------------

### Configure Memory-Based Activation Shedding

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/activation-collection

Enable and configure memory-based activation shedding for grains. This feature helps manage memory pressure by deactivating idle grain activations when memory usage exceeds a specified limit.

```csharp
siloBuilder.Configure<GrainCollectionOptions>(options =>
{
    // Enable memory-based activation shedding
    options.EnableActivationSheddingOnMemoryPressure = true;

    // Trigger collection when memory usage exceeds 80% (default)
    options.MemoryUsageLimitPercentage = 80;

    // Stop collection when memory usage drops below 75% (default)
    options.MemoryUsageTargetPercentage = 75;

    // Poll memory usage every 5 seconds (default)
    options.MemoryUsagePollingPeriod = TimeSpan.FromSeconds(5);
});

```

--------------------------------

### Lifecycle Participant Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/orleans-lifecycle

The ILifecycleParticipant interface is a marker interface used to identify components interested in participating in a lifecycle. It requires the component to implement a Participate method that accepts the observable lifecycle.

```csharp
public interface ILifecycleParticipant<TLifecycleObservable>
    where TLifecycleObservable : ILifecycleObservable
{
    void Participate(TLifecycleObservable lifecycle);
}

```

--------------------------------

### Raise Multiple Events Atomically in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/journaledgrain-basics

Use RaiseEvents to atomically write a sequence of events to storage. This is generally preferred over multiple individual RaiseEvent calls to avoid partial writes and ensure atomicity. Note that this increments the version number by more than one.

```csharp
RaiseEvent(e1);
RaiseEvent(e2);
await ConfirmEvents();
```

```csharp
RaiseEvents(IEnumerable<EventType> events)
```

--------------------------------

### Client Configuration with Azure Table Clustering and Entra ID

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configures an Orleans client to use Azure Table for cluster membership with Microsoft Entra ID authentication. This is recommended for production environments.

```csharp
using Azure.Identity;

var builder = Host.CreateApplicationBuilder(args);
clientBuilder.UseOrleansClient(clientBuilder =>
{
    clientBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAzureStorageClustering(options =>
    {
        options.ConfigureTableServiceClient(
            new Uri("https://<your-storage-account>.table.core.windows.net"),
            new DefaultAzureCredential());
    });
});

using var host = builder.Build();


```

--------------------------------

### Subscribe to a Stream

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Call SubscribeAsync on an IAsyncStream to register an observer and receive events. This returns a handle for managing the subscription.

```csharp
StreamSubscriptionHandle<T> subscriptionHandle = await stream.SubscribeAsync(IAsyncObserver)
```

--------------------------------

### Publish to BroadcastChannel

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Publish messages to a BroadcastChannel. Ensure the ChannelId is created correctly using a namespace and a key.

```csharp
var grainKey = Guid.NewGuid().ToString("N");
var channelId = ChannelId.Create("some-namespace", grainKey);
var stream = provider.GetChannelWriter<int>(channelId);

await stream.Publish(1);
await stream.Publish(2);
await stream.Publish(3);

```

--------------------------------

### Incoming grain call context interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

This interface provides access to details of the incoming grain call, including the grain, methods, arguments, and allows invoking the next filter or grain method. The Result property can be modified after the call.

```csharp
public interface IIncomingGrainCallContext
{
    /// <summary>
    /// Gets the grain being invoked.
    /// </summary>
    IAddressable Grain { get; }

    /// <summary>
    /// Gets the <see cref="MethodInfo"/> for the interface method being invoked.
    /// </summary>
    MethodInfo InterfaceMethod { get; }

    /// <summary>
    /// Gets the <see cref="MethodInfo"/> for the implementation method being invoked.
    /// </summary>
    MethodInfo ImplementationMethod { get; }

    /// <summary>
    /// Gets the arguments for this method invocation.
    /// </summary>
    object[] Arguments { get; }

    /// <summary>
    /// Invokes the request.
    /// </summary>
    Task Invoke();

    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    object Result { get; set; }
}
```

--------------------------------

### Configure Azure Storage Clustering with DefaultAzureCredential

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/client-configuration

Use Azure Table storage for clustering, configuring the TableServiceClient with a storage account URI and DefaultAzureCredential. This credential seamlessly handles authentication across development and production environments.

```csharp
clientBuilder.UseAzureStorageClustering(options =>
{
    options.ConfigureTableServiceClient(
        new Uri("https://<your-storage-account>.table.core.windows.net"),
        new DefaultAzureCredential());
});
```

--------------------------------

### Access Request Context Data in Placement Filter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-context

Access RequestContextData from the PlacementTarget within an IPlacementFilterDirector to make placement decisions based on request context. The static RequestContext is not available at this stage.

```C#
internal sealed class ExamplePlacementFilterDirector(ILogger<ExamplePlacementFilterDirector> logger)
    : IPlacementFilterDirector
{
    public IEnumerable<SiloAddress> Filter(
        PlacementFilterStrategy filterStrategy,
        PlacementTarget target,
        IEnumerable<SiloAddress> silos)
    {
        if (target.RequestContextData.TryGetValue("somekey", out var somevalue) 
            && somevalue is string somestring)
        {
            logger.LogInformation("Read {Value} for {Key} from the RequestContext", somestring, "somekey");
            // somestring is available for the filtering logic
        }
        return silos;
    }
}

```

--------------------------------

### Escaping Orleans Grain Context with Task.Run

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

Demonstrates using Task.Run to execute code on the thread pool scheduler, escaping the Orleans grain context. Ensures correct return to the Orleans scheduler after the awaited task.

```csharp
public async Task MyGrainMethod()
{
    // Grab the grain's task scheduler
    var orleansTS = TaskScheduler.Current;
    await Task.Delay(10_000);

    // Current task scheduler did not change, the code after await is still running
    // in the same task scheduler.
    Assert.AreEqual(orleansTS, TaskScheduler.Current);

    Task t1 = Task.Run(() =>
    {
        // This code runs on the thread pool scheduler, not on Orleans task scheduler
        Assert.AreNotEqual(orleansTS, TaskScheduler.Current);
        Assert.AreEqual(TaskScheduler.Default, TaskScheduler.Current);
    });

    await t1;

    // We are back to the Orleans task scheduler.
    // Since await was executed in Orleans task scheduler context, we are now back
    // to that context.
    Assert.AreEqual(orleansTS, TaskScheduler.Current);

    // Example of using Task.Factory.StartNew with a custom scheduler to escape from
    // the Orleans scheduler
    Task t2 = Task.Factory.StartNew(() =>
    {
        // This code runs on the MyCustomSchedulerThatIWroteMyself scheduler, not on
        // the Orleans task scheduler
        Assert.AreNotEqual(orleansTS, TaskScheduler.Current);
        Assert.AreEqual(MyCustomSchedulerThatIWroteMyself, TaskScheduler.Current);
    },
    CancellationToken.None,
    TaskCreationOptions.None,
    scheduler: MyCustomSchedulerThatIWroteMyself);

    await t2;

    // We are back to Orleans task scheduler.
    Assert.AreEqual(orleansTS, TaskScheduler.Current);
}

```

--------------------------------

### Implement a Deserializer Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization-customization

Use the DeserializerMethodAttribute to flag a static method that deserializes an object. This method reads data from the IDeserializationContext and returns a new instance of the object.

```csharp
[DeserializerMethod]
static private object Deserialize(
    Type expected,
    IDeserializationContext context)
{
    //...
}
```

--------------------------------

### Write Grain State to File

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Writes grain state to a file, performing an ETag check to prevent concurrent modification conflicts. Updates the ETag after writing.

```csharp
public async Task WriteStateAsync<T>(
    string stateName,
    GrainId grainId,
    IGrainState<T> grainState)
{
    var storedData = _options.GrainStorageSerializer.Serialize(grainState.State);
    var fName = GetKeyString(stateName, grainId);
    var path = Path.Combine(_options.RootDirectory, fName!);
    var fileInfo = new FileInfo(path);
    if (fileInfo.Exists && fileInfo.LastWriteTimeUtc.ToString() != grainState.ETag)
    {
        throw new InconsistentStateException($"""Version conflict (WriteState): ServiceId={_clusterOptions.ServiceId}
            ProviderName={_storageName} GrainType={typeof(T)}
            GrainReference={grainId}.
            """);
    }

    await File.WriteAllBytesAsync(path, storedData.ToArray());

    fileInfo.Refresh();
    grainState.ETag = fileInfo.LastWriteTimeUtc.ToString();
}

```

--------------------------------

### Generate Grain State Filename

Source: https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage

Constructs a unique filename for a grain's state based on service ID, grain key, and grain type.

```csharp
private string GetKeyString(string grainType, GrainId grainId) =>
    $"{_clusterOptions.ServiceId}.{grainId.Key}.{grainType}";

```

--------------------------------

### Silo Configuration with Azure Table Clustering and Connection String

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/typical-configurations

Configures an Orleans silo to use Azure Table for cluster membership with a connection string. This method is not recommended for production due to security concerns.

```csharp
const string connectionString = "YOUR_CONNECTION_STRING_HERE";

var builder = Host.CreateApplicationBuilder(args);
siloBuilder.UseOrleans(siloBuilder =>
{
    siloBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster42";
        options.ServiceId = "MyAwesomeService";
    })
    .UseAzureStorageClustering(
        options => options.ConfigureTableServiceClient(connectionString))
    .ConfigureEndpoints(siloPort: 11_111, gatewayPort: 30_000);
});

builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();

using var host = builder.Build();


```

--------------------------------

### Register Silo-Wide Grain Call Filter with Delegate

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Register a delegate as a silo-wide grain call filter using Dependency Injection. This filter can modify request context, intercept results, and modify results.

```csharp
siloHostBuilder.AddIncomingGrainCallFilter(async context =>
{
    // If the method being called is 'MyInterceptedMethod', then set a value
    // on the RequestContext which can then be read by other filters or the grain.
    if (string.Equals(
        context.InterfaceMethod.Name,
        nameof(IMyGrain.MyInterceptedMethod)))
    {
        RequestContext.Set(
            "intercepted value", "this value was added by the filter");
    }

    await context.Invoke();

    // If the grain method returned an int, set the result to double that value.
    if (context.Result is int resultValue)
    {
        context.Result = resultValue * 2;
    }
});

```

--------------------------------

### Configure Messaging Options in Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide

Explicitly set `CancelRequestOnTimeout` to `true` if your application depends on the previous default behavior.

```csharp
siloBuilder.Configure<SiloMessagingOptions>(options =>
{
    options.CancelRequestOnTimeout = true;
});

// For clients
clientBuilder.Configure<ClientMessagingOptions>(options =>
{
    options.CancelRequestOnTimeout = true;
});

```

--------------------------------

### Configure Cluster and Service IDs

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Set the ClusterId for Orleans cluster identification and ServiceId for application-specific providers. The ClusterId allows silos and clients to communicate, while the ServiceId should remain stable across deployments for persistence providers.

```csharp
public static void ConfigureClusterOptions(ISiloHostBuilder siloBuilder)
{
    siloBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "my-first-cluster";
        options.ServiceId = "AspNetSampleApp";
    });
}
```

--------------------------------

### Obsolete RegisterTimer API Signature

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/timers-and-reminders

This is the signature for the obsolete `RegisterTimer` API. Migrate to `RegisterGrainTimer` for Orleans 8.0 and later.

```csharp
protected IDisposable RegisterTimer(
    Func<object, Task> asyncCallback, // function invoked when the timer ticks
    object state,                     // object to pass to asyncCallback
    TimeSpan dueTime,                 // time to wait before the first timer tick
    TimeSpan period)                  // the period of the timer
```

--------------------------------

### Define a surrogate for a foreign value type

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Define a surrogate type and a converter to handle serialization of foreign value types. Surrogates should use plain fields for better performance.

```csharp
// This is the foreign type, which you do not have control over.
public struct MyForeignLibraryValueType
{
    public MyForeignLibraryValueType(int num, string str, DateTimeOffset dto)
    {
        Num = num;
        String = str;
        DateTimeOffset = dto;
    }

    public int Num { get; }
    public string String { get; }
    public DateTimeOffset DateTimeOffset { get; }
}

// This is the surrogate which will act as a stand-in for the foreign type.
// Surrogates should use plain fields instead of properties for better performance.
[GenerateSerializer]
public struct MyForeignLibraryValueTypeSurrogate
{
    [Id(0)]
    public int Num;

    [Id(1)]
    public string String;

    [Id(2)]
    public DateTimeOffset DateTimeOffset;
}

// This is a converter that converts between the surrogate and the foreign type.
[RegisterConverter]
public sealed class MyForeignLibraryValueTypeSurrogateConverter :
    IConverter<MyForeignLibraryValueType, MyForeignLibraryValueTypeSurrogate>
{
    public MyForeignLibraryValueType ConvertFromSurrogate(
        in MyForeignLibraryValueTypeSurrogate surrogate) =>
        new(surrogate.Num, surrogate.String, surrogate.DateTimeOffset);

    public MyForeignLibraryValueTypeSurrogate ConvertToSurrogate(
        in MyForeignLibraryValueType value) =>
        new()
        {
            Num = value.Num,
            String = value.String,
            DateTimeOffset = value.DateTimeOffset
        };
}
```

--------------------------------

### Configure Silo Messaging Options for Cancellation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Configure silo messaging options to control cancellation behavior, such as sending cancellation signals on request timeouts and waiting for acknowledgements.

```csharp
siloBuilder.Configure<SiloMessagingOptions>(options =>
{
    // Send cancellation signal when requests timeout (default: true)
    options.CancelRequestOnTimeout = true;

    // Wait for callee to acknowledge cancellation (default: false)
    // Setting this to true provides stronger cancellation guarantees but may impact performance
    options.WaitForCancellationAcknowledgement = false;
});
```

--------------------------------

### Resume Implicit Subscription in OnSubscribed

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-programming-apis

Called when a grain is subscribed to a stream. Use this to create a stream handle and resume the subscription, attaching the processing logic.

```csharp
public async Task OnSubscribed(IStreamSubscriptionHandleFactory handleFactory)
{
    var handle = handleFactory.Create<string>();
    await handle.ResumeAsync(this);
}
```

--------------------------------

### Configure Cosmos DB Grain Storage

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Configure Azure Cosmos DB grain storage using the AddCosmosGrainStorage extension method. This involves specifying the Cosmos DB endpoint, database name, container name, and resource creation settings.

```csharp
var builder = Host.CreateApplicationBuilder();
builder.UseOrleans(siloBuilder =>
{
    siloBuilder.AddCosmosGrainStorage(
        name: "cosmos",
        configureOptions: options =>
        {
            options.ConfigureCosmosClient(
                "https://myaccount.documents.azure.com:443/",
                new DefaultAzureCredential());
            options.DatabaseName = "Orleans";
            options.ContainerName = "OrleansStorage";
            options.IsResourceCreationEnabled = true;
        });
});

```

--------------------------------

### Handle Connection Issues in JournaledGrain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/event-sourcing/journaledgrain-diagnostics

Override these methods in your JournaledGrain subclass to receive notifications for connection errors and their resolutions. The `ConnectionIssue` object provides details about the error.

```csharp
protected override void OnConnectionIssue(
    ConnectionIssue issue)
{
    /// handle the observed error described by issue
}
```

```csharp
protected override void OnConnectionIssueResolved(
    ConnectionIssue issue)
{
    /// handle the resolution of a previously reported issue
}
```

--------------------------------

### Configure Azure Storage Clustering with DefaultAzureCredential

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration

Use Azure Table Storage for clustering with Microsoft Entra ID authentication. DefaultAzureCredential works across local development and production environments.

```csharp
using Azure.Identity;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder.UseAzureStorageClustering(options =>
        {
            options.ConfigureTableServiceClient(
                new Uri("https://<your-storage-account>.table.core.windows.net"),
                new DefaultAzureCredential());
        });
    })
    .UseConsoleLifetime()
    .Build();

```

--------------------------------

### Run Async Worker Task from Grain Code

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/external-tasks-and-grains

Execute an asynchronous worker task from grain code while maintaining Orleans' turn-based concurrency guarantees. Use .Unwrap() to handle the inner task.

```csharp
Task.Factory.StartNew(WorkerAsync).Unwrap()
```

--------------------------------

### Configure DynamoDB grain storage with JSON serialization

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence/dynamodb-storage

Configure the DynamoDB grain persistence provider to use JSON serialization for grain state. This snippet also includes options for authentication using profile and token.

```csharp
siloBuilder.AddDynamoDBGrainStorage(
  name: "profileStore",
  configureOptions: options =>
  {
      options.UseJson = true;
      options.AccessKey = "***";
      options.SecretKey = "***";
      options.Service = "***";
      options.ProfileName = "***";
      options.Token = "***";
  });


```

--------------------------------

### Orleans Client Connection Management

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/client

Manage the connection and disconnection of the Orleans cluster client within a hosted service. Ensure proper disposal of the client instance upon stopping the service.

```csharp
public class ClusterClientHostedService : IHostedService
{
    private readonly IClusterClient _client;

    public ClusterClientHostedService(IClusterClient client)
    {
        _client = client;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // A retry filter could be provided here.
        await _client.Connect();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.Close();

        _client.Dispose();
    }
}
```

--------------------------------

### Register the custom placement filter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Register the custom placement filter strategy and director with the service collection using AddPlacementFilter. This makes the filter available for use in the Orleans application.

```csharp
builder.Services.AddPlacementFilter<
    ExamplePreferLocalPlacementFilterStrategy,
    ExamplePreferLocalPlacementFilterDirector>();

```

--------------------------------

### Async Void Grain Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

An async void grain method that returns no value simply returns at the end of its execution.

```csharp
public async Task GrainMethod4()
{
    return;
}
```

--------------------------------

### Call a Stateless Worker Grain

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/stateless-worker-grains

Calling a stateless worker grain is similar to calling any other grain. Typically, a single grain ID like 0 or Guid.Empty is used. Subsequent requests may be processed by different activations.

```csharp
var worker = GrainFactory.GetGrain<IMyStatelessWorkerGrain>(0);
await worker.Process(args);
```

--------------------------------

### Define a Grain Service Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grainservices

Define the communication interface for a grain service by inheriting from IGrainService. This interface specifies the methods that the grain service will expose.

```csharp
public interface IDataService : IGrainService
{
    Task MyMethod();
}
```

--------------------------------

### Define Observable Lifecycle Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/orleans-lifecycle

The ILifecycleObservable interface allows components to subscribe to lifecycle events. Observers are identified by name and stage, and can unsubscribe by disposing of the returned object.

```csharp
public interface ILifecycleObservable
{
    IDisposable Subscribe(
        string observerName,
        int stage,
        ILifecycleObserver observer);
}

```

--------------------------------

### Expedite Grain Activation Deactivation in C#

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/activation-collection

Call DeactivateOnIdle to instruct the runtime to deactivate the grain activation the next time it becomes idle. This method takes priority over configuration settings and DelayDeactivation.

```csharp
protected void DeactivateOnIdle()
```

--------------------------------

### Grain with Component Dependency

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-lifecycle

A grain that has a component registered with the service container as a dependency. The component will automatically participate in the grain's lifecycle due to its registration.

```csharp
public class MyGrain : Grain, IMyGrain
{
    private readonly MyComponent _component;

    public MyGrain(MyComponent component)
    {
        _component = component;
    }
}
```

--------------------------------

### PreferredMatchSiloMetadata Placement Filter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-placement-filtering

Use PreferredMatchSiloMetadata to prefer silos matching specific metadata keys, with fallback logic and a minimum candidate count. This helps balance precise matching with avoiding hot silos.

```csharp
// Prefer silos with matching "zone" and "rack" values, but allow fallback
// minCandidates ensures at least 2 silos are considered even without matches
[PreferredMatchSiloMetadataPlacementFilter(new[] { "zone", "rack" }, minCandidates: 2)]
public class LocalityAwareGrain : Grain, ILocalityAwareGrain
{
    // Prefers silos in the same zone/rack as the caller, but can activate elsewhere
}
```

--------------------------------

### Register Logging Filter without Extension Method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Register the LoggingCallFilter class as a silo-wide grain call filter by directly adding it to the Dependency Injection services.

```csharp
siloHostBuilder.Services
    .AddSingleton<IIncomingGrainCallFilter, LoggingCallFilter>();

```

--------------------------------

### ClusterCollection for TestCluster Fixture

Source: https://learn.microsoft.com/en-us/dotnet/orleans/implementation/testing

Defines an xUnit collection fixture to group tests that share the `ClusterFixture`. This ensures that the `TestCluster` is managed as a single unit for the collection.

```csharp
[CollectionDefinition(Name)]
public sealed class ClusterCollection : ICollectionFixture<ClusterFixture>
{
    public const string Name = nameof(ClusterCollection);
}
```

--------------------------------

### Remove Localhost Clustering Configuration

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/deploy-scale-orleans-on-azure

Remove the existing Orleans configuration that uses localhost clustering and in-memory storage.

```csharp
builder.Host.UseOrleans(static siloBuilder =>
{
    siloBuilder
        .UseLocalhostClustering()
        .AddMemoryGrainStorage("urls");
});
```

--------------------------------

### RequestContext.Set

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-context

Use this method to store a value in the request context. The value can be any serializable type.

```APIDOC
## RequestContext.Set

### Description
Stores a key-value pair in the current request context. The value must be a serializable object.

### Method
```csharp
void Set(string key, object value)
```

### Parameters
#### Path Parameters
- **key** (string) - Required - The key for the metadata.
- **value** (object) - Required - The serializable value to store.

### Request Example
```csharp
RequestContext.Set("TraceId", Guid.NewGuid());
```
```

--------------------------------

### Define Receiver Grain with Implicit Subscription

Source: https://learn.microsoft.com/en-us/dotnet/orleans/streaming/streams-quick-start

Use the [ImplicitStreamSubscription] attribute to mark a grain for implicit subscription to a specified stream namespace. The runtime automatically activates this grain when data is pushed to the stream.

```csharp
[ImplicitStreamSubscription("RANDOMDATA")]
public class ReceiverGrain : Grain, IRandomReceiver
```

--------------------------------

### Define a Serializable Type with Orleans

Source: https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/serialization

Mark your types with [GenerateSerializer] and members with [Id] to enable Orleans serialization. Member identifiers are scoped to the type level.

```csharp
[GenerateSerializer]
public class Employee
{
    [Id(0)]
    public string Name { get; set; }
}
```

--------------------------------

### Azure Container Registry Bicep Template

Source: https://learn.microsoft.com/en-us/dotnet/orleans/deployment/deploy-to-azure-container-apps

Defines the Azure Container Registry (ACR) resource. This template outputs the ACR login server and name, which are used in subsequent deployments.

```bicep
param location string = resourceGroup().location

resource acr 'Microsoft.ContainerRegistry/registries@2021-09-01' = {
  name: toLower('${uniqueString(resourceGroup().id)}acr')
  location: location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}

output acrLoginServer string = acr.properties.loginServer
output acrName string = acr.name

```

--------------------------------

### Registering Server-Side Exception Filter

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

Register the custom ExceptionConversionFilter as an incoming grain call filter on the silo host builder. This enables the server-side exception handling logic.

```csharp
siloHostBuilder.AddIncomingGrainCallFilter<ExceptionConversionFilter>();
```

--------------------------------

### Handle Cancellation in a Grain Operation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Implement cancellation handling within a grain method by periodically checking the CancellationToken. Use `tc.CancellationToken.ThrowIfCancellationRequested()` to signal cancellation.

```csharp
public async Task LongIoWork(GrainCancellationToken tc, TimeSpan delay)
{
    // Periodically check if cancellation has been requested
    while (!tc.CancellationToken.IsCancellationRequested)
    {
        // Perform a portion of the work
        await IoOperation(tc.CancellationToken);

        // Example: tc.CancellationToken.ThrowIfCancellationRequested();
    }
    // Perform cleanup if necessary and then exit or throw OperationCanceledException
}
```

--------------------------------

### Grain Interface for Streaming with Cancellation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Defines an `IDataStreamGrain` interface that supports streaming data via `IAsyncEnumerable<T>` and accepts a `CancellationToken`. The `[EnumeratorCancellation]` attribute is used to enable cancellation token injection.

```csharp
using System.Runtime.CompilerServices;

public interface IDataStreamGrain : IGrainWithGuidKey
{
    IAsyncEnumerable<DataPoint> StreamDataAsync(int count, CancellationToken cancellationToken = default);
}
```

--------------------------------

### Removing Obsolete Method in V3 Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-versioning/backward-compatibility-guidelines

Shows the final step of removing an obsolete method in Version 3 of a grain interface after ensuring no V1 calls are active.

```csharp
[Version(3)]
public interface IMyGrain : IGrainWithIntegerKey
{
    // New method added in V2
    Task MyNewMethod(int arg, obj o);
}
```

--------------------------------

### Set Trace ID in Request Context (Client)

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/request-context

Use RequestContext.Set to add custom data like a trace ID to the outgoing request. This data is then available to the grain being called.

```C#
﻿using GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(clientBuilder =>
        clientBuilder.UseLocalhostClustering())
    .Build();

await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();

var grain = client.GetGrain<IHelloGrain>("friend");

var id = "example-id-set-by-client";

RequestContext.Set("TraceId", id);

var message = await friend.SayHello("Good morning!");

Console.WriteLine(message);
// Output:
//   TraceID: example-id-set-by-client
//   Client said: "Good morning!", so HelloGrain says: Hello!

```

--------------------------------

### Orleans Grain Interface Definition

Source: https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app

Defines the interface for the URL shortener grain, specifying the methods for setting and retrieving the full URL. This interface is used by clients to interact with the grain.

```csharp
// <graininterface>
public interface IUrlShortenerGrain : IGrainWithStringKey
{
    Task SetUrl(string fullUrl);

    Task<string> GetUrl();
}
// </graininterface>
```

--------------------------------

### Define a one-way grain interface method

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/oneway

Mark a grain interface method with the OneWayAttribute to make requests to it one-way. One-way methods must return Task or ValueTask, not their generic variants.

```csharp
public interface IOneWayGrain : IGrainWithGuidKey
{
    [OneWay]
    Task Notify(MyData data);
}
```

--------------------------------

### Notify Observers with Cancellation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/observers

When notifying observers, pass a cancellation token to enable cooperative cancellation. A linked token source can be used to combine the grain's token with a timeout for observer notifications.

```csharp
public async Task SendDataToObserversAsync(DataPayload data, CancellationToken cancellationToken = default)
{
    // Create a linked token source to combine the grain's token with a timeout
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    cts.CancelAfter(TimeSpan.FromSeconds(30)); // Timeout for observer notifications

    await _subsManager.NotifyAsync(
        observer => observer.OnDataReceivedAsync(data, cts.Token));
}
```

--------------------------------

### DataStreamGrain Implementation with Cancellation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

Implements `IDataStreamGrain`, yielding `DataPoint` objects. It checks for cancellation before each yield and supports cancellation within asynchronous operations like `Task.Delay`.

```csharp
public class DataStreamGrain : Grain, IDataStreamGrain
{
    public async IAsyncEnumerable<DataPoint>
    StreamDataAsync(
        int count,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < count; i++)
        {
            // Check cancellation before each yield
            cancellationToken.ThrowIfCancellationRequested();
            // Generate or fetch data
            var dataPoint = await GenerateDataPointAsync(i, cancellationToken);
            yield return dataPoint;

            // Optional: add delay with cancellation support
            await Task.Delay(100, cancellationToken);
        }
    }

    private async Task<DataPoint> GenerateDataPointAsync(int index, CancellationToken cancellationToken)
    {
        // Simulate data generation
        await Task.Delay(10, cancellationToken);
        return new DataPoint { Index = index, Value = Random.Shared.NextDouble() };
    }
}

public record DataPoint(int Index, double Value);
```

--------------------------------

### Require Basic Authorization for Dashboard Access

Source: https://learn.microsoft.com/en-us/dotnet/orleans/dashboard

Protect dashboard access by requiring authentication. This ensures only authenticated users can view the dashboard.

```csharp
// Require authentication for dashboard access
app.MapOrleansDashboard()
   .RequireAuthorization();

```

--------------------------------

### Define Streaming Grain Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains

Define a grain interface to return IAsyncEnumerable<T> for streaming data. Supports cancellation via CancellationToken.

```csharp
public interface IDataGrain : IGrainWithStringKey
{
    // Returns a streaming sequence of items
    IAsyncEnumerable<DataItem> GetAllItemsAsync();

    // Can also include CancellationToken for cancellation support
    IAsyncEnumerable<DataItem> GetItemsAsync(CancellationToken cancellationToken = default);
}
```

--------------------------------

### IIncomingGrainCallFilter Interface

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/interceptors

The `IIncomingGrainCallFilter` interface defines the contract for incoming grain call filters. Implement this interface to create custom filters that execute code before or after a grain method is invoked.

```APIDOC
## Interface: IIncomingGrainCallFilter

### Description
Represents an incoming grain call filter.

### Methods
#### Task Invoke(IIncomingGrainCallContext context)

- **context** (IIncomingGrainCallContext) - The context of the incoming grain call.

### Usage
Implement this interface and register it to intercept incoming grain calls.
```

--------------------------------

### Define Grain State Class

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/grain-persistence

Declare a serializable class to hold the state for a grain. This class defines the structure of the data that will be persisted.

```csharp
[Serializable]
public class ProfileState
{
    public string Name { get; set; }

    public Date DateOfBirth { get; set; }
}


```

--------------------------------

### Orleans Timeout Integration with Cancellation

Source: https://learn.microsoft.com/en-us/dotnet/orleans/grains/cancellation-tokens

When `CancelRequestOnTimeout` is enabled, Orleans automatically sends a cancellation signal to the grain if a request times out.

```csharp
// If this call times out, Orleans sends a cancellation signal to the grain
await grain.LongRunningOperationAsync(cancellationToken);
```