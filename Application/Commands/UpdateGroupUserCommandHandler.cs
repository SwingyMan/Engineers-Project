
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateGroupUserCommandHandler : IRequestHandler<UpdateGroupUserCommand, GroupUser>
{
    private readonly IGenericRepository<GroupUser> _genericRepository;
    private readonly IMapper _mapper;

    public async Task<GroupUser> Handle(UpdateGroupUserCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<GroupUser>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}
