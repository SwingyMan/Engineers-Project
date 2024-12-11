using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Commands;

public class AddGroupImageCommand : IRequest<Group>
{
    public IFormFile file { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
}