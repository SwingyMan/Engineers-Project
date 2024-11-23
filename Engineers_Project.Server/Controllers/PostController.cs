using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpGet]
    public async Task<IActionResult> GetAvailablePosts()
    {
        return Ok(await _mediator.Send(
            new PostQuery(Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString()))));
    }
    /// <summary>
    ///     Creates a post.
    /// </summary>
    /// <param name="addPostCommand">Post DTO</param>
    /// <returns>The updated post.</returns>
    // POST api/post/post
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddPostCommand addPostCommand)
    {
        var userIdClaim = User.FindFirst("id");
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(userIdClaim.Value);

        // Create a new command with the user ID
        var commandWithUserId = new AddPostCommand(addPostCommand.entity, userId);
        return Ok(await _mediator.Send(commandWithUserId));
    }

    /// <summary>
    ///     Updates a post.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated post.</returns>
    // PUT api/post/put
    [HttpPatch]
    //[Authorize(Roles = "USER")] 
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
    /// <returns>All tags</returns>
    // GET api/post/getall
    [HttpGet]
    [Authorize(Roles="ADMIN")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GenericGetAllQuery<Post>()));
    }
    [HttpGet]
    public async Task<IActionResult> GetComments(Guid postId)
    {
        return Ok(await _mediator.Send(new PostCommentQuery(postId)));
    }
}