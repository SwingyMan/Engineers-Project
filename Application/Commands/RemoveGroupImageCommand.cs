using MediatR;

namespace Application.Commands;

public class RemoveGroupImageCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }

    public RemoveGroupImageCommand(Guid userId, Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }
}