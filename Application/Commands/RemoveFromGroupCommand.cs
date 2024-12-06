using MediatR;

namespace Application.Commands;

public class RemoveFromGroupCommand : IRequest
{
    public Guid CallerId { get; set; }
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }

    public RemoveFromGroupCommand(Guid callerId, Guid groupId, Guid userId)
    {
        CallerId = callerId;
        GroupId = groupId;
        UserId = userId;
    }
}