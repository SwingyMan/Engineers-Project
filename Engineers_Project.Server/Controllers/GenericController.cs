using Application.Commands;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[ApiController]
[GenericRestControllerNameConvention]
[Route("/api/v1/[controller]/[action]")]
public class GenericController<T,D> : ControllerBase
    where T : class
{
    private readonly IMediator _mediator;

    public GenericController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid key)
    {
        var query = new GenericGetByIdQuery<T>(key);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(GenericAddCommand<T,D> genericAddCommand)
    {
        var result = await _mediator.Send(genericAddCommand);
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<T,D> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a post.
    /// </summary>
    /// <param name="id">Post Guid</param>
    /// <response code="200">If the post was found.</response>
    /// <response code="404">If the post was not found.</response>
    // DELETE api/posts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<T>(id));
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<T> query)
    {
        return Ok(await _mediator.Send(query));
    }
}