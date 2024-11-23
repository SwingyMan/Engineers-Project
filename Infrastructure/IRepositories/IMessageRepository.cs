
using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface IMessageRepository
{
    Task<Message> Update(Message message);
}
