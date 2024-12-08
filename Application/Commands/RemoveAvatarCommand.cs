using MediatR;

namespace Application.Commands;

public class RemoveAvatarCommand : IRequest
{
    public Guid UserId { get; set; }

    public RemoveAvatarCommand(Guid userId)
    {
        UserId = userId;
    }
}