using CsvParser.Domain.Entities;

namespace CsvParser.Application.Abstractions;

public interface ITripService
{
    Task ProcessCsvFileAsync(string path);
    Task<short> GetPickUpLocationIdByHighestAverageTipAsync();
    Task<IReadOnlyList<decimal>> GetLongestFaresByDistanceAsync();
    Task<IReadOnlyList<decimal>> GetLongestFaresByTimeAsync();
    Task<Trip> GetByPickUpLocationIdAsync(int pickUpLocationId);
}