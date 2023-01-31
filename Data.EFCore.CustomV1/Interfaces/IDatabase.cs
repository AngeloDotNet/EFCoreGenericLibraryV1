namespace Data.EFCore.CustomV1.Interfaces;

public interface IDatabase<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}