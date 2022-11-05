namespace WebApi.Models.Interfaces
{
    public interface IGetService<T>
    {
        public List<T> GetAll();
        public IQueryable<T> GetViaIQueriable();
        public T GetFirst();
    }
}
