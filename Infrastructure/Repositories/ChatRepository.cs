using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository(SocialPlatformDbContext _context) : IChatRepository
{
    public async Task<Chat> Update(Guid guid,Chat chat)
    {        var entity = await _context.Chats.FindAsync(guid);
        entity.Name = chat.Name;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}
