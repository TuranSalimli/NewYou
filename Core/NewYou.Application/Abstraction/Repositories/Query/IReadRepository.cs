using Microsoft.EntityFrameworkCore.Query;
using NewYou.Application.Abstraction.Repositories;
using NewYou.Domain.Entities.Common;
using System.Linq.Expressions;

namespace NewYou.Application.Abstraction.Repositories.Query;
public interface IReadRepository<T> : IRepository<T> where T : class, IEntityKeyBase, new()
{
    Task<T?> GetByIdAsync(Guid id, bool IsTracking = false, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false);
    Task<IList<T>> GetAllPagingAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false, int currentPage = 1, int pageSize = 3
        );

    Task<T?> GetAsync(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false);

    IQueryable<T> EntityFind(Expression<Func<T, bool>> predicate, bool enableTracking = false);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
}
