using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddChatMessageCommand : IRequest<Message>
{
    public Guid ChatId { get; set; }
    public string Content { get; set; }
}
