using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GroupRequestQuery : IRequest<List<GroupUser>>
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }

    public GroupRequestQuery(Guid userId, Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }
}