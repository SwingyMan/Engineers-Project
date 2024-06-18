using AutoMapper;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class GenericAddCommandHandler<T, D> : IRequestHandler<GenericAddCommand<T, D>, D> where D : class
{
    private readonly IGenericRepository<D> _genericRepository;
    private readonly IMapper _mapper;

    public GenericAddCommandHandler(IGenericRepository<D> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task<D> Handle(GenericAddCommand<T, D> request, CancellationToken cancellationToken)
    {
        var mappedEntity = _mapper.Map<D>(request.entity);
        return await _genericRepository.Add(mappedEntity);
    }
}