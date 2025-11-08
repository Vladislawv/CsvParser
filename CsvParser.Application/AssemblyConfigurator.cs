using CsvParser.Application.Abstractions;
using CsvParser.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CsvParser.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        return services.AddScoped<ITripService, TripService>();
    }
}