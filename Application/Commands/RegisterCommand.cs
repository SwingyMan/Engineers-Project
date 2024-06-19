using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class RegisterCommand : IRequest<JwtToken>
{
    public RegisterCommand(UserDTO userRegisterDto)
    {
        UserRegisterDto = userRegisterDto;
    }

    public UserDTO UserRegisterDto { get; set; }
}