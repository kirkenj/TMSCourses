namespace WEB_EF.Models.Interfaces
{
    public interface IValidateService<T>
    {
        public bool Validate(T entity, out string explanation);
    }
}
