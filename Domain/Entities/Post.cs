using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Post
{
    [Key] public Guid Id { get; set; }

    public string Title { get; set; }
    public string Body { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; }
    public Availability Availability { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Attachments> Attachments { get; set; }
     public User User { get; set; }

    [JsonIgnore] public IEnumerable<GroupPost>? GroupPosts { get; set; }
}

public enum Availability
{
    Public,Friends
}