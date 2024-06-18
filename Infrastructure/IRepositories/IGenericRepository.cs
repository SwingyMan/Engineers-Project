using System.Linq.Expressions;

namespace Infrastructure.IRepositories;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetByID(Guid guid);
    Task<T> Update(Guid guid, T entity);
    Task Delete(Guid guid);
    Task<T> Add(T entity);
}
