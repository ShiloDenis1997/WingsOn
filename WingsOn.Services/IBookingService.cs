using WingsOn.Services.Dto;
using WingsOn.Services.Dto.Create;

namespace WingsOn.Services;

public interface IBookingService
{
    Task<BookingDto[]> GetManyAsync();
    Task<BookingDto> CreateAsync(BookingCreateDto bookingCreateDto);
}