using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateGroupPostCommandHandler : IRequestHandler<UpdateGroupPostCommand, GroupPost>
{
    private readonly IGroupPostRepository _repository;
    private readonly IMapper _mapper;

    public UpdateGroupPostCommandHandler(IGroupPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GroupPost> Handle(UpdateGroupPostCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<GroupPost>(request.entity);
        return await _repository.Update(mapped);
    }
}
