using MediatR;

namespace Application.Commands;

public class GenericDeleteCommand<T> : IRequest
{
    public GenericDeleteCommand(Guid guid)
    {
        Id = guid;
    }
    public Guid Id { get; set; }
}