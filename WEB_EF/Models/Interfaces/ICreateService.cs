namespace WEB_EF.Models.Interfaces
{
    public interface ICreateService<T>
    {
        public void Create(T item);
    }
}
