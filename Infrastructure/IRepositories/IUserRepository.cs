namespace Infrastructure.IRepositories;

public interface IUserRepository
{
    public Task<bool> CheckEmail(string email);
}