using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateChatCommandHandler : IRequestHandler<UpdateChatCommand, Chat>
{
    private readonly IGenericRepository<Chat> _genericRepository;
    private readonly IMapper _mapper;

    public async Task<Chat> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Chat>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}
