using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateChatMessageCommandHandler : IRequestHandler<UpdateChatMessageCommand, ChatMessage>
{
    private readonly IGenericRepository<ChatMessage> _genericRepository;
    private readonly IMapper _mapper;

    public async Task<ChatMessage> Handle(UpdateChatMessageCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<ChatMessage>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}
