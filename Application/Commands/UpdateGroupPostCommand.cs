using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateGroupPostCommand : IRequest<GroupPost>
{
    public GroupPostDTO entity { get; }
    public Guid id { get; }

    public UpdateGroupPostCommand(GroupPostDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
