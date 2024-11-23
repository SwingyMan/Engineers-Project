using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class ChatUserRepository : IChatUserRepository
{
    private readonly SocialPlatformDbContext _context;

    public ChatUserRepository(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<ChatUser> Update(ChatUser chatUser)
    {
        _context.Entry(chatUser).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return chatUser;
    }
}
