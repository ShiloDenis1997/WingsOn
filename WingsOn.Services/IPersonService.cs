using WingsOn.Domain;
using WingsOn.Services.Dto;
using WingsOn.Services.Dto.Update;

namespace WingsOn.Services;

public interface IPersonService
{
    Task<PersonDto[]> GetManyAsync(GenderType? gender = null);
    Task<PersonDto?> GetByIdAsync(int id);
    Task<PersonDto[]> GetByFlightIdAsync(int flightId);
    Task<PersonDto> UpdatePersonAsync(int id, PersonUpdateDto personUpdateDto);
}