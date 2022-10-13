using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;
using WEB_EF.Models.Interfaces;

namespace WEB_EF.Models.Services
{
    public class CarCrudlService : ICRUDleService<Car>
    {
        public CarCrudlService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(Car item)
        {
            _context.Cars.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Car item)
        {
            _context.Cars.Remove(item);
            _context.SaveChanges();
        }

        public List<Car> GetAll()
        {
            return _context.Cars.ToList();
        }

        public Car GetFirst()
        {
            return _context.Cars.First();
        }

        public void Update(Car item)
        {
            _context.Cars.Update(item);
            _context.SaveChanges();
        }

        public IQueryable<Car> GetViaIQueriable()
        {
            return _context.Cars;
        }
    }
}
