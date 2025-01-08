using Application.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class FriendQueryHandler : IRequestHandler<FriendQuery,FriendsDTO>
{
    private readonly SocialPlatformDbContext _context;

    public FriendQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<FriendsDTO> Handle(FriendQuery request, CancellationToken cancellationToken)
    {
        var friends = await _context.Friends.Where(x => x.UserId1 == request.UserId || x.UserId2 == request.UserId)
            .ToListAsync(cancellationToken);
        var accepted = friends.Where(x=>x.Accepted).ToList();
        var sent = friends.Where(x => x.UserId1 == request.UserId && x.Accepted == false).ToList();
        var received = friends.Where(x => x.UserId2 == request.UserId && x.Accepted == false).ToList();
        var acceptedIds = accepted.Select(x => x.UserId1).ToList();
        acceptedIds.AddRange(accepted.Select(x => x.UserId2).ToList());
        acceptedIds = acceptedIds.Where(x=>x != request.UserId).ToList();
        var sentIds = sent.Select(x => x.UserId2).Distinct().ToList();
        var receivedIds = received.Select(x => x.UserId1).Distinct().ToList();
        var acceptedUsers =await _context.Users.Where(x => acceptedIds.Contains(x.Id)).ToListAsync(cancellationToken);
        var sentUsers =await _context.Users.Where(x => sentIds.Contains(x.Id)).ToListAsync(cancellationToken);
        var receivedUsers =await _context.Users.Where(x => receivedIds.Contains(x.Id)).ToListAsync(cancellationToken);
        var friendsDTO = new FriendsDTO(sentUsers,receivedUsers, acceptedUsers);
        
        return friendsDTO;
    }
}