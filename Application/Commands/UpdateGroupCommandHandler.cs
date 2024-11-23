using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Group>
{
    private readonly IGenericRepository<Group> _genericRepository;
    private readonly IMapper _mapper;

    public UpdateGroupCommandHandler(IGenericRepository<Group> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<Group> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Group>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}
