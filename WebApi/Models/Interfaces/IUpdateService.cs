namespace WebApi.Models.Interfaces
{
    public interface IUpdateService<T>
    {
        public void Update(T item);
    }
}
