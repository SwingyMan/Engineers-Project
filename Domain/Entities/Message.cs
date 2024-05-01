namespace Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }

    public ICollection<ChatMessage> ChatMessages { get; set; }
}
