using CsvParser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsvParser.Infrastructure.Database.EntityConfigurations;

public class TripEntityConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");

        builder.HasIndex(e => new { e.PickupTime, e.DropoffTime });
        builder.HasIndex(e => e.Distance);
        builder.HasIndex(e => e.PickUpLocationId);

        builder.Property(e => e.FareAmount)
            .HasColumnType("decimal(6,2)")
            .IsRequired();

        builder.Property(e => e.TipAmount)
            .HasColumnType("decimal(5,2)")
            .IsRequired();
    }
}