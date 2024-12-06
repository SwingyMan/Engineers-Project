namespace Infrastructure.SignalR;

public class ChatHubSentMessageDTO
{
    public string Id { get; set; }
    public string Message { get; set; }
    public string SenderId { get; set; }
    public string SenderName { get; set; }
    public DateTime DateTime { get; set; }
}
