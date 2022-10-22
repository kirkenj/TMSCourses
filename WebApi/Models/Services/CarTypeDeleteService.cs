using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarTypeDeleteService : IDeleteService<int>
    {
        public CarTypeDeleteService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;
        public void Delete(int id)
        {
            var type = _context.CarTypes.Single(c => c.Id == id);
            _context.CarTypes.Remove(type);
            _context.SaveChanges();
        }
    }
}
