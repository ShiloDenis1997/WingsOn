using Microsoft.Extensions.DependencyInjection;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Services.Extensions;

public static class SetupExtensions
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        services.AddTransient<IPersonService, PersonService>();

        services.AddSingleton<IRepository<Person>, PersonRepository>();

        return services;
    }
}
