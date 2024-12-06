using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities;

public class ChatUser
{
    [Key] public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
    public Guid ChatId { get; set; }
    [JsonIgnore] public Chat Chat { get; set; }
}