using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class PostGroupQueryHandler : IRequestHandler<PostGroupQuery,List<Post>>
{
    private readonly SocialPlatformDbContext _context;

    public PostGroupQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<List<Post>> Handle(PostGroupQuery request, CancellationToken cancellationToken)
    {
        var check = _context.GroupUsers.Single(x =>
            x.GroupId == request.GroupId && x.UserId == request.UserId && x.IsAccepted == true);
        if (check == null)
            return null;
        return await _context.Posts.Where(x =>
            x.Availability == Availability.Group && x.GroupPosts.Where(x => x.GroupId == request.GroupId).Any()).ToListAsync();
    }
}