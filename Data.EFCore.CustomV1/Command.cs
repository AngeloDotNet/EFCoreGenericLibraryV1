using Data.EFCore.CustomV1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.EFCore.CustomV1;

public class Command<T> : ICommand<T> where T : class
{
    public DbContext DbContext { get; }

    public Command(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task CreateAsync(T entity)
    {
        DbContext.Set<T>().Add(entity);

        await DbContext.SaveChangesAsync();

        DbContext.Entry(entity).State = EntityState.Detached;
    }

    public async Task UpdateAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);

        await DbContext.SaveChangesAsync();

        DbContext.Entry(entity).State = EntityState.Detached;
    }

    public async Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);

        await DbContext.SaveChangesAsync();
    }
}