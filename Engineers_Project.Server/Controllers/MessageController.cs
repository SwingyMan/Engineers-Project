using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a message by its Guid.
    /// </summary>
    /// <param name="id">Message Guid</param>
    /// <returns>The retrieved message, if found.</returns>
    /// <response code="200">Returns the message if found.</response>
    /// <response code="404">If the message is not found.</response>
    // GET api/message/get/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var message = await _mediator.Send(new GenericGetByIdQuery<Message>(id));
        if (message == null) return NotFound();
        return Ok(message);
    }

    /// <summary>
    ///     Creates a message.
    /// </summary>
    /// <param name="genericAddCommand">Message DTO</param>
    /// <returns>The updated message.</returns>
    // POST api/message/post
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<MessageDTO, Message> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a message.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated message.</returns>
    // PUT api/message/put
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<MessageDTO, Message> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a message.
    /// </summary>
    /// <param name="id">Post Guid</param>
    /// <response code="200">If the message was found.</response>
    /// <response code="404">If the message was not found.</response>
    // DELETE api/message/delete/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<Message>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all messages.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All tags</returns>
    // POST api/message/getall
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<Message> query)
    {
        return Ok(await _mediator.Send(query));
    }
}