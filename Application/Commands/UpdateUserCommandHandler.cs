

using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IGenericRepository<User> _genericRepository;
    private readonly IMapper _mapper;

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<User>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}