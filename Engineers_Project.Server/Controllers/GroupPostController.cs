using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class GroupPostController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupPostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a GroupPost by its Guid.
    /// </summary>
    /// <param name="id">GroupPost Guid</param>
    /// <returns>The retrieved GroupPost, if found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var groupPost = await _mediator.Send(new GenericGetByIdQuery<GroupPostDTO>(id));
        if (groupPost == null) return NotFound();
        return Ok(groupPost);
    }

    /// <summary>
    ///     Creates a GroupPost.
    /// </summary>
    /// <param name="genericAddCommand">GroupPostDTO</param>
    /// <returns>The created GroupPost.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<GroupPostDTO, GroupPost> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a GroupPost.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated GroupPost.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<GroupPostDTO, GroupPost> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a GroupPost.
    /// </summary>
    /// <param name="id">GroupPost Guid</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<GroupPostDTO>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all GroupPosts.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All GroupPosts</returns>
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<GroupPostDTO> query)
    {
        return Ok(await _mediator.Send(query));
    }
}
