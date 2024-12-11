using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands;

public class AddAvatarCommand : IRequest<User>
{
    public Guid UserId { get; set; }
    public IFormFile Avatar { get; set; }

    public AddAvatarCommand(Guid userId, IFormFile avatar)
    {
        UserId = userId;
        Avatar = avatar;
    }
}