using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class RequestToGroupCommand : IRequest<GroupUser>
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }

    public RequestToGroupCommand(Guid groupId, Guid userId)
    {
        GroupId = groupId;
        UserId = userId;
    }
}