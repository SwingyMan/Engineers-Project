using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class GenericDeleteCommandHandler<T> : IRequestHandler<GenericDeleteCommand<T>> where T : class
{
    private readonly IGenericRepository<T> _genericRepository;

    public GenericDeleteCommandHandler(IGenericRepository<T> genericRepository)
    {
        _genericRepository = genericRepository;
    }
    public async Task Handle(GenericDeleteCommand<T> request, CancellationToken cancellationToken)
    {
        await _genericRepository.Delete(request.Id);
    }
}