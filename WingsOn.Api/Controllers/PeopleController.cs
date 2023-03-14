using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Services;

namespace WingsOn.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(IPersonService personService, IMapper mapper, ILogger<PeopleController> logger)
    {
        _personService = personService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Person>> GetAsync()
    {
        var peopleDtos = await _personService.GetManyAsync();
        return _mapper.Map<Person[]>(peopleDtos);
    }

    [HttpGet("{id}")]
    public async Task<Person> GetByIdAsync(int id)
    {
        var personDto = await _personService.GetByIdAsync(id);
        return _mapper.Map<Person>(personDto);
    }
}
