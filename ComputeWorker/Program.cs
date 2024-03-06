using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ComputeWorker;
using ComputeWorker.Utils;

var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults();

hostBuilder.ConfigureServices(services =>
{
    services.AddTransient<ISolver, Solver>();
    services.AddTransient<IMoveBuilder, MoveBuilder>();
});

var host = hostBuilder.Build();


host.Run();

