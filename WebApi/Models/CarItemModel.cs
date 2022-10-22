using WebApiDatabase.Entities;

namespace WebApi.Models
{
    public class CarItemModel
    {
        public int Id { get; set; }
        public string RegNumber { get; set; } = null!;
        public int? ClientId { get; set; }
        public int CarType { get; set; }

        public static CarItemModel FromCarEntity(Car car)
        {
            return new CarItemModel { CarType = car.CarType, RegNumber = car.RegNumber, ClientId = car.ClientId, Id = car.Id };
        }
    }
}
