using WingsOn.Services.Dto;

namespace WingsOn.Services;

public interface IPersonService
{
    Task<PersonDto[]> GetManyAsync();
    Task<PersonDto> GetByIdAsync(int id);
}