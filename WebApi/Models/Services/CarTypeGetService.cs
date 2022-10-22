using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarTypeGetService : IGetService2<CarType, CarTypeItemModel>
    {
        public CarTypeGetService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public List<CarTypeItemModel> GetAll()
        {
            return _context.CarTypes.Select(c=> new CarTypeItemModel { Id = c.Id, TypeName = c.TypeName}).ToList();
        }

        public CarTypeItemModel GetFirst()
        {
            var type = _context.CarTypes.First();
            return new CarTypeItemModel { Id = type.Id, TypeName = type.TypeName };
        }

        public IQueryable<CarType> GetViaIQueriable()
        {
            return _context.CarTypes;
        }

        public CarTypeItemModel? GetFirst(Func<CarType, bool> func)
        {
            var type = _context.CarTypes.FirstOrDefault(func);
            return type == null ? null : new CarTypeItemModel { Id = type.Id, TypeName = type.TypeName };

        }
    }
}
