using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class PostGroupQuery : IRequest<List<Post>>
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }

    public PostGroupQuery(Guid userId,Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }
}