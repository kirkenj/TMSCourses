namespace WebApi.Models.Interfaces
{
    public interface ICRUDLService<TCreateModel, IUpdateModel, TDeleteModel, TItemModel> : ICreateService<TCreateModel>, IUpdateService<IUpdateModel>, IDeleteService<TDeleteModel>, IGetService<TItemModel>
    {
    }
}
