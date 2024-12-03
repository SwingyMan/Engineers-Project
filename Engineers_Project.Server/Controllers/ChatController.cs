using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using Infrastructure.Hubs;
using Infrastructure.IRepositories;
using Infrastructure.SignalR;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Engineers_Project.Server.Controllers;

[Authorize(Policy = "ChatMemberOrAdmin")]
[Route("api/v1/[controller]/[action]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IChatRepository _chatRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IHubContext<ChatHub> _hubContext;


    public ChatController(IMediator mediator, IChatRepository chatRepository, IGenericRepository<User> userRepository, IHubContext<ChatHub> hubContext)
    {
        _mediator = mediator;
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _hubContext = hubContext;
    }

    /// <summary>
    ///     Retrieves a Chat by its Guid.
    /// </summary>
    /// <param name="id">Chat Guid</param>
    /// <returns>The retrieved Chat, if found.</returns>
    [HttpGet("{id}")]

    public async Task<IActionResult> Get(Guid id)
    {
        var chat = await _mediator.Send(new GenericGetByIdQuery<ChatDTO>(id));
        if (chat == null) return NotFound();
        return Ok(chat);
    }

    public async Task<IActionResult> GetOrCreateChat([FromBody] string recipientId)
    {
        string userId = User.FindFirstValue("id");

        Guid userGuid = Guid.Parse(userId);
        Guid recipientGuid = Guid.Parse(recipientId);

        Guid[] userIds = [userGuid, recipientGuid];
        Chat? chat = await _chatRepository.GetChatByUserIds(userIds);

        if(chat == null)
        {
            User user = await _mediator.Send(new GenericGetByIdQuery<User>(userGuid));
            User user2 = await _mediator.Send(new GenericGetByIdQuery<User>(recipientGuid));
            //User user = await _userRepository.GetByID(userGuid);
            //User user2 = await _userRepository.GetByID(recipientGuid);

            chat = new Chat()
            {
                Users = [user, user2],
                Name = ""
            };

            await _chatRepository.AddChatAsync(chat);
        }

        return Ok(chat);
    }

    public async Task SendMessage([FromBody] MessageDTO message)
    {
        string senderId = User.FindFirst("id")?.Value!;
        Guid senderGuid = Guid.Parse(senderId);

        ChatHubMessageDTO chatHubMessageDTO = new ChatHubMessageDTO()
        {
            Message = message.Content,
            ChatId = message.ChatId.ToString(),
            SenderId = senderId,
            Timestamp = DateTime.UtcNow
        };

        // Save message to the db

        await _mediator.Send(new GenericAddCommand<MessageDTO, Message>(message));

        // Send message via SignalR
        Chat chat = await _mediator.Send(new GenericGetByIdQuery<Chat>(message.ChatId));

        string recipientId = chat.Users.First(user => user.Id != senderGuid).Id.ToString();

        await _hubContext.Clients.User(recipientId).SendAsync("ReceiveMessage", message.Content);


    }

    /// <summary>
    ///     Creates a Chat.
    /// </summary>
    /// <param name="genericAddCommand">ChatDTO</param>
    /// <returns>The created Chat.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<ChatDTO, Chat> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a Chat.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated Chat.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<ChatDTO, Chat> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a Chat.
    /// </summary>
    /// <param name="id">Chat Guid</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<ChatDTO>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all Chats.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All Chats</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<ChatDTO> query)
    {
        return Ok(await _mediator.Send(query));
    }
}
