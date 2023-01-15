namespace Data.EFCore.Generic.Interfaces;

public interface ICommand<T> where T : class
{
    Task<bool> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}