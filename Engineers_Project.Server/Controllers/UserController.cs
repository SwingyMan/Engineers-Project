using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Engineers_Project.Server.Controllers;

public class UserController : GenericController<User,UserDTO>
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }
}