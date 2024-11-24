using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateChatMessageCommandHandler : IRequestHandler<UpdateChatMessageCommand, ChatMessage>
{
    private readonly IChatMessageRepository _repository;
    private readonly IMapper _mapper;

    public UpdateChatMessageCommandHandler(IChatMessageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ChatMessage> Handle(UpdateChatMessageCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<ChatMessage>(request.entity);
        return await _repository.Update(mapped);
    }
}
