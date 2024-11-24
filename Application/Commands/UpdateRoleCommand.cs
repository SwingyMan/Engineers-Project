using Application.DTOs;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands;

public class UpdateRoleCommand : IRequest<Role>
{
    public RoleDTO entity { get; }
    public Guid id { get; }

    public UpdateRoleCommand(RoleDTO entity, Guid id)
    {
        this.entity = entity;
        this.id = id;
    }
}
