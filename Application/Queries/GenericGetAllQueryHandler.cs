using AutoMapper;
using Azure;
using Infrastructure.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class GenericGetAllQueryHandler<T> : IRequestHandler<GenericGetAllQuery<T>, IEnumerable<T>> where T : class
{
    private readonly IGenericRepository<T> _genericRepository;
    private readonly IMapper _mapper;
    public GenericGetAllQueryHandler(IGenericRepository<T> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<T>> Handle(GenericGetAllQuery<T> request, CancellationToken cancellationToken)
    {
        var entities = await _genericRepository.GetAll();
        return _mapper.Map<IEnumerable<T>>(entities);
    }
}