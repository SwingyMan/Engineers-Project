using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities;

public class ChatMessage
{
    [Key] public Guid Id { get; set; }

    public Guid ChatId { get; set; }
    public Guid MessageId { get; set; }

    [JsonIgnore] public Message Message { get; set; }
}