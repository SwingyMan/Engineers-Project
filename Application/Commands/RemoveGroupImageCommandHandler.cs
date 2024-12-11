using Infrastructure.Blobs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class RemoveGroupImageCommandHandler : IRequestHandler<RemoveGroupImageCommand>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IBlobInfrastructure _blobInfrastructure;
    public RemoveGroupImageCommandHandler(SocialPlatformDbContext context, IBlobInfrastructure blobInfrastructure)
    {
        _context = context;
        _blobInfrastructure = blobInfrastructure;
    }
    public async Task Handle(RemoveGroupImageCommand request, CancellationToken cancellationToken)
    { 
        var group = await _context.Groups.SingleAsync(g => g.Id == request.GroupId);
        var groupUser = await _context.GroupUsers.SingleAsync(ug => ug.GroupId == request.GroupId && ug.UserId==request.UserId && ug.IsOwner ==true);
        if (groupUser is null)
        {
            return;
        }

        if (group.ImageLink == "default.png")
        {
            return;
        }
        await _blobInfrastructure.deleteBlob(group.ImageLink, "groups");
        group.ImageLink = "default.png";
        _context.Groups.Update(group);
        await _context.SaveChangesAsync();
    }
}