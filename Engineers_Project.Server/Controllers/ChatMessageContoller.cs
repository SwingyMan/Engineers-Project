using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Engineers_Project.Server.Controllers;

[Authorize(Policy = "ChatMessageMemberOrAdmin")]
[Route("api/v1/[controller]/[action]")]
[ApiController]
public class ChatMessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a ChatMessage by its Guid.
    /// </summary>
    /// <param name="id">ChatMessage Guid</param>
    /// <returns>The retrieved ChatMessage, if found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var chatMessage = await _mediator.Send(new GenericGetByIdQuery<ChatMessageDTO>(id));
        if (chatMessage == null) return NotFound();
        return Ok(chatMessage);
    }

    /// <summary>
    ///     Creates a ChatMessage.
    /// </summary>
    /// <param name="genericAddCommand">ChatMessageDTO</param>
    /// <returns>The created ChatMessage.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<ChatMessageDTO, ChatMessage> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a ChatMessage.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated ChatMessage.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<ChatMessageDTO, ChatMessage> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a ChatMessage.
    /// </summary>
    /// <param name="id">ChatMessage Guid</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<ChatMessageDTO>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all ChatMessages.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All ChatMessages</returns>
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<ChatMessageDTO> query)
    {
        return Ok(await _mediator.Send(query));
    }
}
