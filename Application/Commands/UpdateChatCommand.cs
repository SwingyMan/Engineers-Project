using System.Text.Json.Serialization;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class UpdateChatCommand : IRequest<Chat>
{
    public ChatDTO entity { get; }
    public Guid id { get; }

    public UpdateChatCommand(ChatDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
