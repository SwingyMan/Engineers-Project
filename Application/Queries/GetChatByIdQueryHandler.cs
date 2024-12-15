using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Queries;

public class GetChatByIdQueryHandler : IRequestHandler<GenericGetByIdQuery<Chat>, Chat>
{
    private readonly IChatRepository _chatRepository;
    public GetChatByIdQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    public async Task<Chat> Handle(GenericGetByIdQuery<Chat> request, CancellationToken cancellationToken)
    {
        return await _chatRepository.GetChatById(request.id);
    }
}
