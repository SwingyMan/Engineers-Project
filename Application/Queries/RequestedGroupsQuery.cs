using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class RequestedGroupsQuery : IRequest<List<Group>>
{
    public Guid UserId { get; set; }

    public RequestedGroupsQuery(Guid userId)
    {
        UserId = userId;
    }
}