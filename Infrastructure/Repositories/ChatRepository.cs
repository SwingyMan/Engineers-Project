using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository(SocialPlatformDbContext _context) : IChatRepository
{
    public async Task<Chat?> GetChatByUserIds(Guid[] userIds)
    {
        return await _context.Chats
            .Include(c => c.Users)
            .Include(m => m.Messages)
            .FirstOrDefaultAsync(c => c.Users.Count == 2
            && c.Users.Any(u => u.Id == userIds[0])
            && c.Users.Any(u => u.Id == userIds[1]));
    }

    public async Task<Chat?> GetChatById(Guid chatId)
    {
        return await _context.Chats
            .Include(u => u.Users)
            .Include(m => m.Messages)
            .FirstOrDefaultAsync(c => c.Id == chatId);
    }

    public async Task<Chat?> AddChatAsync(Chat chat)
    {
        _context.Set<Chat>().Add(chat);
        await _context.SaveChangesAsync();
        return chat;
    }

    public async Task<IEnumerable<Chat>> GetChatsByUserId(Guid userGuid)
    {
        return await _context.Chats
            .Include(u => u.Users)
            .Where(chat => chat.Users.Any(user => user.Id == userGuid))
            .Select(chat => new Chat
            {
                Id = chat.Id,
                Name = chat.Name,
                Users = chat.Users,
                Messages = chat.Messages
                    .OrderByDescending(m => m.CreationDate)
                    .Take(1)
                    .ToList()
            })
            .ToListAsync();
    }

    public async Task<Chat> Update(Guid guid, Chat chat)
    {
        _context.Set<Chat>().Find(guid);
        _context.Entry(chat).CurrentValues.SetValues(chat);
        await _context.SaveChangesAsync();
        return chat;
    }
}
