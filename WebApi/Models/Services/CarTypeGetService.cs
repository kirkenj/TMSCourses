using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarTypeGetService : IGetService<CarTypeItemModel>
    {
        public CarTypeGetService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;
        private IQueryable<CarTypeItemModel> ItemModels => _context.CarTypes.Select(c => new CarTypeItemModel { Id = c.Id, TypeName = c.TypeName });

        public List<CarTypeItemModel> GetAll()
        {
            return ItemModels.ToList();
        }

        public CarTypeItemModel GetFirst()
        {
            return ItemModels.First();
        }

        public IQueryable<CarTypeItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public CarTypeItemModel? GetFirst(Func<CarTypeItemModel, bool> func)
        {
            return ItemModels.FirstOrDefault(func);
        }
    }
}
