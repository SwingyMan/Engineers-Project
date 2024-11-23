
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateGroupUserCommandHandler : IRequestHandler<UpdateGroupUserCommand, GroupUser>
{
    private readonly IGroupUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateGroupUserCommandHandler(IGroupUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GroupUser> Handle(UpdateGroupUserCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<GroupUser>(request.entity);
        return await _repository.Update(mapped);
    }
}
