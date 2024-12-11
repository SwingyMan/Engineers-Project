using Infrastructure.Blobs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class RemoveAvatarCommandHandler : IRequestHandler<RemoveAvatarCommand>
{
    private readonly IBlobInfrastructure _blobInfrastructure;
    private readonly SocialPlatformDbContext _context;

    public RemoveAvatarCommandHandler(IBlobInfrastructure blobInfrastructure, SocialPlatformDbContext context)
    {
        _blobInfrastructure = blobInfrastructure;
        _context = context;
    }
    public async Task Handle(RemoveAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId,cancellationToken);
        if (user.AvatarFileName == "default.jpg")
        {
            return;
        }
        await _blobInfrastructure.deleteBlob(user.AvatarFileName, "avatars");
        user.AvatarFileName = "default.jpg";
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}