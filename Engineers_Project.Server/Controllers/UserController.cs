using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        var token = await _mediator.Send(new RegisterCommand(userDto));
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
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<User> getQuery)
    {
        return Ok(await _mediator.Send(getQuery));
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid guid)
    {
        return Ok(await _mediator.Send(new GenericGetByIdQuery<User>(guid)));
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateByID([FromBody] GenericUpdateCommand<UserRegisterDTO, User> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteByID(Guid guid)
    {
        await _mediator.Send(new GenericDeleteCommand<User>(guid));
        return Ok();
    }
}