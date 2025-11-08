using CsvParser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvParser.Infrastructure.Database;

public class CsvParserDbContext(DbContextOptions<CsvParserDbContext> options) : DbContext(options)
{
    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CsvParserDbContext).Assembly);
    }
}