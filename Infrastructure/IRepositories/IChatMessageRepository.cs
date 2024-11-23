using Domain.Entities;


namespace Infrastructure.IRepositories;

public interface IChatMessageRepository
{
    Task<ChatMessage> Update(ChatMessage chatMessage);
}
