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
    ///     Retrieves a groupPost by its Guid.
    /// </summary>
    /// <param name="id">GroupPost Guid</param>
    /// <returns>The retrieved post, if found.</returns>
    /// <response code="200">Returns the groupPost if found.</response>
    /// <response code="404">If the groupPost is not found.</response>
    // GET api/groupPost/get/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var post = await _mediator.Send(new GenericGetByIdQuery<GroupPost>(id));
        if (post == null) return NotFound();
        return Ok(post);
    }

    /// <summary>
    ///     Creates a groupPost.
    /// </summary>
    /// <param name="genericAddCommand">GroupPost DTO</param>
    /// <returns>The updated post.</returns>
    // POST api/groupPost/post
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<GroupPostDTO, GroupPost> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a groupPost.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated groupPost.</returns>
    // PUT api/groupPost/put
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<GroupPostDTO, GroupPost> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a groupPost.
    /// </summary>
    /// <param name="id">GroupPost Guid</param>
    /// <response code="200">If the groupPost was found.</response>
    /// <response code="404">If the groupPost was not found.</response>
    // DELETE api/groupPost/delete/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<GroupPost>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all groupPosts.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All groupPosts</returns>
    // POST api/groupPost/getall
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<GroupPost> query)
    {
        return Ok(await _mediator.Send(query));
    }
}