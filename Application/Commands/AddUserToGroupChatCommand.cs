using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands;

public class AddUserFromGroupChatCommand : IRequest<Chat>
{
    public Guid GroupChatGuid { get; set; }
    public Guid UserGuid { get; set; }
    public AddUserFromGroupChatCommand(Guid groupChatGuid, Guid userGuid)
    {
        GroupChatGuid = groupChatGuid;
        UserGuid = userGuid;
    }
}
