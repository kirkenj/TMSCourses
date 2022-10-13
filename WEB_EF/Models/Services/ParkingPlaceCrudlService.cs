using WEB_EF.Models.Interfaces;
using WEB_EF.Models.Classes;

namespace WEB_EF.Models.Services
{
    public class ParkingPlaceCrudlService : ICRUDleService<ParkingPlace>
    {
        public ParkingPlaceCrudlService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(ParkingPlace item)
        {
            _context.ParkingPlaces.Add(item);
            _context.SaveChanges();
        }

        public void Delete(ParkingPlace item)
        {
            _context.ParkingPlaces.Remove(item);
            _context.SaveChanges();
        }

        public List<ParkingPlace> GetAll()
        {
            return _context.ParkingPlaces.ToList();
        }

        public ParkingPlace GetFirst()
        {
            return _context.ParkingPlaces.First();
        }

        public void Update(ParkingPlace item)
        {
            _context.ParkingPlaces.Update(item);
            _context.SaveChanges();
        }

        public IQueryable<ParkingPlace> GetViaIQueriable()
        {
            return _context.ParkingPlaces;
        }
    }
}
