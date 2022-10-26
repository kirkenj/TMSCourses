using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;
using WebApi.Models;
using WebApi.Models.Interfaces;

namespace WebApi.Models.Services
{
    public class CarCRUDLService : ICRUDLService<CarCreateModel, CarUpdateModel, int, CarItemModel>
    {
        public CarCRUDLService(IAutoparkDBContext context)
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

        public void Delete(int id)
        {
            var car = _context.Cars.Single(c => c.Id == id);
            _context.Cars.Remove(car);
            _context.SaveChanges();
        }

        private IQueryable<CarItemModel> ItemModels => _context.Cars.Select(c => CarItemModel.FromCarEntity(c));

        public List<CarItemModel> GetAll()
        {
            return ItemModels.ToList();
        }

        public CarItemModel GetFirst()
        {
            return ItemModels.First();
        }

        public IQueryable<CarItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public CarItemModel? GetFirst(Func<CarItemModel, bool> func)
        {
            return ItemModels.FirstOrDefault(func);
        }

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
