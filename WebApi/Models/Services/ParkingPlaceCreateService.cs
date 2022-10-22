using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ParkingPlaceCreateService : ICreateService<int>
    {
        public ParkingPlaceCreateService(IAutoparkDBContext context)
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
    }
}
