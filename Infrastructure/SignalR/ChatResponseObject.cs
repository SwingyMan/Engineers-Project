namespace Infrastructure.SignalR;

public class ChatResponseObject
{
    public string Id { get; set; }
    public ICollection<ChatMessageResponseObject> Messages { get; set; } = [];
    public ICollection<ChatUserResponseObject> Users { get; set; } = [];
}
