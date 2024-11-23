

using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Role>
{
    private readonly IGenericRepository<Role> _genericRepository;
    private readonly IMapper _mapper;

    public async Task<Role> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Role>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}