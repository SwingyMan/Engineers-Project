

using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IRoleRepository
{
    Task<Role> Update(Guid guid,Role role);
}