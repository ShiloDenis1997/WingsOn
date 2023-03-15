using WingsOn.Domain;

namespace WingsOn.Api.Models;

public class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly DateBirth { get; set; }

    public GenderType Gender { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;
}
