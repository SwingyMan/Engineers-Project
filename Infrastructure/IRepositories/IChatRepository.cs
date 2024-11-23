
using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IChatRepository
{
    Task<Chat> Update(Guid guid,Chat chat);
}