using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Chat>
{
    private readonly IChatRepository _chatRepository;

    public CreateChatCommandHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Chat?> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {

        Chat chat = new Chat()
        {
            Users = request.Users,
            Messages = new List<Message>(),
            Name = ""
        };

        return await _chatRepository.AddChatAsync(chat);
    }
}
