using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateGroupCommand : IRequest<Group>
{
    public GroupDTO entity { get; }
    public Guid id { get; }

    public UpdateGroupCommand(GroupDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
