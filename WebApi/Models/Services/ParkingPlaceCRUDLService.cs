using WebApi.Models.Interfaces;
using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ParkingPlaceCRUDLService : ICRUDLService<int, ParkingPlaceUpdateModel, int, ParkingPlaceItemModel>
    {
        public ParkingPlaceCRUDLService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(int parkingPlaceCarType)
        {
            ParkingPlace type = new()
            {
                CarType = parkingPlaceCarType
            };

            _context.ParkingPlaces.Add(type);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var type = _context.ParkingPlaces.Single(c => c.Id == id);
            _context.ParkingPlaces.Remove(type);
            _context.SaveChanges();
        }

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

        public void Update(ParkingPlaceUpdateModel item)
        {
            var type = _context.ParkingPlaces.Single(c => c.Id == item.Id);
            type.CarType = item.NewCarType;
            _context.SaveChanges();
        }
    }
}
