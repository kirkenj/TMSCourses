using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;
using WebApi.Models.Interfaces;

namespace WebApi.Models.Services
{
    public class CarTypeCrudlService : ICRUDlService<CarType>
    {
        public CarTypeCrudlService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(CarType item)
        {
            _context.CarTypes.Add(item);
            _context.SaveChanges();
        }

        public void Delete(CarType item)
        {
            _context.CarTypes.Remove(item);
            _context.SaveChanges();
        }

        public List<CarType> GetAll()
        {
            return _context.CarTypes.ToList();
        }

        public CarType GetFirst()
        {
            return _context.CarTypes.First();
        }

        public void Update(CarType item)
        {
            _context.CarTypes.Update(item);
            _context.SaveChanges();
        }

        public IQueryable<CarType> GetViaIQueriable()
        {
            return _context.CarTypes;
        }
    }
}
