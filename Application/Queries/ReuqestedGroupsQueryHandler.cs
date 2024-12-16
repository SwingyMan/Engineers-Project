using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class ReuqestedGroupsQueryHandler : IRequestHandler<RequestedGroupsQuery,List<Group>>
{
    private readonly SocialPlatformDbContext _context;

    public ReuqestedGroupsQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<List<Group>> Handle(RequestedGroupsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Groups
            .Where(x => x.GroupUsers.Any(x => x.UserId == request.UserId && x.IsAccepted == false)).ToListAsync();
    }
}