using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class FriendQueryHandler : IRequestHandler<FriendQuery,List<Friends>>
{
    private readonly SocialPlatformDbContext _context;

    public FriendQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<List<Friends>> Handle(FriendQuery request, CancellationToken cancellationToken)
    {
        return await _context.Friends.Where(x => x.UserId1 == request.UserId || x.UserId2 == request.UserId)
            .ToListAsync(cancellationToken);
    }
}