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
        var group = await _mediator.Send(new GenericGetByIdQuery<Group>(id));
        if (group == null) return NotFound();
        return Ok(group);
    }

    /// <summary>
    ///     Creates a Group.
    /// </summary>
    /// <param name="genericAddCommand">GroupDTO</param>
    /// <returns>The created Group.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GroupDTO group)
    {
        try
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(userId);
            return Ok(await _mediator.Send(
                new AddGroupCommand(guid,group)));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    /// <summary>
    ///     Updates a Group.
    /// </summary>
    /// <param name="updateGroupCommand">Update command</param>
    /// <returns>The updated Group.</returns>
    [HttpPatch]
    public async Task<IActionResult> Patch([FromBody] GroupUpdateDTO group)
    {

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(userId);
            if (userId == null)
            {
                return Unauthorized();
            }

        
        return Ok(await _mediator.Send(new UpdateGroupCommand(group.GroupName,group.GroupDescription,group.GroupID,guid)));
    }

    /// <summary>
    ///     Deletes a Group.
    /// </summary>
    /// <param name="id">Group Guid</param>
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid groupId)
    {
        try
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(userId);
            await _mediator.Send(
                new RemoveGroupCommand(groupId, guid));
            return Ok();
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
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

    [HttpGet]
    public async Task<IActionResult> RequestToGroup(Guid groupId)
    {
        try
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(userId);
            return Ok(await _mediator.Send(
                new RequestToGroupCommand(groupId, guid)));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    [HttpGet]
    public async Task<IActionResult> AcceptToGroup(Guid groupId, Guid userId)
    {
        try
        {
            var callerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(callerId);
            return Ok(await _mediator.Send(
                new AcceptToGroupCommand(guid,groupId,userId)));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFromGroup(Guid groupId, Guid userId)
    {
        try
        {
            var callerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(callerId);
            await _mediator.Send(
                new RemoveFromGroupCommand(guid, groupId, userId));
            return Ok();
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupMembership()
    {
        try
        {
            var callerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(callerId);
            var group = await _mediator.Send(
                new GroupsUserQuery(guid));
            return Ok(group.Select(g => new 
            {
                g.Id,
                g.Name,
                g.Description

            }));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddImage([FromForm] AddGroupImageCommand addGroupImageCommand)
    {
        var callerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
        var guid = Guid.Parse(callerId);
        addGroupImageCommand.UserId = guid;
        return Ok(await _mediator.Send(addGroupImageCommand));
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveImageFromGroup(Guid groupId)
    {
        var callerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
        var guid = Guid.Parse(callerId);
        await _mediator.Send(new RemoveGroupImageCommand(guid,groupId));
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupImageById(Guid groupId)
    {
        
        return await _mediator.Send(new GroupImageQuery(groupId));
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupRequests(Guid groupId)
    {
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        var userId = Guid.Parse(userIdClaim.Value);
        return Ok(await _mediator.Send(new GroupRequestQuery(userId, groupId)));
    }

    [HttpGet]
    public async Task<IActionResult> GetRequestsToGroup()
    {
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        var userId = Guid.Parse(userIdClaim.Value);
        var groups = await _mediator.Send(new RequestedGroupsQuery(userId));
        return Ok(groups.Select(g => new 
        {
            g.Id,
            g.Name,
            g.Description

        }));
    }
}
