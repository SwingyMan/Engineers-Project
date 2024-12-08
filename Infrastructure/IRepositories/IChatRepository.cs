using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IChatRepository
{
    Task<Chat?> AddChatAsync(Chat chat);
    Task<Chat?> GetChatById(Guid chatId);
    Task<Chat> GetChatByUserIds(Guid[] userIds);
}
