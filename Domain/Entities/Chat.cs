namespace Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<ChatUser>? ChatUsers { get; set; }
}
