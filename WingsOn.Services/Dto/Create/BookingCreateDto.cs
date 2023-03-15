namespace WingsOn.Services.Dto.Create;

public class BookingCreateDto
{
    public string Number { get; set; }

    public int FlightId { get; set; }

    public int CustomerId { get; set; }

    public IEnumerable<PersonDto> Passengers { get; set; }
}
