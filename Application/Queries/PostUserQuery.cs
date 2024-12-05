using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class PostUserQuery : IRequest<List<Post>>
{
    public Guid UserId { get; set; }
    public Guid OwnerId { get; set; }

    public PostUserQuery(Guid userId, Guid ownerId)
    {
        UserId = userId;
        OwnerId = ownerId;
    }
}