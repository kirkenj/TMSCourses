namespace WebApi.Models.Interfaces
{
    public interface IUpdateService<T>
    {
        public Task UpdateAsync(T item);
    }
}
