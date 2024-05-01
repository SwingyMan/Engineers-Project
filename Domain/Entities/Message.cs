using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Message
{
    [Key]
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }

    public ICollection<ChatMessage> ChatMessages { get; set; }
}
