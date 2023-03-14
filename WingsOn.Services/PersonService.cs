using AutoMapper;
using Microsoft.Extensions.Logging;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Services.Dto;

namespace WingsOn.Services;

internal class PersonService : ServiceBase, IPersonService
{
    private readonly IRepository<Person> _personRepository;
    private readonly IRepository<Booking> _bookingRepository;

    public PersonService(IRepository<Person> personRepository, IRepository<Booking> bookingRepository, IMapper mapper,
        ILogger<PersonService> logger)
        : base(mapper, logger)
    {
        _personRepository = personRepository;
        _bookingRepository = bookingRepository;
    }

    public Task<PersonDto[]> GetManyAsync(GenderType? gender = null)
    {
        var people = _personRepository.GetAll();
        if (gender != null)
        {
            people = people.Where(x => x.Gender == gender);
        }

        var peopleDtos = Mapper.Map<PersonDto[]>(people);
        return Task.FromResult(peopleDtos);
    }

    public Task<PersonDto> GetByIdAsync(int id)
    {
        var person = _personRepository.Get(id);

        var personDto = Mapper.Map<PersonDto>(person);
        return Task.FromResult(personDto);
    }

    public Task<PersonDto[]> GetByFlightIdAsync(int flightId)
    {
        var passengers = _bookingRepository.GetAll().Where(b => b.Flight.Id == flightId)
            .SelectMany(b => b.Passengers);

        var peopleDtos = Mapper.Map<PersonDto[]>(passengers);
        return Task.FromResult(peopleDtos);
    }
}