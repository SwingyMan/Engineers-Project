
using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdatePostCommand : IRequest<Post>
{
    public PostDTO entity { get; }
    [JsonIgnore]
    public Guid id { get; }

    public UpdatePostCommand(PostDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
