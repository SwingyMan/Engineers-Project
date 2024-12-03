using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IChatRepository
{
    Task AddChatAsync(Chat chat);
    Task<Chat> GetChatByUserIds(Guid[] userIds);
}
