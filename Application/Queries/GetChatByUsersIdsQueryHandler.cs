using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Queries
{
    public class GetChatByUsersIdsQueryHandler : IRequestHandler<GetChatByUsersIdsQuery, Chat>
    {
        private readonly IChatRepository _chatRepository;

        public GetChatByUsersIdsQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Chat> Handle(GetChatByUsersIdsQuery request, CancellationToken cancellationToken)
        {
            return await _chatRepository.GetChatByUserIds(request.userIds);
        }
    }
}
