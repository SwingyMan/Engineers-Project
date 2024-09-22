using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a post by its Guid.
    /// </summary>
    /// <param name="id">Post Guid</param>
    /// <returns>The retrieved post, if found.</returns>
    /// <response code="200">Returns the post if found.</response>
    /// <response code="404">If the post is not found.</response>
    // GET api/post/get/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var post = await _mediator.Send(new GenericGetByIdQuery<Post>(id));
        if (post == null) return NotFound();
        return Ok(post);
    }

    /// <summary>
    ///     Creates a post.
    /// </summary>
    /// <param name="genericAddCommand">Post DTO</param>
    /// <returns>The updated post.</returns>
    // POST api/post/post
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<PostDTO, Post> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a post.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated post.</returns>
    // PUT api/post/put
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<PostDTO, Post> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a post.
    /// </summary>
    /// <param name="id">Post Guid</param>
    /// <response code="200">If the post was found.</response>
    /// <response code="404">If the post was not found.</response>
    // DELETE api/post/delete/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<Post>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all posts.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All tags</returns>
    // POST api/post/getall
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<Post> query)
    {
        return Ok(await _mediator.Send(query));
    }
}