public interface IGodsRepository
{
    Task CreateAsync(God entity);
    Task<IReadOnlyCollection<God>> GetAllAsync();
    Task<God> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(God entity);
}
