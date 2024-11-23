using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly SocialPlatformDbContext _context;

    public GroupRepository(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Group> Update(Guid guid,Group group)
    {
        var entity = await _context.Groups.FindAsync(guid);
        entity.Name = group.Name;
        entity.Description = group.Description;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}