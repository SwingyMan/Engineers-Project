using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class AddPostCommandHandler : IRequestHandler<AddPostCommand, Post>
{
    private readonly IGenericRepository<Post> _genericRepository;
    private readonly IMapper _mapper;

    public AddPostCommandHandler(IGenericRepository<Post> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task<Post> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var postEntity = _mapper.Map<Post>(request.entity);
        postEntity.UserId = request.guid;
        postEntity.CreatedAt = DateTime.UtcNow;
        postEntity.Status = "status";
        postEntity.Availability = "availability";
        return await _genericRepository.Add(postEntity);
    }
}
