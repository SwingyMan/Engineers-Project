using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore] public ICollection<User> Users { get; set; }
    public Message[] Messages { get; set; }
}