using AutoMapper;
using Microsoft.Extensions.Logging;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Services.Dto;

namespace WingsOn.Services;

internal class FlightService : ServiceBase, IFlightService
{
    private readonly IRepository<Flight> _flightsRepository;

    public FlightService(IRepository<Flight> flightsRepository, IMapper mapper, ILogger<FlightService> logger)
        : base(mapper, logger)
    {
        _flightsRepository = flightsRepository;
    }

    public Task<FlightDto[]> GetManyAsync(string? number = null)
    {
        var flights = _flightsRepository.GetAll();
        if (!string.IsNullOrWhiteSpace(number))
        {
            flights = flights.Where(f => string.Equals(f.Number, number));
        }

        var flightsDtos = Mapper.Map<FlightDto[]>(flights);
        return Task.FromResult(flightsDtos);
    }
}