

using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IRoleRepository
{
    Task<Role> Update(Role role);
}