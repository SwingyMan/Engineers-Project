

using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Role>
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(IRoleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Role> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Role>(request.entity);
        return await _repository.Update(mapped);
    }
}