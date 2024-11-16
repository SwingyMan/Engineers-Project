using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AcceptFriendCommand : IRequest<Friends>
{
    public Guid AcceptorId { get; set; }
    public Guid FriendId { get; set; }

    public AcceptFriendCommand(Guid acceptorId, Guid friendId)
    {
        AcceptorId = acceptorId;
        FriendId = friendId;
    }
}