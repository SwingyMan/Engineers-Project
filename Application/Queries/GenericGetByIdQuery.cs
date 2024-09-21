using MediatR;

namespace Application.Queries;

public class GenericGetByIdQuery<T> : IRequest<T>
{
    public GenericGetByIdQuery(Guid guid)
    {
        id = guid;
    }

    public Guid id { get; set; }
}