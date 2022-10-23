using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ParkingPlaceGetService : IGetService<ParkingPlaceItemModel>
    {
        public ParkingPlaceGetService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;
        private IQueryable<ParkingPlaceItemModel> ItemModels => _context.ParkingPlaces.Select(c => new ParkingPlaceItemModel { Id = c.Id, CarType = c.CarType });

        public List<ParkingPlaceItemModel> GetAll()
        {
            return ItemModels.ToList();
        }

        public ParkingPlaceItemModel GetFirst()
        {
            return ItemModels.First();
        }

        public IQueryable<ParkingPlaceItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public ParkingPlaceItemModel? GetFirst(Func<ParkingPlaceItemModel, bool> func)
        {
            return ItemModels.FirstOrDefault(func);
        }
    }
}
