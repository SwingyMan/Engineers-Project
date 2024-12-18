using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GroupQuery : IRequest<List<Group>>
{
    public string GroupName { get; set; }

    public GroupQuery(string groupName)
    {
        GroupName = groupName;
    }
}