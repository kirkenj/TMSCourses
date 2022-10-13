namespace WEB_EF.Models.Interfaces
{
    public interface ICRUDleService<T> : ICreateService<T>, IDeleteService<T>, IUpdateService<T>, IGetService<T>
    {
    }
}
