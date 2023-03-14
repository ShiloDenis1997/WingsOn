using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WingsOn.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMapper Mapper;
    protected readonly ILogger Logger;

    protected ApiControllerBase(IMapper mapper, ILogger logger)
    {
        Mapper = mapper;
        Logger = logger;
    }
}
