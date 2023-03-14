using AutoMapper;

namespace WingsOn.Api.Mapping;

public static class SetupExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        return services.AddSingleton<IMapper>(_ =>
        {
            var mapperConfigurationProvider = new MapperConfigurationProvider();
            var configurationProvider = new MapperConfiguration(mapperConfigurationProvider);

            return new Mapper(configurationProvider);
        });
    }
}
