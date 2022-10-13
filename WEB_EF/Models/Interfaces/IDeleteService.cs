namespace WEB_EF.Models.Interfaces
{
    public interface IDeleteService<T>
    {
        public void Delete(T item);
    }
}
