using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a Chat by its Guid.
    /// </summary>
    /// <param name="id">Chat Guid</param>
    /// <returns>The retrieved Chat, if found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var chat = await _mediator.Send(new GenericGetByIdQuery<ChatDTO>(id));
        if (chat == null) return NotFound();
        return Ok(chat);
    }

    /// <summary>
    ///     Creates a Chat.
    /// </summary>
    /// <param name="genericAddCommand">ChatDTO</param>
    /// <returns>The created Chat.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<ChatDTO, Chat> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a Chat.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated Chat.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<ChatDTO, Chat> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a Chat.
    /// </summary>
    /// <param name="id">Chat Guid</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<ChatDTO>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all Chats.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All Chats</returns>
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<ChatDTO> query)
    {
        return Ok(await _mediator.Send(query));
    }
}
