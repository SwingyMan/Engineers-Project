using Application.Commands;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[ApiController]
[GenericRestControllerNameConvention]
[Route("[controller]")]
public class GenericController<T, TRequest, TResponse> : ControllerBase
    where T : class where TRequest : class where TResponse : class
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GenericController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<TResponse>> Get(Guid key)
    {
        var query = new GenericGetByIdQuery<T>(key);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return _mapper.Map<T, TResponse>(result);
    }

    [HttpPost]
    public async Task<ActionResult<TResponse>> Create(TRequest request)
    {
        var command = new GenericAddCommand<TRequest, TResponse>(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}