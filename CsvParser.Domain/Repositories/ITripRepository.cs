using CsvParser.Domain.Entities;

namespace CsvParser.Domain.Repositories;

public interface ITripRepository
{
    Task AddRangeAsync(IEnumerable<Trip> trips, int chunkSize = 5000);
}