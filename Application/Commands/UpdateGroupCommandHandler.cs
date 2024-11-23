using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Group>
{
    private readonly IGroupRepository _repository;
    private readonly IMapper _mapper;

    public UpdateGroupCommandHandler(IGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Group> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Group>(request.entity);
        return await _repository.Update(mapped);
    }
}
