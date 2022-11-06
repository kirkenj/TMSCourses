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

        public async Task CreateAsync(int parkingPlaceCarType)
        {
            ParkingPlace type = new()
            {
                CarType = parkingPlaceCarType
            };

            _context.ParkingPlaces.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var type = _context.ParkingPlaces.Single(c => c.Id == id);
            _context.ParkingPlaces.Remove(type);
            await _context.SaveChangesAsync();
        }

        private IQueryable<ParkingPlaceItemModel> ItemModels => _context.ParkingPlaces.Select(c => new ParkingPlaceItemModel { Id = c.Id, CarType = c.CarType });

        public async Task<List<ParkingPlaceItemModel>> GetAllAsync()
        {
            return await Task.Run(() => ItemModels.ToList());
        }

        public async Task<ParkingPlaceItemModel> GetFirstAsync()
        {
            return await Task.Run(()=> ItemModels.First());
        }

        public IQueryable<ParkingPlaceItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public async Task<ParkingPlaceItemModel?> GetFirstAsync(Func<ParkingPlaceItemModel, bool> func)
        {
            return await Task.Run(() => ItemModels.FirstOrDefault(func));
        }

        public async Task UpdateAsync(ParkingPlaceUpdateModel item)
        {
            var type = _context.ParkingPlaces.Single(c => c.Id == item.Id);
            type.CarType = item.NewCarType;
            await _context.SaveChangesAsync();
        }
    }
}
