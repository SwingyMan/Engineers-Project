using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class PostTitleQueryHandler : IRequestHandler<PostTitleQuery,List<Post>>
{
    private readonly IMediator _mediator;

    public PostTitleQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<List<Post>> Handle(PostTitleQuery request, CancellationToken cancellationToken)
    {
        var posts = await _mediator.Send(new PostQuery(request.UserId));
        return posts.Where(x=>x.Title.Contains(request.Title,StringComparison.OrdinalIgnoreCase)).ToList();
    }
}