using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class RemoveFriendsCommandHandler : IRequestHandler<RemoveFriendsCommand>
{
    private readonly SocialPlatformDbContext _context;

    public RemoveFriendsCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task Handle(RemoveFriendsCommand request, CancellationToken cancellationToken)
    {
        var friends =await _context.Friends.FirstAsync(x=>(x.UserId1==request.UserId1 && x.UserId2 == request.UserId2) || (x.UserId1 == request.UserId2 && x.UserId2 == request.UserId1));
         _context.Remove(friends);
        await _context.SaveChangesAsync(cancellationToken);
    }
}