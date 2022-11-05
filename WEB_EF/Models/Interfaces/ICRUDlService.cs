namespace WebApi.Models.Interfaces
{
    public interface ICRUDlService<T> : ICreateService<T>, IDeleteService<T>, IUpdateService<T>, IGetService<T>
    {
    }
}
