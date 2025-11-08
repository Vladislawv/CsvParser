using CsvParser.Application;
using CsvParser.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CsvParser.ConsoleApplication;

public static class AssemblyConfigurator
{
    public static IHost CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services
                    .ConfigureApplicationServices()
                    .ConfigureInfrastructureServices(context.Configuration);
            })
            .Build();
    }
}