namespace Infrastructure.SignalR;

public interface IChatClient
{
    Task ReceiveMessage(ChatHubMessageDTO message);
}
