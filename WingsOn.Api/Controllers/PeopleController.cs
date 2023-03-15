using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Api.Models.Patch;
using WingsOn.Domain;
using WingsOn.Services;
using WingsOn.Services.Dto.Update;
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
    public async Task<IEnumerable<Person>> GetManyAsync([FromQuery] GenderType? gender)
    {
        var personDtos = await _personService.GetManyAsync(gender);
        return Mapper.Map<Person[]>(personDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var personDto = await _personService.GetByIdAsync(id);
        if (personDto == null)
        {
            return NotFound(new ErrorResponse($"Person with id '{id}' is not found."));
        }

        return Ok(Mapper.Map<Person>(personDto));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
    public async Task<Person> PatchAsync([FromRoute] int id, [FromBody] PersonPatchRequest patchRequest)
    {
        var updateDto = Mapper.Map<PersonUpdateDto>(patchRequest);
        var updatedPersonDto = await _personService.UpdatePersonAsync(id, updateDto);

        return Mapper.Map<Person>(updatedPersonDto);
    }
}
