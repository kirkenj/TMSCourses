using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;
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

        public async Task CreateAsync(CarCreateModel item)
        {
            var car = new Car()
            {
                CarType = item.CarType,
                ClientId = item.ClientId,
                RegNumber = item.RegNumber,
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var car = _context.Cars.Single(c => c.Id == id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        private IQueryable<CarItemModel> ItemModels => _context.Cars.Select(c => CarItemModel.FromCarEntity(c));

        public async Task<List<CarItemModel>> GetAllAsync()
        {
            return await Task.Run(()=> ItemModels.ToList());
        }

        public async Task<CarItemModel> GetFirstAsync()
        {
            return await Task.Run(() => ItemModels.First());
        }

        public IQueryable<CarItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public async Task<CarItemModel?> GetFirstAsync(Func<CarItemModel, bool> func)
        {
            return await Task.Run(() => ItemModels.FirstOrDefault(func));
        }

        public async Task UpdateAsync(CarUpdateModel item)
        {
            var car = _context.Cars.Single(c => c.Id == item.Id);
            car.ClientId = item.NewClientId;
            car.CarType = item.NewCarType;
            car.RegNumber = item.NewRegNumber;
            await _context.SaveChangesAsync();
        }
    }
}
