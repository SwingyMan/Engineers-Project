using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class GroupUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupUserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a groupUser by its Guid.
    /// </summary>
    /// <param name="id">groupUser Guid</param>
    /// <returns>The retrieved groupUser, if found.</returns>
    /// <response code="200">Returns the groupUser if found.</response>
    /// <response code="404">If the GrougroupUserpUser is not found.</response>
    // GET api/groupUser/get/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var groupUser = await _mediator.Send(new GenericGetByIdQuery<GroupUser>(id));
        if (groupUser == null) return NotFound();
        return Ok(groupUser);
    }

    /// <summary>
    ///     Creates a groupUser.
    /// </summary>
    /// <param name="genericAddCommand">GroupUser DTO</param>
    /// <returns>The updated groupUser.</returns>
    // POST api/groupUser/post
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<GroupUserDTO, GroupUser> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a groupUser.
    /// </summary>
    /// <param name="updateGroupUserCommand">Update command</param>
    /// <returns>The updated groupUser.</returns>
    // PUT api/groupUser/put
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromBody] UpdateGroupUserCommand updateGroupUserCommand)
    {
        return Ok(await _mediator.Send(updateGroupUserCommand));
    }

    /// <summary>
    ///     Deletes a groupUser.
    /// </summary>
    /// <param name="id">GroupUser Guid</param>
    /// <response code="200">If the groupUser was found.</response>
    /// <response code="404">If the groupUser was not found.</response>
    // DELETE api/groupUser/delete/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        // TODO : Check if user is a member of the group and is the creator of the post
        await _mediator.Send(new GenericDeleteCommand<GroupUser>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all groupUsers.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All groupUsers</returns>
    // POST api/groupUser/getall
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GenericGetAllQuery<GroupUser>()));
    }
}