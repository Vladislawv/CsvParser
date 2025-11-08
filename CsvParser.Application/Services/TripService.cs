using CsvParser.Application.Abstractions;
using CsvParser.Application.Comparers;
using CsvParser.Application.Utils;
using CsvParser.Domain.DataSources;
using CsvParser.Domain.Entities;
using CsvParser.Domain.Repositories;

namespace CsvParser.Application.Services;

public class TripService(IFileService fileService, ITripRepository repository, ITripDataSource dataSource) 
    : ITripService
{
    public async Task ProcessCsvFileAsync(string path)
    {
        Console.WriteLine($"{nameof(ProcessCsvFileAsync)} started");

        var trips = fileService.ReadStreamAsync<Trip>(path);
        var (uniqueTripCsvDto, duplicateTripCsvDto) = await FilterDuplicatesAsync(trips);

        var uniqueTrips = uniqueTripCsvDto.Select(MappingExtensions.MapToEntity);
        await repository.AddRangeAsync(uniqueTrips);
        
        if (duplicateTripCsvDto.Count != 0)
        {
            var duplicatesFileName = "duplicates.csv";
            await fileService.WriteRangeAsync(duplicatesFileName, duplicateTripCsvDto);
            Console.WriteLine($"All duplicated records have been saved to the file {duplicatesFileName}");
        }

        Console.WriteLine($"{nameof(ProcessCsvFileAsync)} finished successfully");
    }

    public async Task<short> GetPickUpLocationIdByHighestAverageTipAsync()
    {
        return await dataSource.GetPickUpLocationIdByHighestAverageTipAsync();
    }

    public async Task<IReadOnlyList<decimal>> GetLongestFaresByDistanceAsync()
    {
        return await dataSource.GetLongestFaresByDistanceAsync();
    }

    public async Task<IReadOnlyList<decimal>> GetLongestFaresByTimeAsync()
    {
        return await dataSource.GetLongestFaresByTimeAsync();
    }

    public async Task<Trip> GetByPickUpLocationIdAsync(int pickUpLocationId)
    {
        return await dataSource.GetByPickUpLocationIdAsync(pickUpLocationId);
    }

    private static async Task<(List<Trip> unique, List<Trip> duplicates)> FilterDuplicatesAsync(
        IAsyncEnumerable<Trip> trips)
    {
        var unique = new List<Trip>();
        var duplicates = new List<Trip>();
        var seen = new HashSet<Trip>(new TripComparer());

        await foreach (var trip in trips)
        {
            if (!seen.Add(trip))
            {
                duplicates.Add(trip);
            }
            else
            {
                unique.Add(trip);
            }
        }

        return (unique, duplicates);
    }
}