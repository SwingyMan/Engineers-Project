using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Queries;

public class PostUserQueryHandler : IRequestHandler<PostUserQuery, List<Post>>
{
    private readonly IMediator _mediator;
    public PostUserQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<List<Post>> Handle(PostUserQuery request, CancellationToken cancellationToken)
    {
        var posts = await _mediator.Send(new PostQuery(request.OwnerId));
        return posts.Where(p => p.UserId == request.UserId).ToList();
    }
}