using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities;

public class GroupUser
{
    [Key] public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public bool IsAccepted { get; set; } = false;
    public bool IsOwner { get; set; } = false;

    [JsonIgnore] 
    public User User { get; set; }

    [JsonIgnore]
    public Group Group { get; set; }

    public GroupUser(Guid userId,Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }
}