using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class FriendQuery : IRequest<List<Friends>>
{
    public Guid UserId { get; set; }

    public FriendQuery(Guid userId)
    {
        UserId = userId;
    }
}