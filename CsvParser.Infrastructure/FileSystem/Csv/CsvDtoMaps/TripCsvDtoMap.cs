using CsvHelper.Configuration;
using CsvParser.Domain.Entities;
using CsvParser.Infrastructure.FileSystem.Csv.CsvTypeConverters;

namespace CsvParser.Infrastructure.FileSystem.Csv.CsvDtoMaps;

public sealed class TripCsvDtoMap : ClassMap<Trip>
{
    public TripCsvDtoMap()
    {
        Map(m => m.PickupTime).Name("tpep_pickup_datetime");
        Map(m => m.DropoffTime).Name("tpep_dropoff_datetime");
        Map(m => m.PassengerCount).Name("passenger_count").TypeConverter<ByteDefaultConverter>();
        Map(m => m.Distance).Name("trip_distance");
        Map(m => m.StoreAndForwardFlag).Name("store_and_fwd_flag");
        Map(m => m.FareAmount).Name("fare_amount");
        Map(m => m.TipAmount).Name("tip_amount");
        Map(m => m.PickUpLocationId).Name("PULocationID");
        Map(m => m.DropOffLocationId).Name("DOLocationID");
    }
}
