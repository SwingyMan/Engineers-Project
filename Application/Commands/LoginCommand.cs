using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class LoginCommand : IRequest<JwtToken>
{
    public LoginCommand(UserLoginDTO userLoginDto)
    {
        UserLoginDto = userLoginDto;
    }
    public UserLoginDTO UserLoginDto { get; set; }
}