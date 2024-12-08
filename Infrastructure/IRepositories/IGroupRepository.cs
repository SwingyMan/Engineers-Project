
using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IGroupRepository
{
    Task<Group> Update(Guid guid,Group group);
}
