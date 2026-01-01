using NewYou.Application.Abstraction.Repositories.Command;
using NewYou.Application.Abstraction.Repositories.Query;
using NewYou.Domain.Entities.Common;
using NewYou.Domain.Entities;

namespace NewYou.Application.Abstraction.UnitOfWorks;
public interface IUnitOfWork : IAsyncDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : class, IEntityKeyBase, new();
    IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityKeyBase, new();
    Task<int> SaveAsync();
    int Save();
}
