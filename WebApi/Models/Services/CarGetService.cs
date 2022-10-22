using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarGetService : IGetService2<Car, CarItemModel>
    {
        public CarGetService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public List<CarItemModel> GetAll()
        {
            return _context.Cars.Select(c=>CarItemModel.FromCarEntity(c)).ToList();
        }

        public CarItemModel GetFirst()
        {
            return CarItemModel.FromCarEntity(_context.Cars.First());
        }

        public IQueryable<Car> GetViaIQueriable()
        {
            return _context.Cars;
        }

        public CarItemModel? GetFirst(Func<Car, bool> func)
        {
            var car = _context.Cars.FirstOrDefault(func);
            return car == null ? null : CarItemModel.FromCarEntity(car);

        }
    }
}
