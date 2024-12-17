using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Message
{
    [Key] public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
    [JsonIgnore] public Chat Chat { get; set; }
}