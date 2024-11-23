using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateGroupPostCommandHandler : IRequestHandler<UpdateGroupPostCommand, GroupPost>
{
    private readonly IGenericRepository<GroupPost> _genericRepository;
    private readonly IMapper _mapper;

    public UpdateGroupPostCommandHandler(IGenericRepository<GroupPost> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<GroupPost> Handle(UpdateGroupPostCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<GroupPost>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}
