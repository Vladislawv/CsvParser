using CsvParser.Domain.Entities;

namespace CsvParser.Application.Comparers;

public sealed class TripComparer : IEqualityComparer<Trip>
{
    public bool Equals(Trip x, Trip y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.PickupTime == y.PickupTime
               && x.DropoffTime == y.DropoffTime
               && x.PassengerCount == y.PassengerCount;
    }
        

    public int GetHashCode(Trip obj) =>
        HashCode.Combine(obj.PickupTime, obj.DropoffTime, obj.PassengerCount);
}
