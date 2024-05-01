using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly SocialPlatformDbContext _context;

    public GenericRepository(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAll(int page, int limit, params Expression<Func<T, object>>[] includeProperties)
    {
        var queries = _context.Set<T>();
        foreach (var VARIABLE in includeProperties)
        {
            queries.Include(VARIABLE);
        }
        return await queries.ToListAsync();
    }

    public async Task<T> GetByID(Guid guid)
    {
        var entity = await _context.Set<T>().FindAsync(guid);
        return entity;
    }

    public async Task<T> Update(Guid guid, T entity)
    {
        _context.Set<T>().Find(guid);
        _context.Entry(entity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(Guid guid)
    {
        var entity = _context.Set<T>();
        var deleteEntity = await entity.FindAsync(guid);
        _context.Remove(deleteEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}