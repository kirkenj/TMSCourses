namespace WebApi.Models.Interfaces
{
    public interface IGetService2<Tentity, TModel>
        where TModel : class
    {
        public List<TModel> GetAll();
        public IQueryable<Tentity> GetViaIQueriable();
        public TModel GetFirst();
        public TModel? GetFirst(Func<Tentity, bool> func);
    }
}
