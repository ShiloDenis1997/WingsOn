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
        CreateMap<Airline, AirlineDto>()
            .ReverseMap();
        CreateMap<Airport, AirportDto>()
            .ReverseMap();
        CreateMap<Flight, FlightDto>()
            .ReverseMap();
        CreateMap<Booking, BookingDto>()
            .ReverseMap();
    }
}
