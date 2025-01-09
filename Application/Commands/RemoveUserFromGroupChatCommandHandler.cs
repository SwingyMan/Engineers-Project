using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class RemoveUserFromGroupChatCommandHandler : IRequestHandler<RemoveUserFromGroupChatCommand, Chat?>
{
    private readonly IChatRepository _chatRepository;
    private readonly IGenericRepository<User> _userRepository;
    public RemoveUserFromGroupChatCommandHandler(IChatRepository chatRepository, IGenericRepository<User> userRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }
    public async Task<Chat?> Handle(RemoveUserFromGroupChatCommand request, CancellationToken cancellationToken)
    {
        Chat? chat = await _chatRepository.GetChatById(request.GroupChatGuid);
        if (chat == null)
        {
            return null;
        }

        if (!chat.IsGroupChat)
        {
            return null;
        }

        User? user = await _userRepository.GetByID(request.UserGuid);

        if (user == null)
        {
            return null;
        }

        chat.Users.Remove(user);
        return await _chatRepository.Update(chat.Id, chat);
    }

}
