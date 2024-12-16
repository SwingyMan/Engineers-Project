using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateGroupCommand : IRequest<Group>
{
    public string GroupName { get; set; }
    public string GroupDescription { get; set; }
    public Guid Groupid { get; }
    public Guid UserId { get; set; }

    public UpdateGroupCommand(string groupName, string groupDescription, Guid groupid, Guid userId)
    {
        GroupName = groupName;
        GroupDescription = groupDescription;
        Groupid = groupid;
        UserId = userId;
    }
}
