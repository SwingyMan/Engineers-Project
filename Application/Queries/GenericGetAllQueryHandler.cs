using Infrastructure.IRepositories;
using MediatR;

namespace Application.Queries;

public class GenericGetAllQueryHandler<T> : IRequestHandler<GenericGetAllQuery<T>, IEnumerable<T>> where T : class
{
    private readonly IGenericRepository<T> _genericRepository;

    public GenericGetAllQueryHandler(IGenericRepository<T> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IEnumerable<T>> Handle(GenericGetAllQuery<T> request, CancellationToken cancellationToken)
    {
        return await _genericRepository.GetAll();
    }
}