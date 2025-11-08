using CsvParser.Domain.Entities;

namespace CsvParser.Application.Utils;

public static class MappingExtensions
{
    private static readonly TimeZoneInfo EstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

    public static Trip MapToEntity(this Trip trip)
    {
        return new Trip
        {
            PickupTime = ConvertEstToUtc(trip.PickupTime),
            DropoffTime = ConvertEstToUtc(trip.DropoffTime),
            PassengerCount = trip.PassengerCount,
            Distance = trip.Distance,
            StoreAndForwardFlag = ConvertStoreAndFwdFlag(trip.StoreAndForwardFlag),
            PickUpLocationId = trip.PickUpLocationId,
            DropOffLocationId = trip.DropOffLocationId,
            FareAmount = trip.FareAmount,
            TipAmount = trip.TipAmount
        };
    }

    private static DateTime ConvertEstToUtc(this DateTime estDateTime)
    {
        return TimeZoneInfo.ConvertTimeToUtc(estDateTime, EstTimeZone);
    }

    private static string ConvertStoreAndFwdFlag(this string flag)
    {
        return flag?.Trim()?.ToUpperInvariant() switch
        {
            "Y" => "Yes",
            _ => "No"
        };
    }
}