using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IGroupUserRepository
{
    Task<GroupUser> Update(GroupUser groupUser);
}