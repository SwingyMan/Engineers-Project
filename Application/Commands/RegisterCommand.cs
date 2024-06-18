using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class RegisterCommand : IRequest<JwtToken>
{
    public RegisterCommand(UserRegisterDTO userRegisterDto)
    {
        UserRegisterDto = userRegisterDto;
    }
    public UserRegisterDTO UserRegisterDto { get; set; }
}