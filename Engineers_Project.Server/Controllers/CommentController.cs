using Application.Commands;
using Application.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;
[ApiController]
[Route("api/v1/[controller]/[action]")]

public class CommentController : Controller
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpDelete]
    public async Task<ActionResult<List<CommentDTO>>> DeleteComment(Guid guid)
    {
        await _mediator.Send(new GenericDeleteCommand<Comment>(guid));
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CommentDTO commentDTO)
    {
        return Ok(await _mediator.Send(new AddCommentCommand(commentDTO,Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value.ToString()))));
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateComment([FromBody] CommentDTO commentDTO)
    {
        return Ok(await _mediator.Send(new UpdateCommentCommand(commentDTO,Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value.ToString()))));
    }
}