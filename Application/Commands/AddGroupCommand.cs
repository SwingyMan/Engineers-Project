using System.Text.Json.Serialization;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddGroupCommand : IRequest<Group>
{
    [JsonIgnore]
    public Guid CallerId { get; set; }
    public GroupDTO GroupDto { get; set; }

    public AddGroupCommand(Guid callerId, GroupDTO groupDto)
    {
        CallerId = callerId;
        GroupDto = groupDto;
    }
}