using Microsoft.EntityFrameworkCore;
using NewYou.Domain.Entities.Common;

namespace NewYou.Application.Abstraction.Repositories;
public interface IRepository<T> where T : class, IEntityKeyBase, new()
{
    DbSet<T> Table { get; }

}
