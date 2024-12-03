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

    //public override async Task OnConnectedAsync()
    //{
    //    Console.WriteLine($"{Context.ConnectionId} has connected"); 
    //    //await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    //    Console.WriteLine($"Sent join message for {Context.ConnectionId}");
    //}

    //public async Task SendMessage(string RecipientID, string message)
    //{
    //    string senderId = Context.User.FindFirst("id")?.Value!;

    //    ChatHubMessageDTO chatHubMessageDTO = new ChatHubMessageDTO()
    //    {
    //        Message = message,
    //        ChatId = chatId,
    //        SenderId = senderId,
    //        Timestamp = DateTime.UtcNow
    //    };

    //    Message messageEntity = new Message()
    //    {
    //        Content = message,
    //        CreationDate = DateTime.UtcNow,
    //        UserId = Guid.Parse(senderId)
    //    };

    //    await _messageRepository.Add(messageEntity);

    //    _mediator.Send(new GetChatQuery());

        

    //    await Clients.User(recipentGuid).ReceiveMessage(chatHubMessageDTO);
    //}

}