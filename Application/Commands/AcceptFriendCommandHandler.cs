using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class AcceptFriendCommandHandler : IRequestHandler<AcceptFriendCommand,Friends>
{
    private readonly SocialPlatformDbContext _context;

    public AcceptFriendCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<Friends> Handle(AcceptFriendCommand request, CancellationToken cancellationToken)
    {
        var friend = await _context.Friends.FirstAsync(x=>x.UserId1==request.FriendId && x.UserId2==request.AcceptorId);
        friend.Accepted=true;
         _context.Friends.Update(friend);
        await _context.SaveChangesAsync(cancellationToken);
        return friend;
    }
}