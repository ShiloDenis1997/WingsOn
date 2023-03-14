using AutoMapper;
using Microsoft.Extensions.Logging;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Services.Dto;

namespace WingsOn.Services;

internal class PersonService : IPersonService
{
    private readonly IRepository<Person> _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IRepository<Person> personRepository, IMapper mapper, ILogger<PersonService> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public Task<PersonDto[]> GetManyAsync()
    {
        var people = _personRepository.GetAll();
        var peopleDtos = _mapper.Map<PersonDto[]>(people);
        return Task.FromResult(peopleDtos);
    }

    public Task<PersonDto> GetByIdAsync(int id)
    {
        var person = _personRepository.Get(id);
        var personDto = _mapper.Map<PersonDto>(person);
        return Task.FromResult(personDto);
    }
}