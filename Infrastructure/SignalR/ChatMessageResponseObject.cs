namespace Infrastructure.SignalR;

public class ChatMessageResponseObject
{
    public string Id { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public ChatUserResponseObject User { get; set; }
}
