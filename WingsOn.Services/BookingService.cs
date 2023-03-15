using AutoMapper;
using Microsoft.Extensions.Logging;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Services.Dto;
using WingsOn.Services.Dto.Create;
using WingsOn.Services.Exceptions;

namespace WingsOn.Services;

internal class BookingService : ServiceBase, IBookingService
{
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Flight> _flightRepository;
    private readonly IRepository<Person> _personRepository;

    public BookingService(IRepository<Booking> bookingRepository,
        IRepository<Flight> flightRepository,
        IRepository<Person> personRepository,
        IMapper mapper,
        ILogger<BookingService> logger) : base(mapper, logger)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
        _personRepository = personRepository;
    }

    public Task<BookingDto[]> GetManyAsync()
    {
        var bookings = _bookingRepository.GetAll();

        var bookingDtos = Mapper.Map<BookingDto[]>(bookings);
        return Task.FromResult(bookingDtos);
    }

    public Task<BookingDto> CreateAsync(BookingCreateDto bookingCreateDto)
    {
        var newBooking = new Booking
        {
            Number = bookingCreateDto.Number,
            DateBooking = DateTime.UtcNow,
            Customer = GetPerson(bookingCreateDto.CustomerId),
            Flight = GetFlight(bookingCreateDto.FlightId)
        };

        var bookingPassengers = new List<Person>();
        var maxPersonId = _personRepository.GetAll().Max(p => p.Id); // just to emulate new id assignment as it's not done by dal
        foreach (var passenger in bookingCreateDto.Passengers)
        {
            if (passenger.Id != 0)
            {
                var person = GetPerson(passenger.Id);
                bookingPassengers.Add(person);
            }
            else
            {
                passenger.Id = ++maxPersonId;
                var newPassenger = Mapper.Map<Person>(passenger);
                _personRepository.Save(newPassenger);
                bookingPassengers.Add(newPassenger);
            }
        }

        var maxBookingId = _bookingRepository.GetAll().Max(b => b.Id); // just to emulate new id assignment as it's not done by dal
        newBooking.Id = ++maxBookingId;
        newBooking.Passengers = bookingPassengers;
        _bookingRepository.Save(newBooking);

        var bookingDto = Mapper.Map<BookingDto>(newBooking);
        return Task.FromResult(bookingDto);
    }

    private Person GetPerson(int personId)
    {
        var customer = _personRepository.Get(personId);
        if (customer == null)
        {
            throw new NotFoundException($"Person with id '{personId}' is not found.");
        }

        return customer;
    }

    private Flight GetFlight(int flightId)
    {
        var flight = _flightRepository.Get(flightId);
        if (flight == null)
        {
            throw new NotFoundException($"Flight with id '{flightId}' is not found.");
        }

        return flight;
    }
}