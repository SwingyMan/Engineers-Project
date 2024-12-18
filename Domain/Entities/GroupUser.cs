using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class GroupUser
{
    [JsonIgnore]
    [Key] public Guid Id { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public Guid GroupId { get; set; }
    public bool IsAccepted { get; set; } = false;
    public bool IsOwner { get; set; } = false;

    public User User { get; set; }

    [JsonIgnore]
    public Group Group { get; set; }

    public GroupUser(Guid userId,Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }
}