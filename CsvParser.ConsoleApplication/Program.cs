using CsvParser.Application.Abstractions;
using CsvParser.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CsvParser.ConsoleApplication;

internal static class Program
{
    private const string FilePath = "sample-cab-data.csv";
    
    private static async Task Main(string[] args)
    {
        var host = AssemblyConfigurator.CreateHostBuilder(args);
        using (var scope = host.Services.CreateScope())
        {
            scope.InitializeInfrastructure();
            await RunApplicationAsync(scope.ServiceProvider.GetRequiredService<ITripService>());
        }
    }

    private static async Task RunApplicationAsync(ITripService tripService)
    {
        while (true)
        {
            ShowMenu();

            var key = Console.ReadKey(intercept: true);
            Console.WriteLine();
            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Exiting application...");
                break;
            }

            switch (key.KeyChar)
            {
                case '1': await tripService.ProcessCsvFileAsync(FilePath); break;
                case '2': await tripService.GetPickUpLocationIdByHighestAverageTipAsync(); break;
                case '3': await tripService.GetLongestFaresByDistanceAsync(); break;
                case '4': await tripService.GetLongestFaresByTimeAsync(); break;
                case '5':
                    var pickUpLocationId = Console.Read();
                    await tripService.GetByPickUpLocationIdAsync(pickUpLocationId);
                    break;

                default: Console.WriteLine("Invalid option. Please select 1-5."); break;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== CSV Parser Application ===");
        Console.WriteLine();
        Console.WriteLine("1. Extract data from CSV to the database");
        Console.WriteLine("2. Find pick up location id with highest average tip");
        Console.WriteLine("3. Find top 100 longest fares by distance");
        Console.WriteLine("4. Find top 100 longest fares by time");
        Console.WriteLine("5. Search by pick up location id");
        Console.WriteLine();
        Console.WriteLine("Press ESC to exit");
        Console.WriteLine();
        Console.Write("Select option (1-5): ");
    }
}