using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AcceptToGroupCommand : IRequest<GroupUser>
{
    public Guid CallerId { get; set; }
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }

    public AcceptToGroupCommand(Guid callerId, Guid groupId, Guid userId)
    {
        CallerId = callerId;
        GroupId = groupId;
        UserId = userId;
    }
}