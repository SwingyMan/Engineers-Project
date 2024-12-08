
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Post>
{
    private readonly IPostsRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePostCommandHandler(IPostsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Post>(request.entity);
        return await _repository.UpdatePostAsync(request.id, mapped);
    }
}