using Autofac.Extras.Moq;
using AutoMapper;
using FluentAssertions;
using Moq;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Services;
using WingsOn.Services.Dto;
using WingsOn.Services.Dto.Create;
using WingsOn.Services.Exceptions;

namespace WingsOn.Tests;

public class BookingServiceTests
{
    private BookingService _sut = null!;
    private AutoMock _mocker = null!;

    [SetUp]
    public void Setup()
    {
        _mocker = AutoMock.GetLoose();
        _sut = _mocker.Create<BookingService>();
    }

    [Test]
    public async Task GetMany_Ok()
    {
        var dalBookings = new [] { new Booking { Id = 1 }, new Booking { Id = 2 } };
        _mocker.Mock<IRepository<Booking>>().Setup(x => x.GetAll()).Returns(dalBookings);
        var mappedBookings = new[] { new BookingDto { Id = 1 }, new BookingDto { Id = 2 } };
        _mocker.Mock<IMapper>().Setup(x => x.Map<BookingDto[]>(dalBookings)).Returns(mappedBookings);

        var result = await _sut.GetManyAsync();

        result.Should().BeEquivalentTo(mappedBookings);
    }

    [Test]
    public async Task CreateAsync_TwoNewPassengersAndOneExisting_CreatesNewPassengers_Ok()
    {
        var existingPassengerId = 6;
        var firstNewPassengerId = existingPassengerId + 1;
        var secondNewPassengerId = firstNewPassengerId + 1;
        var bookingCreateDto = new BookingCreateDto
        {
            CustomerId = 4,
            FlightId = 9,
            Number = "42",
            Passengers = new[]
            {
                new PersonDto { Id = 0 },
                new PersonDto { Id = 0 },
                new PersonDto { Id = existingPassengerId }
            }
        };
        var maxBookingId = 5;
        var newBookingId = maxBookingId + 1;
        var existingBookings = new[] { new Booking { Id = 2 }, new Booking { Id = maxBookingId } };
        var flight = new Flight { Id = bookingCreateDto.FlightId };
        var existingPassenger = new Person { Id = existingPassengerId };
        var customer = new Person { Id = bookingCreateDto.CustomerId };
        _mocker.Mock<IRepository<Booking>>().Setup(x => x.GetAll()).Returns(existingBookings);
        _mocker.Mock<IRepository<Flight>>().Setup(x => x.Get(bookingCreateDto.FlightId)).Returns(flight);
        _mocker.Mock<IRepository<Person>>().Setup(x => x.Get(bookingCreateDto.CustomerId)).Returns(customer);
        _mocker.Mock<IRepository<Person>>().Setup(x => x.Get(existingPassengerId)).Returns(existingPassenger);
        _mocker.Mock<IRepository<Person>>().Setup(x => x.GetAll()).Returns(new[] { existingPassenger });
        _mocker.Mock<IMapper>().Setup(x => x.Map<Person>(It.IsAny<PersonDto>()))
            .Returns((PersonDto dto) => new Person { Id = dto.Id });
        var mappedBookingResult = new BookingDto();
        _mocker.Mock<IMapper>().Setup(x => x.Map<BookingDto>(It.Is<Booking>(b => b.Id == newBookingId)))
            .Returns(mappedBookingResult);

        var utcBefore = DateTime.UtcNow;
        var result = await _sut.CreateAsync(bookingCreateDto);
        var utcAfter = DateTime.UtcNow;

        result.Should().Be(mappedBookingResult);
        _mocker.Mock<IRepository<Booking>>().Verify(x =>
            x.Save(It.Is<Booking>(b => b.Number == bookingCreateDto.Number && b.DateBooking >= utcBefore && b.DateBooking <= utcAfter
            && b.Flight.Id == bookingCreateDto.FlightId
            && b.Customer.Id == bookingCreateDto.CustomerId
            && b.Id == newBookingId
            && b.Passengers.Count() == 3
            && b.Passengers.All(p => p.Id >= existingPassengerId))));
        _mocker.Mock<IRepository<Person>>().Verify(x => x.Save(It.Is<Person>(p => p.Id == firstNewPassengerId)));
        _mocker.Mock<IRepository<Person>>().Verify(x => x.Save(It.Is<Person>(p => p.Id == secondNewPassengerId)));
    }

    [Test]
    public void CreateAsync_InvalidCustomerId_NotFoundException()
    {
        var existingPassengerId = 6;
        var bookingCreateDto = new BookingCreateDto
        {
            CustomerId = 8,
            FlightId = 9,
            Passengers = new[]
            {
                new PersonDto { Id = existingPassengerId }
            }
        };
        _mocker.Mock<IRepository<Person>>().Setup(x => x.Get(bookingCreateDto.CustomerId)).Returns((Person)null!);

        var exception = Assert.ThrowsAsync<NotFoundException>(() => _sut.CreateAsync(bookingCreateDto));
        exception.Message.Should().Be("Person with id '8' is not found.");
    }

    [Test]
    public void CreateAsync_InvalidFlightId_NotFoundException()
    {
        var existingPassengerId = 6;
        var bookingCreateDto = new BookingCreateDto
        {
            CustomerId = 8,
            FlightId = 9,
            Passengers = new[]
            {
                new PersonDto { Id = existingPassengerId }
            }
        };
        _mocker.Mock<IRepository<Person>>().Setup(x => x.Get(bookingCreateDto.CustomerId)).Returns(new Person());
        _mocker.Mock<IRepository<Flight>>().Setup(x => x.Get(bookingCreateDto.CustomerId)).Returns((Flight)null!);

        var exception = Assert.ThrowsAsync<NotFoundException>(() => _sut.CreateAsync(bookingCreateDto));
        exception.Message.Should().Be("Flight with id '9' is not found.");
    }

    [Test]
    public void CreateAsync_InvalidPassengerId_NotFoundException()
    {
        var invalidPassengerId = 6;
        var bookingCreateDto = new BookingCreateDto
        {
            CustomerId = 8,
            FlightId = 9,
            Passengers = new[]
            {
                new PersonDto { Id = 0 },
                new PersonDto { Id = invalidPassengerId }
            }
        };
        var existingPassengers = new[] { new Person { Id = 5 } };
        _mocker.Mock<IRepository<Person>>().Setup(x => x.GetAll()).Returns(existingPassengers);
        _mocker.Mock<IRepository<Person>>().Setup(x => x.Get(bookingCreateDto.CustomerId)).Returns(new Person());
        _mocker.Mock<IRepository<Flight>>().Setup(x => x.Get(bookingCreateDto.FlightId)).Returns(new Flight());
        _mocker.Mock<IRepository<Person>>().Setup(x => x.Get(invalidPassengerId)).Returns((Person)null!);

        var exception = Assert.ThrowsAsync<NotFoundException>(() => _sut.CreateAsync(bookingCreateDto));
        exception.Message.Should().Be("Person with id '6' is not found.");
    }
}