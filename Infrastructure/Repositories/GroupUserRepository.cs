using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class GroupUserRepository : IGroupUserRepository
    {
        private readonly SocialPlatformDbContext _context;

        public GroupUserRepository(SocialPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<GroupUser> Update(GroupUser groupUser)
        {
            _context.Entry(groupUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return groupUser;
        }
    }
}
