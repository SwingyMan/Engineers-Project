
using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateMessageCommand : IRequest<Message>
{
    public MessageDTO entity { get; }
    public Guid id { get; }

    public UpdateMessageCommand(MessageDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
