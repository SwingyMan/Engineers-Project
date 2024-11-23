using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateChatUserCommand : IRequest<ChatUser>
{
    public ChatUserDTO entity { get; }
    public Guid id { get; }

    public UpdateChatUserCommand(ChatUserDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
