namespace WebApi.Models.Interfaces
{
    public interface IGetService<TModel>
    {
        public List<TModel> GetAll();
        public IQueryable<TModel> GetViaIQueriable();
        public TModel GetFirst();
        public TModel? GetFirst(Func<TModel, bool> func);
    }
}
