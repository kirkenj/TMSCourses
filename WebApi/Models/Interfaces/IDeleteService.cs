namespace WebApi.Models.Interfaces
{
    public interface IDeleteService<T>
    {
        public Task DeleteAsync(T item);
    }
}
