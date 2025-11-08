using CsvParser.Domain.DataSources;
using CsvParser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvParser.Infrastructure.Database.DataSources;

public class TripDataSource(CsvParserDbContext context) : ITripDataSource
{
    public async Task<short> GetPickUpLocationIdByHighestAverageTipAsync()
    {
        return await context.Trips
            .AsNoTracking()
            .GroupBy(trip => trip.PickUpLocationId)
            .Select(g => new
            {
                LocationId = g.Key,
                AverageTip = g.Average(trip => trip.TipAmount)
            })
            .OrderByDescending(x => x.AverageTip)
            .Select(x => x.LocationId)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<decimal>> GetLongestFaresByDistanceAsync(int size = 100)
    {
        return await context.Trips
            .OrderByDescending(trip => trip.Distance)
            .Take(size)
            .Select(x => x.FareAmount)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<decimal>> GetLongestFaresByTimeAsync(int size = 100)
    {
        return await context.Trips
            .OrderByDescending(trip => EF.Functions.DateDiffSecond(trip.PickupTime, trip.DropoffTime))
            .Take(size)
            .Select(x => x.FareAmount)
            .ToListAsync();
    }

    public async Task<Trip> GetByPickUpLocationIdAsync(int pickUpLocationId)
    {
        return await context.Trips.FirstOrDefaultAsync(x => x.PickUpLocationId == pickUpLocationId);
    }
}