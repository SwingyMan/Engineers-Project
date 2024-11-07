using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Commands;

public class RegisterCommand : IRequest<JwtToken>
{
    public RegisterCommand(UserRegisterDTO userRegisterDto, IPAddress ipAddress,string host)
    {
        UserRegisterDto = userRegisterDto;
        IpAddress = ipAddress;
        Host = host;
    }

    public UserRegisterDTO UserRegisterDto { get; set; }
    public IPAddress IpAddress { get; set; }
    public string Host { get; set; }
}