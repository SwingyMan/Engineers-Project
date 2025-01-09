using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddGroupChatCommand : IRequest<Chat>
{
    public ICollection<Guid> usersGuids { get; set; } = Array.Empty<Guid>();
    public string Name { get; set; } = "";
}
