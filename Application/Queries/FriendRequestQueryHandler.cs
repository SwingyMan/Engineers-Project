using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class FriendRequestQueryHandler : IRequestHandler<FriendRequestQuery,List<Friends>>
{
    private readonly SocialPlatformDbContext _context;

    public FriendRequestQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<List<Friends>> Handle(FriendRequestQuery request, CancellationToken cancellationToken)
    {
        return await _context.Friends.Where(x => (x.UserId1 == request.UserId || x.UserId2 == request.UserId) && x.Accepted == false).ToListAsync(cancellationToken);
    }
}