using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class ChatUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatUserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a ChatUser by its Guid.
    /// </summary>
    /// <param name="id">ChatUser Guid</param>
    /// <returns>The retrieved ChatUser, if found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var chatUser = await _mediator.Send(new GenericGetByIdQuery<ChatUserDTO>(id));
        if (chatUser == null) return NotFound();
        return Ok(chatUser);
    }

    /// <summary>
    ///     Creates a ChatUser.
    /// </summary>
    /// <param name="genericAddCommand">ChatUserDTO</param>
    /// <returns>The created ChatUser.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<ChatUserDTO, ChatUser> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a ChatUser.
    /// </summary>
    /// <param name="updateChatUserCommand">Update command</param>
    /// <returns>The updated ChatUser.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] UpdateChatUserCommand updateChatUserCommand)
    {
        return Ok(await _mediator.Send(updateChatUserCommand));
    }

    /// <summary>
    ///     Deletes a ChatUser.
    /// </summary>
    /// <param name="id">ChatUser Guid</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<ChatUserDTO>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all ChatUsers.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All ChatUsers</returns>
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<ChatUser> query)
    {
        return Ok(await _mediator.Send(query));
    }
}
