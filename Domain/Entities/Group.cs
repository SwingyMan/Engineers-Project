using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Group
{
    [Key] public Guid Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageLink { get; set; }
    public DateTime CreatedAt { get; set; }

    [JsonIgnore] 
    public ICollection<GroupUser>? GroupUsers { get; set; }

    [JsonIgnore] 
    public ICollection<GroupPost>? GroupPosts { get; set; }
}