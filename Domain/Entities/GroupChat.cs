namespace Domain.Entities;

public class GroupChat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<ChatMessage> ChatMessages { get; set; }
}
