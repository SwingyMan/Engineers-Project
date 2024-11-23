
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateChatUserCommandHandler : IRequestHandler<UpdateChatUserCommand, ChatUser>
{
    private readonly IChatUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateChatUserCommandHandler(IChatUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ChatUser> Handle(UpdateChatUserCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<ChatUser>(request.entity);
        return await _repository.Update(mapped);
    }
}
