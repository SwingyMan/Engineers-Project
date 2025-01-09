using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IChatRepository
{
    Task<Chat?> AddChatAsync(Chat chat);
    Task<Chat?> GetChatById(Guid chatId);
    Task<Chat> GetChatByUserIds(Guid[] userIds);
    Task<IEnumerable<Chat>> GetChatsByUserId(Guid userGuid);
    Task<Chat> Update(Guid guid, Chat chat);
}   