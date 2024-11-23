using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly SocialPlatformDbContext _context;

    public RoleRepository(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Role> Update(Guid guid,Role role)
    {
        var entity = await _context.Roles.FindAsync(guid);
        role.Name = role.Name;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}