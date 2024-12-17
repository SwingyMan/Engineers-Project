using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<User> Users { get; set; } = new List<User>();
    [JsonIgnore]
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public bool IsGroupChat { get; set; }
}