using MediatR;

namespace Application.Queries;

public class GenericGetByIdQuery<T> : IRequest<T>
{
    public Guid id { get; set; }

    public GenericGetByIdQuery(Guid guid)
    {
        id = guid;
    }
}