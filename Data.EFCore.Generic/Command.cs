using Data.EFCore.Generic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.EFCore.Generic;

public class Command<T> : ICommand<T> where T : class
{
    public DbContext DbContext { get; }

    public Command(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<bool> CreateAsync(T entity)
    {
        DbContext.Set<T>().Add(entity);

        var result = await DbContext.SaveChangesAsync();

        DbContext.Entry(entity).State = EntityState.Detached;

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);

        var result = await DbContext.SaveChangesAsync();

        DbContext.Entry(entity).State = EntityState.Detached;

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);

        var result = await DbContext.SaveChangesAsync();

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}