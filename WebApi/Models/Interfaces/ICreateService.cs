namespace WebApi.Models.Interfaces
{
    public interface ICreateService<T>
    {
        public Task CreateAsync(T item);
    }
}
