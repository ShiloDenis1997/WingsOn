using WingsOn.Domain;

namespace WingsOn.Services.Dto;

public class PersonDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateBirth { get; set; }

    public GenderType Gender { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;
}
