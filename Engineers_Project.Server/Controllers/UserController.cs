using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]

public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO userDto)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress;
        var host = HttpContext.Request.Host.Host;
        var token = await _mediator.Send(new RegisterCommand(userDto, ipAddress, host));
        if (token is null)
            return NotFound();
        return Ok(token);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
    {
        var token = await _mediator.Send(new LoginCommand(userLoginDto));
        if (token is null)
            return NotFound();
        return Ok(token);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<User> getQuery)
    {
        return Ok(await _mediator.Send(getQuery));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetById(Guid guid)
    {
        return Ok(await _mediator.Send(new GenericGetByIdQuery<User>(guid)));
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateByID([FromBody] UpdateUserCommand updateUserCommand)
    {
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        var userId = Guid.Parse(userIdClaim.Value);
        updateUserCommand.UserId = userId;

        return Ok(await _mediator.Send(updateUserCommand));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteByID(Guid guid)
    {
        await _mediator.Send(new GenericDeleteCommand<User>(guid));
        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ActivateAccount([FromQuery] Guid token)
    {
        var result = await _mediator.Send(new ActivateAccountCommand(token));
        if (result)
        {
            return Ok("Account activated successfully.");
        }
        else
        {
            return BadRequest("Invalid or expired activation link.");
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAvatar(string FileName)
    {
        var avatar = await _mediator.Send(new AvatarQuery(FileName));
        if (avatar is null)
        {
            return NotFound();
        }
        return avatar;
    }
    [HttpPost]
    [RequestSizeLimit(10_000_000_000)]

    public async Task<IActionResult> AddAvatar( IFormFile file)
    {
        var callerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
        var guid = Guid.Parse(callerId);
        return Ok(await _mediator.Send(new AddAvatarCommand(guid, file)));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteAvatar()
    {
        var callerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value.ToString();
        var guid = Guid.Parse(callerId);
        await _mediator.Send(new RemoveAvatarCommand(guid));
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var token = await _mediator.Send(new RefreshTokenCommand(refreshToken));
        if (token is null)
            return NotFound();
        return Ok(new 
        {
            token =token,
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetUserByName(string userName)
    {
        return Ok(await _mediator.Send(new UserQuery(userName)));
    }

    [HttpGet]
    public async Task<IActionResult> GetFriendsRequests()
    {
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        var userId = Guid.Parse(userIdClaim.Value);

        return Ok(await _mediator.Send(new FriendQuery(userId)));
    }
}