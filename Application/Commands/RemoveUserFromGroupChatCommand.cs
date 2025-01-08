using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class RemoveUserFromGroupChatCommand : IRequest<Chat>
{
    public Guid GroupChatGuid { get; set; }
    public Guid UserGuid { get; set; }
    public RemoveUserFromGroupChatCommand(Guid groupChatGuid, Guid userGuid)
    {
        GroupChatGuid = groupChatGuid;
        UserGuid = userGuid;
    }
}
