using System.Text.Json.Serialization;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class UpdateChatMessageCommand : IRequest<ChatMessage>
{
    public ChatMessageDTO entity { get; }
    public Guid id { get; }

    public UpdateChatMessageCommand(ChatMessageDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
