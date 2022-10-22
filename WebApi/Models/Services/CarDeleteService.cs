using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarDeleteService : IDeleteService<int>
    {
        public CarDeleteService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;
        public void Delete(int id)
        {
            var car = _context.Cars.Single(c => c.Id == id);
            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
    }
}
