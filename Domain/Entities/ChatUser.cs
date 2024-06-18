using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities;

public class ChatUser
{
    [Key] public Guid Id { get; set; }

    public Guid UserId { get; set; } //
    public Guid ChatId { get; set; }
    public Guid MessageId { get; set; }

    [JsonIgnore] public User User { get; set; } //

    [JsonIgnore] public Chat Chat { get; set; }

    [JsonIgnore] public Message Message { get; set; }
}