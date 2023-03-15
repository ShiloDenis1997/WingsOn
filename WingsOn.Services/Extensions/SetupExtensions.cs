using Microsoft.Extensions.DependencyInjection;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Services.Extensions;

public static class SetupExtensions
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        services.AddTransient<IPersonService, PersonService>();
        services.AddTransient<IFlightService, FlightService>();
        services.AddTransient<IBookingService, BookingService>();

        services.AddSingleton<IRepository<Person>, PersonRepository>();
        services.AddSingleton<IRepository<Flight>, FlightRepository>();
        services.AddSingleton<IRepository<Booking>, BookingRepository>();

        return services;
    }
}
