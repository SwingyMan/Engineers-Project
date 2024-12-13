

using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateUserCommand : IRequest<User>
{

    [JsonIgnore]
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
