
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Post>
{
    private readonly IGenericRepository<Post> _genericRepository;
    private readonly IMapper _mapper;

    public UpdatePostCommandHandler(IGenericRepository<Post> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Post>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}