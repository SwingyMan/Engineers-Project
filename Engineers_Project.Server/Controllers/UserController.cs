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
        var token = await _mediator.Send(new RegisterCommand(userDto, ipAddress,host));
        if (token is null)
            return NotFound();
        return Ok(token);
    }

    [HttpPost]
    [AllowAnonymous]
    [DisableCors]
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
    public async Task<IActionResult> UpdateByID([FromBody] GenericUpdateCommand<UserDTO, User> genericUpdateCommand)
    {
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        var userId = Guid.Parse(userIdClaim.Value);
        if (userId != genericUpdateCommand.id)
        {
            return Forbid("Users can only update their own information.");
        }

        return Ok(await _mediator.Send(genericUpdateCommand));
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
}