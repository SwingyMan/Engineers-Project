using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class PostsTagController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsTagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a postsTag by its Guid.
    /// </summary>
    /// <param name="id">Post Guid</param>
    /// <returns>The retrieved postsTag, if found.</returns>
    /// <response code="200">Returns the postsTag if found.</response>
    /// <response code="404">If the postsTag is not found.</response>
    // GET api/postsTag/get/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var postsTag = await _mediator.Send(new GenericGetByIdQuery<Post>(id));
        if (postsTag == null) return NotFound();
        return Ok(postsTag);
    }

    /// <summary>
    ///     Creates a postsTag.
    /// </summary>
    /// <param name="genericAddCommand">Post DTO</param>
    /// <returns>The updated postsTag.</returns>
    // POST api/postsTag/post
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<PostsTagDTO, PostsTag> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a postsTag.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated postsTag.</returns>
    // PUT api/postsTag/put
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<PostsTagDTO, PostsTag> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a postsTag.
    /// </summary>
    /// <param name="id">PostsTag Guid</param>
    /// <response code="200">If the postsTag was found.</response>
    /// <response code="404">If the postsTag was not found.</response>
    // DELETE api/postsTag/delete/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<PostsTag>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all postsTags.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All tags</returns>
    // POST api/postsTag/getall
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<PostsTag> query)
    {
        return Ok(await _mediator.Send(query));
    }
}