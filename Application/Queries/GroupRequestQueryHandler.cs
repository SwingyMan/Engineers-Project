using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class GroupRequestQueryHandler : IRequestHandler<GroupRequestQuery,List<GroupUser>>
{
    private readonly SocialPlatformDbContext _context;

    public GroupRequestQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<List<GroupUser>> Handle(GroupRequestQuery request, CancellationToken cancellationToken)
    {
        var user = _context.GroupUsers.Single(u => u.GroupId == request.GroupId && u.UserId==request.UserId && u.IsOwner==true);
        if (user == null)
        {
            return null;
        }
        return await _context.GroupUsers.Where(x => x.GroupId == request.GroupId && x.IsAccepted ==false).ToListAsync(cancellationToken);
    }
}