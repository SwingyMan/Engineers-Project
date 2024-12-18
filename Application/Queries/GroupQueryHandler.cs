using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class GroupQueryHandler : IRequestHandler<GroupQuery,List<Group>>
{
    private readonly SocialPlatformDbContext _context;

    public GroupQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<List<Group>> Handle(GroupQuery request, CancellationToken cancellationToken)
    {
        return _context.Groups.ToList()
            .Where(x => x.Name.Contains(request.GroupName, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}