
using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IChatUserRepository
{
    Task<ChatUser> Update(ChatUser chatUser);
}
