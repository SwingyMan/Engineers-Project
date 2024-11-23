using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IUserRepository
{
    public Task<bool> CheckEmail(string email);
    Task<User> Update(User user);
}