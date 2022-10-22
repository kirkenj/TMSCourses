using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ParkingPlaceGetService : IGetService<ParkingPlace, ParkingPlaceItemModel>
    {
        public ParkingPlaceGetService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public List<ParkingPlaceItemModel> GetAll()
        {
            return _context.ParkingPlaces.Select(c=> new ParkingPlaceItemModel { Id = c.Id, CarType = c.CarType}).ToList();
        }

        public ParkingPlaceItemModel GetFirst()
        {
            var place = _context.ParkingPlaces.First();
            return new ParkingPlaceItemModel { Id = place.Id, CarType = place.CarType };
        }

        public IQueryable<ParkingPlace> GetViaIQueriable()
        {
            return _context.ParkingPlaces;
        }

        public ParkingPlaceItemModel? GetFirst(Func<ParkingPlace, bool> func)
        {
            var place = _context.ParkingPlaces.FirstOrDefault(func);
            return place == null ? null : new ParkingPlaceItemModel { Id = place.Id, CarType = place.CarType };

        }
    }
}
