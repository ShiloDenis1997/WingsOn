using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Services;

namespace WingsOn.Api.Controllers;

public class FlightsController : ApiControllerBase
{
    private readonly IFlightService _flightService;
    private readonly IPersonService _personService;

    public FlightsController(IFlightService flightService, IPersonService personService, IMapper mapper,
        ILogger<PeopleController> logger)
        : base(mapper, logger)
    {
        _flightService = flightService;
        _personService = personService;
    }

    [HttpGet]
    public async Task<IEnumerable<Flight>> GetManyAsync([FromQuery] string? number)
    {
        var flightDtos = await _flightService.GetManyAsync(number);
        return Mapper.Map<Flight[]>(flightDtos);
    }

    [HttpGet("{flightId}/passengers")]
    public async Task<IEnumerable<Person>> GetPassengersAsync(int flightId)
    {
        var personDtos = await _personService.GetByFlightIdAsync(flightId);
        return Mapper.Map<Person[]>(personDtos);
    }
}
