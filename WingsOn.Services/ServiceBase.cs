using AutoMapper;
using Microsoft.Extensions.Logging;

namespace WingsOn.Services;

internal abstract class ServiceBase
{
    protected readonly IMapper Mapper;
    protected readonly ILogger Logger;

    protected ServiceBase(IMapper mapper, ILogger logger)
    {
        Mapper = mapper;
        Logger = logger;
    }
}
