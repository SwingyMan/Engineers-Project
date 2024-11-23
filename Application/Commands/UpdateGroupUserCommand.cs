
using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateGroupUserCommand : IRequest<GroupUser>
{
    public GroupUserDTO entity { get; }
    [JsonIgnore]
    public Guid id { get; }

    public UpdateGroupUserCommand(GroupUserDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
