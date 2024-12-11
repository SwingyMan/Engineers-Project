using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Queries;

public class GroupImageQuery : IRequest<FileStreamResult>
{
    public Guid GroupId { get; set; }

    public GroupImageQuery(Guid groupId)
    {
        GroupId = groupId;
    }
}