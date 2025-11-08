using CsvParser.Domain.Entities;
using CsvParser.Domain.Repositories;

namespace CsvParser.Infrastructure.Database.Repositories;

public class TripRepository(CsvParserDbContext context) : ITripRepository
{
    public async Task AddRangeAsync(IEnumerable<Trip> trips, int chunkSize = 5000)
    {
        foreach (var chunk in trips.Chunk(chunkSize))
        {
            await context.Trips.AddRangeAsync(chunk);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
        }
    }
}