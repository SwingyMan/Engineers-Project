using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class PostQuery : IRequest<List<Post>>
{
    public Guid UserId { get; set; }

    public PostQuery(Guid userId)
    {
        UserId = userId;
    }
}