using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SocialPlatformDbContext _context;

    public UserRepository(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckEmail(string email)
    {
        var entity = await _context.Users.Where(x => x.Email == email).ToListAsync();
        if (entity.Count == 0) return true;
        return false;
    }
}