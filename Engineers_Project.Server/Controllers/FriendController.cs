using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
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
        return Ok(await _mediator.Send(new FriendQuery(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value.ToString()))));
    }
    [HttpPost]
    public async Task<IActionResult> AddFriend(Guid friendId)
    {
       var friends = await _mediator.Send(
            new AddFriendsCommand(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value.ToString()),
                friendId));
       if (friends == null) return Conflict("You already sent friend request");
        return Ok(friends);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveFriend(Guid friendId)
    {
        await _mediator.Send(
            new RemoveFriendsCommand(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value.ToString()),
                friendId));
        return Ok();

    }

    [HttpPost]
    public async Task<IActionResult> AcceptFriend( Guid friendId)
    {
        var friend =
            await _mediator.Send(new AcceptFriendCommand(
                Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value.ToString()), friendId));
        if (friend is null)
            return BadRequest();
        return Ok(friend);
    }
}