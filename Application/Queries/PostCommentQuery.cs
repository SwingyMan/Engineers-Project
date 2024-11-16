using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class PostCommentQuery : IRequest<List<Comment>>
{
    public Guid PostId { get; set; }

    public PostCommentQuery(Guid postId)
    {
        PostId = postId;
    }
}