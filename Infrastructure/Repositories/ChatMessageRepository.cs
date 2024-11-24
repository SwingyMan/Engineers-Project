using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly SocialPlatformDbContext _context;

        public ChatMessageRepository(SocialPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<ChatMessage> Update(ChatMessage chatMessage)
        {
            _context.Entry(chatMessage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return chatMessage;
        }
    }
}
