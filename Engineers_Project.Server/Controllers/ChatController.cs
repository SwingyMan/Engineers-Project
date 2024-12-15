using Application.Commands;
using Application.DTOs;
using Application.Queries;
using AutoMapper;
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

//[Authorize(Policy = "ChatMemberOrAdmin")]
[Authorize]
[Route("api/v1/[controller]/[action]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IChatRepository _chatRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IMapper _mapper;


    public ChatController(IMediator mediator, IChatRepository chatRepository, IGenericRepository<User> userRepository, IHubContext<ChatHub> hubContext, IMapper mapper)
    {
        _mediator = mediator;
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _hubContext = hubContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a Chat by its Guid.
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

    /// <summary>
    /// Retrieves chat between current user and user with given ID. 
    /// If chat between those two users does not exist, it is created and then returned.
    /// </summary>
    /// <param name="getOrCreateChatDTO">DTO containing recipieng ID</param>
    /// <returns>Chat DTO of the chat room between two users.</returns>
    [HttpPost]
    public async Task<IActionResult> GetOrCreateChat([FromBody] GetOrCreateChatDTO getOrCreateChatDTO)
    {
        Guid userGuid = Guid.Parse(User.FindFirstValue("id")!);
        Guid[] userIds = [userGuid, getOrCreateChatDTO.RecipientGuid];

        Chat? chat = await _chatRepository.GetChatByUserIds(userIds);

        if (chat == null)
        {
            User sender = await _userRepository.GetByID(userGuid);
            User recipient = await _userRepository.GetByID(getOrCreateChatDTO.RecipientGuid);

            var command = new CreateChatCommand() { Users = [sender, recipient] };
            chat = await _mediator.Send(command);
        }
        return Ok(_mapper.Map<ChatResponseObject>(chat));
    }

    /// <summary>
    /// Sends private chat message via SignalR hub
    /// </summary>
    /// <param name="message">MessageDTO of the message intended to be sent</param>
    /// <returns>Sent message response DTO</returns>
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] AddChatMessageCommand request)
    {
        Message newMessage = await _mediator.Send(request);

        GenericGetByIdQuery<Chat> query = new GenericGetByIdQuery<Chat>(request.ChatId);
        Chat chat = await _mediator.Send(query);

        string recipientId = chat.Users.First(user => user.Id != newMessage.UserId).Id.ToString();

        var messageResponseObject = _mapper.Map<ChatMessageResponseObject>(newMessage);
        await _hubContext.Clients.User(recipientId).SendAsync("ReceiveMessage", messageResponseObject);
        return Ok(messageResponseObject);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroupChat([FromBody] CreateGroupChatDTO createGroupChatDTO)
    {


    }
}
