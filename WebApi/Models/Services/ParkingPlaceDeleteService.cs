using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ParkingPlaceDeleteService : IDeleteService<int>
    {
        public ParkingPlaceDeleteService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;
        public void Delete(int id)
        {
            var type = _context.ParkingPlaces.Single(c => c.Id == id);
            _context.ParkingPlaces.Remove(type);
            _context.SaveChanges();
        }
    }
}
