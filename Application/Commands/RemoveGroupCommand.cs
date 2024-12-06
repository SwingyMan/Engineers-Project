using MediatR;

namespace Application.Commands;

public class RemoveGroupCommand : IRequest
{
    public Guid GroupId { get; set; }
    public Guid CallerId { get; set; }

    public RemoveGroupCommand(Guid groupId, Guid callerId)
    {
        GroupId = groupId;
        CallerId = callerId;
    }
}