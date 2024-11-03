using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddPostCommand : IRequest<Post>
{
    public PostDTO entity { get; }
    public Guid guid { get; }

    public AddPostCommand(PostDTO entity, Guid guid)
    {
        this.entity = entity;
        this.guid = guid;
    }
}
