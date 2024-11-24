using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class GroupPostRepository : IGroupPostRepository
{
    private readonly SocialPlatformDbContext _context;

    public GroupPostRepository(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<GroupPost> Update(GroupPost groupPost)
    {
        _context.Entry(groupPost).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return groupPost;
    }
}
