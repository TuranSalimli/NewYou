using NewYou.Application.Abstraction.Repositories.Command;
using NewYou.Application.Abstraction.Repositories.Query;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Persistance.Contexts;
using NewYou.Persistence.Concretes.Repositories.Command;
using NewYou.Persistence.Concretes.Repositories.Query;

namespace NewYou.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly NewYouDbContext _dbContext;

    public UnitOfWork(NewYouDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();


    public int Save() => _dbContext.SaveChanges();

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();

    IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_dbContext);
    IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_dbContext);
}
