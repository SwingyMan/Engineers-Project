using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetChatsByUserIdQuery : IRequest<IEnumerable<Chat>>
{
    public GetChatsByUserIdQuery(Guid guid)
    {
        userGuid = guid;
    }

    public Guid userGuid;
}
