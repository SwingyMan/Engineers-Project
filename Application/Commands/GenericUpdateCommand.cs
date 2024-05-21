using MediatR;

namespace Application.Commands;

public class GenericUpdateCommand<T, R> : IRequest<R>
{
    public Guid id { get; set; }
    public T Entity { get; set; }
}
