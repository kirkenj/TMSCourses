using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ParkingPlaceUpdateService : IUpdateService<ParkingPlaceUpdateModel>
    {
        public ParkingPlaceUpdateService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Update(ParkingPlaceUpdateModel item)
        {
            var type = _context.ParkingPlaces.Single(c => c.Id == item.Id);
            type.CarType = item.NewCarType;
            _context.SaveChanges();
        }
    }
}
