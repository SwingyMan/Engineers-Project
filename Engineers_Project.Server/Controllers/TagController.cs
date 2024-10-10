using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves a tag by its Guid.
    /// </summary>
    /// <param name="id">Tag Guid</param>
    /// <returns>The retrieved tag, if found.</returns>
    /// <response code="200">Returns the tag if found.</response>
    /// <response code="404">If tag is not found.</response>
    // GET api/tags/get/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var tag = await _mediator.Send(new GenericGetByIdQuery<Tag>(id));
        if (tag == null) return NotFound();
        return Ok(tag);
    }

    /// <summary>
    ///     Creates a tag.
    /// </summary>
    /// <param name="genericAddCommand">Add tag command</param>
    /// <returns>The updated tag.</returns> 
    /// <response code="200">Returns the tag if found.</response>
    /// <response code="401">If the user is not authorized.</response>
    /// <response code="403">If the user does not have admin role.</response>
    // POST api/tags/post
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<TagDTO, Tag> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a tag.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated tag.</returns>
    // PUT api/tags/put
    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<TagDTO, Tag> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a tag.
    /// </summary>
    /// <param name="id">Tag Guid</param>
    /// <response code="200">If the tag was found.</response>
    /// <response code="404">If the tag was not found.</response>
    // DELETE api/tags/delete/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<Tag>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all tags.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All tags</returns>
    // POST api/tags/getall
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<Tag> query)
    {
        return Ok(await _mediator.Send(query));
    }
}
