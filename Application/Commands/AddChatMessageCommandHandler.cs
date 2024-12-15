using Application.Services;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class AddChatMessageCommandHandler : IRequestHandler<AddChatMessageCommand, Message>
{
    private readonly IGenericRepository<Message> _messageRepository;
    private IUserAccessor _userAccessor;

    public AddChatMessageCommandHandler(IGenericRepository<Message> messageRepository, IUserAccessor userAccessor)
    {
        _messageRepository = messageRepository;
        _userAccessor = userAccessor;
    }

    public async Task<Message> Handle(AddChatMessageCommand request, CancellationToken cancellationToken)
    {
        Guid senderGuid = Guid.Parse(_userAccessor.User.FindFirst("id")?.Value!);

        Message message = new Message()
        {
            ChatId = request.ChatId,
            UserId = senderGuid,
            CreationDate = DateTime.UtcNow,
            Content = request.Content,
        };

        return await _messageRepository.Add(message);
    }
}
