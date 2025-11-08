using CsvParser.Application.Abstractions;

namespace CsvParser.ConsoleApplication.Services;

public static class CsvParserConsoleService
{
    private const string FilePath = "sample-cab-data.csv";

    public static async Task RunAsync(ITripService tripService)
    {
        while (true)
        {
            ShowMenu();

            var key = Console.ReadKey(intercept: true);
            Console.WriteLine();
            if (key.Key == ConsoleKey.Q)
            {
                Console.WriteLine("Exiting application...");
                break;
            }

            switch (key.KeyChar)
            {
                case '1': await ProcessCsvFileAsync(tripService); break;
                case '2': await ShowPickUpLocationIdByHighestAverageTipAsync(tripService); break;
                case '3': await ShowLongestFaresByDistanceAsync(tripService); break;
                case '4': await ShowLongestFaresByTimeAsync(tripService); break;
                case '5': await ShowTripByPickUpLocationIdAsync(tripService); break;
                default: Console.WriteLine("Invalid option. Please select 1-5."); break;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(intercept: true);
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
        Console.WriteLine("Press Q to exit");
        Console.WriteLine();
        Console.Write("Select option (1-5): ");
    }

    private static async Task ProcessCsvFileAsync(ITripService tripService)
    {
        Console.WriteLine($"{nameof(ProcessCsvFileAsync)} started");

        await tripService.ProcessCsvFileAsync(FilePath);
        
        Console.WriteLine($"{nameof(ProcessCsvFileAsync)} finished successfully");
    }
    
    private static async Task ShowPickUpLocationIdByHighestAverageTipAsync(ITripService tripService)
    {
        var pickUpLocationId = await tripService.GetPickUpLocationIdByHighestAverageTipAsync();
        Console.WriteLine($"Pick up location id with highest average tip is {pickUpLocationId}");
    }

    private static async Task ShowLongestFaresByDistanceAsync(ITripService tripService)
    {
        var fares = await tripService.GetLongestFaresByDistanceAsync();
        Console.WriteLine("Longest fares by distance is");
        LogFaresToConsole(fares);
    }

    private static async Task ShowLongestFaresByTimeAsync(ITripService tripService)
    {
        var fares = await tripService.GetLongestFaresByTimeAsync();
        Console.WriteLine("Longest fares by time is");
        LogFaresToConsole(fares);
    }
    
    private static async Task ShowTripByPickUpLocationIdAsync(ITripService tripService)
    {
        Console.WriteLine("Enter pick up location id");
        var pickUpLocationId = int.Parse(Console.ReadLine());
        var trip = await tripService.GetByPickUpLocationIdAsync(pickUpLocationId);

        var result = trip == null ? $"Trip with pick up location id: {pickUpLocationId} in not exists" : trip.ToString(); 
        Console.WriteLine(result);
    }

    private static void LogFaresToConsole(IReadOnlyList<decimal> fares)
    {
        Console.WriteLine($"{"#",3} | {"Fare Amount",12}");
        Console.WriteLine(new string('-', 20));

        var fareIndex = 1;
        foreach (var fare in fares)
        {
            Console.WriteLine($"{fareIndex++,3} | {fare,12:F2}");
        }
    }
}