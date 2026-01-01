using NewYou.Domain.Entities.Common;

namespace NewYou.Application.Abstraction.Repositories.Command;
public interface IWriteRepository<T> : IRepository<T> where T : class, IEntityKeyBase, new()
{
    Task CreateAsync(T entity);
    Task AddRangeAsync(IList<T> entities);
    Task<T> UpdateAsync(T entity);
    Task HardDeleteAsync(T entity);
}
