
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateChatUserCommandHandler : IRequestHandler<UpdateChatUserCommand, ChatUser>
{
    private readonly IGenericRepository<ChatUser> _genericRepository;
    private readonly IMapper _mapper;

    public async Task<ChatUser> Handle(UpdateChatUserCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<ChatUser>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}
