using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{

    private readonly IPostsRepository _postsRepository;

    public PostsController(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }

    /// <summary>
    /// Retrieves a post by its Guid.
    /// </summary>
    /// <param name="id">Post Guid</param>
    /// <returns>The retrieved post, if found.</returns>
    /// <response code="200">Returns the post if found.</response>
    /// <response code="404">If the post is not found.</response>
    // GET api/posts/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        Post post = await _postsRepository.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        // TODO convert to DTO
        return Ok();
    }

    /// <summary>
    /// Creates a post.
    /// </summary>
    /// <param name="post">Post DTO</param>
    /// <returns>The updated post.</returns>
    // POST api/posts/5
    [HttpPost]
    public void Post([FromBody] string post)
    {
    }

    /// <summary>
    /// Updates a post.
    /// </summary>
    /// <param name="id">Post Guid</param>
    /// <param name="post">Updated post DTO</param>
    /// <returns>The updated post.</returns>
    // PUT api/posts/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string post)
    {
    }

    /// <summary>
    /// Deletes a post.
    /// </summary>
    /// <param name="id">Post Guid</param>
    /// <response code="200">If the post was found.</response>
    /// <response code="404">If the post was not found.</response>
    // DELETE api/posts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        Post post = await _postsRepository.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        await _postsRepository.DeletePostAsync(id);
        return Ok();
    }
}
