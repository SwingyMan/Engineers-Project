using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class PostQueryHandler : IRequestHandler<PostQuery, List<Post>>
{
    private readonly SocialPlatformDbContext _context;

    public PostQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<List<Post>> Handle(PostQuery request, CancellationToken cancellationToken)
    {
        var friends = _context.Friends.Where(x=>(x.UserId1==request.UserId && x.Accepted==true) || (x.UserId2==request.UserId && x.Accepted==true)).ToList();
        var own = _context.Posts.Where(x => x.UserId == request.UserId && x.Availability == Availability.Friends);
        var publicPosts =  _context.Posts.Where(x => x.Availability == Availability.Public).ToList();
        foreach (var friend in friends)
        {
            if (request.UserId == friend.UserId1)
                publicPosts.AddRange( _context.Posts.Where(x =>
                    (x.UserId == friend.UserId2 && x.Availability == Availability.Friends)).ToList());
            else
            {
                publicPosts.AddRange(_context.Posts.Where(x =>
                        (x.UserId == friend.UserId1 && x.Availability == Availability.Friends))
                    .ToList());
            }
        }
        publicPosts.AddRange(own);

        return publicPosts.ToList();
    }
}