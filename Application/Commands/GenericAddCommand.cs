using MediatR;

namespace Application.Commands;

public class GenericAddCommand<T, D> : IRequest<D>
{
    public GenericAddCommand(T entity)
    {
        this.entity = entity;
    }
    public T entity { get; set; }
}