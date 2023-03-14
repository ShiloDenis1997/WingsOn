using WingsOn.Services.Dto;

namespace WingsOn.Services;

public interface IFlightService
{
    Task<FlightDto[]> GetManyAsync(string? number = null);
}