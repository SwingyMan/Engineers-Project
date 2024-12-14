using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class FriendRequestQuery : IRequest<List<Friends>>
{
    public Guid UserId { get; set; }

    public FriendRequestQuery(Guid userId)
    {
        UserId = userId;
    }
}