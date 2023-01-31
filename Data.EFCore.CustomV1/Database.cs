using Data.EFCore.CustomV1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.EFCore.CustomV1;

public class Database<T> : IDatabase<T> where T : class
{
    public DbContext DbContext { get; }

    public Database(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await DbContext.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await DbContext.Set<T>().FindAsync(id);

        if (entity == null)
        {
            return null;
        }

        DbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }
}