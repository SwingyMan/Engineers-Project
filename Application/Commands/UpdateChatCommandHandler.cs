using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateChatCommandHandler : IRequestHandler<UpdateChatCommand, Chat>
{
    private readonly IChatRepository _repository;
    private readonly IMapper _mapper;

    public UpdateChatCommandHandler(IChatRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Chat> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Chat>(request.entity);
        return await _repository.Update(request.id,mapped);
    }
}
