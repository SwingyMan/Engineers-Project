using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class GroupsUserQueryHandler : IRequestHandler<GroupsUserQuery,List<Group>>
{
    private readonly SocialPlatformDbContext _context;

    public GroupsUserQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<List<Group>> Handle(GroupsUserQuery request, CancellationToken cancellationToken)
    {
        return await _context.Groups.Where(x => x.GroupUsers.Where(x => x.UserId == request.userId && x.IsAccepted == true).Any()).ToListAsync();
    }
}