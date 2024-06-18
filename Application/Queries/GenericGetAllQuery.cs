using MediatR;

namespace Application.Queries;

public class GenericGetAllQuery<T> : IRequest<IEnumerable<T>>
{

}
