using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class RemoveGroupCommandHandler : IRequestHandler<RemoveGroupCommand>
{
    private readonly SocialPlatformDbContext _context;

    public RemoveGroupCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
    {
        var check =await _context.GroupUsers.SingleAsync(x=>x.GroupId==request.GroupId&& x.UserId==request.CallerId&& x.IsOwner==true);
        if (check is null)
        {
            return;
        }

        var group =await _context.Groups.SingleAsync(x => x.Id == request.GroupId);
        var users =await _context.GroupUsers.Where(x => x.GroupId == group.Id).ToListAsync();
        var posts =await _context.GroupPosts.Where(x => x.GroupId == group.Id).ToListAsync();
        _context.RemoveRange(users);
        _context.RemoveRange(posts);
        _context.Remove(group);
    }
}