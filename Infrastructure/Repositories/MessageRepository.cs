using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly SocialPlatformDbContext _context;

    public MessageRepository(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Message> Update(Message message)
    {
        _context.Entry(message).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return message;
    }
}
