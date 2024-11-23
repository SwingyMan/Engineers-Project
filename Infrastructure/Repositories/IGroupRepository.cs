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

    public async Task<Group> Update(Group group)
    {
        _context.Entry(group).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return group;
    }
}