using AutoMapper;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class GenericUpdateCommandHandler<T, R> : IRequestHandler<GenericUpdateCommand<T, R>, R> where T : class where R : class
{
    private readonly IGenericRepository<R> _genericRepository;
    private readonly IMapper _mapper;
    public GenericUpdateCommandHandler(IGenericRepository<R> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<R> Handle(GenericUpdateCommand<T, R> request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<R>(request.Entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}