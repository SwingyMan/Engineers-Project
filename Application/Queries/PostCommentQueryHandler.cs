using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class PostCommentQueryHandler : IRequestHandler<PostCommentQuery,List<Comment>>
{
    private readonly SocialPlatformDbContext _dbContext;

    public PostCommentQueryHandler(SocialPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Comment>> Handle(PostCommentQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.Where(x => x.PostId == request.PostId).ToListAsync(cancellationToken);
    }
}