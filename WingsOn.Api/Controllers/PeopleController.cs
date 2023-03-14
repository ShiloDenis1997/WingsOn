using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Domain;
using WingsOn.Services;
using Person = WingsOn.Api.Models.Person;

namespace WingsOn.Api.Controllers;

public class PeopleController : ApiControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService, IMapper mapper, ILogger<PeopleController> logger)
        : base(mapper, logger)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<IEnumerable<Person>> GetAsync([FromQuery] GenderType? gender)
    {
        var personDtos = await _personService.GetManyAsync(gender);
        return Mapper.Map<Person[]>(personDtos);
    }

    [HttpGet("{id}")]
    public async Task<Person> GetByIdAsync(int id)
    {
        var personDto = await _personService.GetByIdAsync(id);
        return Mapper.Map<Person>(personDto);
    }
}
