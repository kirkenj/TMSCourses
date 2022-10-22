using WebApiDatabase.Entities;

namespace WebApi.Models
{
    public class CarTypeItemModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;


        public static CarTypeItemModel FromCarEntity(CarType type)
        {
            return new CarTypeItemModel { Id = type.Id,  TypeName = type.TypeName};
        }
    }
}
