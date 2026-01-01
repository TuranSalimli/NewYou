using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NewYou.Application.Abstraction.Repositories.Query;
using NewYou.Domain.Entities.Common;
using NewYou.Persistance.Contexts;
using System.Linq.Expressions;

namespace NewYou.Persistence.Concretes.Repositories.Query;
public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityKeyBase, new()
{
    private readonly NewYouDbContext _context;
    public ReadRepository(NewYouDbContext context)
    {
        _context = context;
    }
    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.AsNoTracking().AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate)
    {
        IQueryable<T> query = Table.AsNoTracking();
        if (predicate is not null) query = query.Where(predicate);
        return await query.CountAsync();
    }

    public IQueryable<T> EntityFind(Expression<Func<T, bool>> predicate, bool enableTracking = false)
    {
        if (!enableTracking) Table.AsNoTracking();
        return Table.Where(predicate);
    }

    public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
    {
        IQueryable<T> queryable = Table;
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include is not null) queryable = include(queryable);
        if (predicate is not null) queryable = queryable.Where(predicate);
        if (orderBy is not null)
            return await orderBy(queryable).ToListAsync();
        return await queryable.ToListAsync();

    }

    public async Task<IList<T>> GetAllPagingAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
    {
        IQueryable<T> queryable = Table;
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include is not null) queryable = include(queryable);
        if (predicate is not null) queryable = queryable.Where(predicate);
        if (orderBy is not null)
            return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
    {
        IQueryable<T> queryable = Table;
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include is not null) queryable = include(queryable);

        //queryable = queryable.Where(predicate);

        return await queryable.FirstOrDefaultAsync(predicate);
    }

    public async Task<T?> GetByIdAsync(Guid id, bool IsTracking = false, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> queryable = Table;
        if (include is not null) queryable = include(queryable);
        var query = queryable.AsQueryable();
        if (!IsTracking)
        {
            query = query.AsNoTracking();
        }
        T? entity = await query.SingleOrDefaultAsync(t => t.Id == id);
        return entity;
    }

}
