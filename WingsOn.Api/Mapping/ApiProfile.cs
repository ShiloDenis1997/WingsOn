using AutoMapper;
using WingsOn.Api.Models;
using WingsOn.Api.Models.Patch;
using WingsOn.Api.Models.Post;
using WingsOn.Services.Dto;
using WingsOn.Services.Dto.Create;
using WingsOn.Services.Dto.Update;

namespace WingsOn.Api.Mapping;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<PersonDto, Person>()
            .ForMember(dest => dest.DateBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateBirth)))
            .ReverseMap()
            .ForMember(dest => dest.DateBirth, opt => opt.MapFrom(src => src.DateBirth.ToDateTime(TimeOnly.MinValue)));
        CreateMap<AirlineDto, Airline>();
        CreateMap<AirportDto, Airport>();
        CreateMap<FlightDto, Flight>();
        CreateMap<BookingDto, Booking>();
        CreateMap<PersonPatchRequest, PersonUpdateDto>();
        CreateMap<BookingPostRequest, BookingCreateDto>();
    }
}
