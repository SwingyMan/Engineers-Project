
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }
    public Guid PostId { get; set; }
    [JsonIgnore]
    public Post Post { get; set; }
}