namespace WingsOn.Api.Models;

public class Booking
{
    public int Id { get; set; }

    public string Number { get; set; }

    public Flight Flight { get; set; }

    public Person Customer { get; set; }

    public IEnumerable<Person> Passengers { get; set; }

    public DateTime DateBooking { get; set; }
}
