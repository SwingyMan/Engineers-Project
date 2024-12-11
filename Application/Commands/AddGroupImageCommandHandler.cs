using Domain.Entities;
using Infrastructure.Blobs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class AddGroupImageCommandHandler : IRequestHandler<AddGroupImageCommand,Group>
{
    private readonly IBlobInfrastructure _blobInfrastructure;
    private readonly SocialPlatformDbContext _context;

    public AddGroupImageCommandHandler(IBlobInfrastructure blobInfrastructure, SocialPlatformDbContext socialPlatformDbContext)
    {
        _blobInfrastructure = blobInfrastructure;
        _context = socialPlatformDbContext;
    }

    public async Task<Group> Handle(AddGroupImageCommand request, CancellationToken cancellationToken)
    {
        var user =await _context.Users.SingleAsync(u => u.Id == request.UserId);
        var group = await _context.Groups.SingleAsync(g => g.Id == request.GroupId);
        var groupUser = await _context.GroupUsers.SingleAsync(ug => ug.GroupId == request.GroupId && ug.UserId==user.Id && ug.IsOwner ==true);
        if (groupUser is null)
        {
            return null;
        }
        group.ImageLink = $"{group.Id}{Path.GetExtension(request.file.FileName)}";

        await _blobInfrastructure.addBlob(request.file, group.Id,"groups");

        _context.Groups.Update(group);
        await _context.SaveChangesAsync();
        group.GroupUsers = null;
        return group;
    }
}