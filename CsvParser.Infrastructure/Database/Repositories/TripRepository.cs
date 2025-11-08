using CsvParser.Domain.Entities;
using CsvParser.Domain.Repositories;

namespace CsvParser.Infrastructure.Database.Repositories;

public class TripRepository(CsvParserDbContext context) : ITripRepository
{
    public async Task AddRangeAsync(IEnumerable<Trip> trips, int chunkSize = 5000)
    {
        if (!trips.Any())
        {
            return;
        }

        var totalRecords = trips.Count();
        var processedRecords = 0;

        foreach (var chunk in trips.Chunk(chunkSize))
        {
            await context.Trips.AddRangeAsync(chunk);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            processedRecords += chunk.Length;
            Utils.LogProgressToConsole(processedRecords, totalRecords);
        }

        Console.WriteLine();
        Console.WriteLine("All unique trips have been saved to the database");
    }
}