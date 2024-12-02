using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a Group by its Guid.
    /// </summary>
    /// <param name="id">Group Guid</param>
    /// <returns>The retrieved Group, if found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var group = await _mediator.Send(new GenericGetByIdQuery<GroupDTO>(id));
        if (group == null) return NotFound();
        return Ok(group);
    }

    /// <summary>
    ///     Creates a Group.
    /// </summary>
    /// <param name="genericAddCommand">GroupDTO</param>
    /// <returns>The created Group.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<GroupDTO, Group> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a Group.
    /// </summary>
    /// <param name="updateGroupCommand">Update command</param>
    /// <returns>The updated Group.</returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromBody] UpdateGroupCommand updateGroupCommand)
    {
        return Ok(await _mediator.Send(updateGroupCommand));
    }

    /// <summary>
    ///     Deletes a Group.
    /// </summary>
    /// <param name="id">Group Guid</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<GroupDTO>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all Groups.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All Groups</returns>
    [HttpPost]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GenericGetAllQuery<Group>()));
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupByName(string name)
    {
        return Ok(await _mediator.Send(new GroupQuery(name)));
    }
}
