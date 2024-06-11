using Infrastructure.IRepositories;
using MediatR;

namespace Application.Queries
{
    public class GenericGetByIdQueryHandler<T> : IRequestHandler<GenericGetByIdQuery<T>, T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        public GenericGetByIdQueryHandler(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<T> Handle(GenericGetByIdQuery<T> request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetByID(request.id);
        }
    }
}
