using AutoMapper;
using WingsOn.Api.Models;
using WingsOn.Services.Dto;

namespace WingsOn.Api.Mapping;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<PersonDto, Person>();
        CreateMap<AirlineDto, Airline>();
        CreateMap<AirportDto, Airport>();
        CreateMap<FlightDto, Flight>();
    }
}
