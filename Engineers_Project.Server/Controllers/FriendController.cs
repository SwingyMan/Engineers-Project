using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Microsoft.AspNetCore.Components.Route("api/v1/[controller]/[action]")]
public class FriendController : Controller
{
    private readonly IMediator _mediator;

    public FriendController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetFriends()
    {
        return Ok(await _mediator.Send(new FriendQuery(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").ToString()))));
    }
    [HttpPost]
    public async Task<IActionResult> AddFriend([FromBody]Guid friendId)
    {
        return Ok(await _mediator.Send(new AddFriendsCommand(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").ToString()), friendId)));
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveFriend([FromBody]Guid friendId)
    {
        await _mediator.Send(
            new RemoveFriendsCommand(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").ToString()),
                friendId));
        return Ok();

    }

    [HttpPost]
    public async Task<IActionResult> AcceptFriend([FromBody] Guid friendId)
    {
        return Ok(await _mediator.Send(new AcceptFriendCommand(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").ToString()), friendId)));
    }
}