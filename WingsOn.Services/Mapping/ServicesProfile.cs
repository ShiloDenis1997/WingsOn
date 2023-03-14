using AutoMapper;
using WingsOn.Domain;
using WingsOn.Services.Dto;

namespace WingsOn.Services.Mapping;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        CreateMap<Person, PersonDto>()
            .ReverseMap();
    }
}
