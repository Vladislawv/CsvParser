using CsvParser.Application.Abstractions;
using CsvParser.ConsoleApplication.Services;
using CsvParser.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CsvParser.ConsoleApplication;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var host = AssemblyConfigurator.CreateHostBuilder(args);
        using (var scope = host.Services.CreateScope())
        {
            scope.InitializeInfrastructure();
            await CsvParserConsoleService.RunAsync(scope.ServiceProvider.GetRequiredService<ITripService>());
        }
    }
}