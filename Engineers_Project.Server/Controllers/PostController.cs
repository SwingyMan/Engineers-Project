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
        try
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(userId);
            return Ok(await _mediator.Send(
                new PostQuery(guid)));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }


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
    /// <param name="updatePostCommand">Update command</param>
    /// <returns>The updated post.</returns>
    // PUT api/post/put
    [HttpPatch]
    //[Authorize(Roles = "USER")] 
    public async Task<IActionResult> Patch([FromBody] UpdatePostCommand updatePostCommand)
    {
        return Ok(await _mediator.Send(updatePostCommand));
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
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GenericGetAllQuery<Post>()));
    }
    [HttpGet]
    public async Task<IActionResult> FindPostByTitle(string title)
    {
        try
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(userId);
            return Ok(await _mediator.Send(
                new PostTitleQuery(title,guid)));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }  }

    [HttpGet]
    public async Task<IActionResult> FindPostByUser(Guid userId)
    {
        try
        {
            var ownerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(ownerId);
            return Ok(await _mediator.Send(
                new PostUserQuery(userId,guid)));
        }
        catch (Exception e)
        {
            return Unauthorized();
        } 
    }

    [HttpGet]
    public async Task<IActionResult> FindPostInGroup(Guid groupId)
    {
        try
        {
            var callerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
            var guid = Guid.Parse(callerId);
            
            return Ok(await _mediator.Send(
                new PostGroupQuery(guid,groupId)));
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }
}