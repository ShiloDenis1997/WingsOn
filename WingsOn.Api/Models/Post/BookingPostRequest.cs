using System.ComponentModel.DataAnnotations;

namespace WingsOn.Api.Models.Post;

public class BookingPostRequest
{
    [Required]
    public string Number { get; set; }

    [Required]
    public int FlightId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [MinLength(1)]
    public IEnumerable<Person> Passengers { get; set; }
}
