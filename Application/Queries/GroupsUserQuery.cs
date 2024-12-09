using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GroupsUserQuery : IRequest<List<Group>>
{
    public Guid userId { get; set; }

    public GroupsUserQuery(Guid userId)
    {
        this.userId = userId;
    }
}