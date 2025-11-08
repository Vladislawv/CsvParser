using CsvParser.Domain.Entities;

namespace CsvParser.Domain.DataSources;

public interface ITripDataSource
{
    Task<short> GetPickUpLocationIdByHighestAverageTipAsync();
    Task<IReadOnlyList<decimal>> GetLongestFaresByDistanceAsync(int size = 100);
    Task<IReadOnlyList<decimal>> GetLongestFaresByTimeAsync(int size = 100);
    Task<Trip> GetByPickUpLocationIdAsync(int pickUpLocationId);
}