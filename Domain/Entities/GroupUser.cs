using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities;

public class GroupUser
{
    [Key] public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }

    [JsonIgnore] public User User { get; set; }

    [JsonIgnore] public Group Group { get; set; }
}