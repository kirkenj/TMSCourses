using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarUpdateService : IUpdateService<CarUpdateModel>
    {
        public CarUpdateService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Update(CarUpdateModel item)
        {
            var car = _context.Cars.Single(c => c.Id == item.Id);
            car.ClientId = item.NewClientId;
            car.CarType = item.NewCarType;
            car.RegNumber = item.NewRegNumber;
            _context.SaveChanges();
        }
    }
}
