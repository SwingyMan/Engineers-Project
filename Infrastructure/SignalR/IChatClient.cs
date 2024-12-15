namespace Infrastructure.SignalR;

public interface IChatClient
{
    Task ReceiveMessage(ChatMessageResponseObject message);
}
