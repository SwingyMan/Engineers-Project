namespace Infrastructure.SignalR;
public class ChatHubMessageDTO
{
    public string ChatId { get; set; }
    public string SenderId { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
