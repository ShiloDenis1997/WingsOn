using AutoMapper;
using WingsOn.Services.Mapping;

namespace WingsOn.Api.Mapping;

public class MapperConfigurationProvider : MapperConfigurationExpression
{
    public MapperConfigurationProvider()
    {
        AddProfile<ApiProfile>();
        AddProfile<ServicesProfile>();
    }
}
