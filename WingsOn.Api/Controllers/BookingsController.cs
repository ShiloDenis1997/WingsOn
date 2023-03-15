using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Api.Models.Post;
using WingsOn.Services;
using WingsOn.Services.Dto.Create;

namespace WingsOn.Api.Controllers;

public class BookingsController : ApiControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService, IMapper mapper, ILogger<BookingsController> logger) :
        base(mapper, logger)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IEnumerable<Booking>> GetManyAsync()
    {
        var bookingDtos = await _bookingService.GetManyAsync();
        return Mapper.Map<Booking[]>(bookingDtos);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BookingPostRequest postRequest)
    {
        var createDto = Mapper.Map<BookingCreateDto>(postRequest);
        var newBooking = await _bookingService.CreateAsync(createDto);

        return Created($"{Request.GetDisplayUrl()}/{newBooking.Id}", newBooking);
    }
}
