namespace WebApi.Models.Interfaces
{
    public interface IGetService<TModel>
    {
        public Task<List<TModel>> GetAllAsync();
        public IQueryable<TModel> GetViaIQueriable();
        public Task<TModel> GetFirstAsync();
        public Task<TModel?> GetFirstAsync(Func<TModel, bool> func);
    }
}
