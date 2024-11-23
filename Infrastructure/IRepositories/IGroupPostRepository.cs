using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IGroupPostRepository
{
    Task<GroupPost> Update(GroupPost groupPost);
}
