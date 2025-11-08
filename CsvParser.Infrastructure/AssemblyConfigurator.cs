using CsvParser.Application.Abstractions;
using CsvParser.Domain.DataSources;
using CsvParser.Domain.Repositories;
using CsvParser.Infrastructure.Database;
using CsvParser.Infrastructure.Database.DataSources;
using CsvParser.Infrastructure.Database.Repositories;
using CsvParser.Infrastructure.FileSystem.Csv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvParser.Infrastructure;

public static class AssemblyConfigurator
{
    private const string DatabasePrefix = "MsSqlConnection";

    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<ITripRepository, TripRepository>()
            .AddScoped<ITripDataSource, TripDataSource>()
            .AddScoped<IFileService, CsvFileService>()
            .AddDbContext<CsvParserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(DatabasePrefix)));
    }

    public static void InitializeInfrastructure(this IServiceScope scope)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<CsvParserDbContext>();
        dbContext.Database.EnsureCreated();
    }
}