using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarTypeCreateService : ICreateService<CarTypeCreateModel>
    {
        public CarTypeCreateService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(CarTypeCreateModel item)
        {
            CarType type = new()
            {
                TypeName = item.TypeName
            };

            _context.CarTypes.Add(type);
            _context.SaveChanges();
        }
    }
}
