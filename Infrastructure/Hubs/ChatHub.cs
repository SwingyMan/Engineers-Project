using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.SignalR;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs;

[Authorize]
public sealed class ChatHub : Hub<IChatClient>
{
    private readonly IGenericRepository<Message> _messageRepository;
    private readonly IMediator _mediator;

    public ChatHub(IGenericRepository<Message> messageRepository, IMediator mediator)
    {
        _messageRepository = messageRepository;
        _mediator = mediator;
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public async Task JoinChat(Guid chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}