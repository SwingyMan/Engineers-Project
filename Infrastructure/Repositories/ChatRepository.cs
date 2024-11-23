using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository(SocialPlatformDbContext _context) : IChatRepository
{
    public async Task<Chat> Update(Chat chat)
    {
        _context.Entry(chat).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return chat;
    }
}
