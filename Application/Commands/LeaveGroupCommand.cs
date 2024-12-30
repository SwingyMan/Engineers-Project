using MediatR;

namespace Application.Commands;

public class LeaveGroupCommand : IRequest
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }

    public LeaveGroupCommand(Guid groupId, Guid userId)
    {
        GroupId = groupId;
        UserId = userId;
    }
}