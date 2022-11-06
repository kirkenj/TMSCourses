namespace WebApi.Models.Interfaces
{
    public interface IDeleteService<T>
    {
        public void Delete(T item);
    }
}
