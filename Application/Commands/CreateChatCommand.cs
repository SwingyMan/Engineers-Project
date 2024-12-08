using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class CreateChatCommand : IRequest<Chat>
{
    public User[] Users { get; set; } = [];
}
