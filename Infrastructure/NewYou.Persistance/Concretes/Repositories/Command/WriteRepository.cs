using Microsoft.EntityFrameworkCore;
using NewYou.Application.Abstraction.Repositories.Command;
using NewYou.Domain.Entities.Common;
using NewYou.Persistance.Contexts;

namespace NewYou.Persistence.Concretes.Repositories.Command;

public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityKeyBase, new()
{
    private readonly NewYouDbContext _context;
    public WriteRepository(NewYouDbContext context)
    {
        _context = context;
    }
    public DbSet<T> Table => _context.Set<T>();

    public async Task AddRangeAsync(IList<T> entities)
    {
        await Table.AddRangeAsync(entities);
    }

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task HardDeleteAsync(T entity)
    {
        Table.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Table.Update(entity);
        return await Task.FromResult(entity);
    }
}