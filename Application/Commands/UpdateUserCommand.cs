

using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateUserCommand : IRequest<User>
{

    public UserDTO entity { get; }
    public Guid id { get; }

    public UpdateUserCommand(UserDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
