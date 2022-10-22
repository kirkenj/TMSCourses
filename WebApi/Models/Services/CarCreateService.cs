using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarCreateService : ICreateService<CarCreateModel>
    {
        public CarCreateService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(CarCreateModel item)
        {
            var car = new Car()
            {
                CarType = item.CarType,
                ClientId = item.ClientId,
                RegNumber = item.RegNumber,
            };

            _context.Cars.Add(car);
            _context.SaveChanges();
        }
    }
}
