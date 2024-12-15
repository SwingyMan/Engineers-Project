using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Queries;

public class GetChatsByUserIdQueryHandler : IRequestHandler<GetChatsByUserIdQuery, IEnumerable<Chat>>
{
    private readonly IChatRepository _chatRepository;

    public GetChatsByUserIdQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public Task<IEnumerable<Chat>> Handle(GetChatsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return _chatRepository.GetChatsByUserId(request.userGuid);
    }
}
