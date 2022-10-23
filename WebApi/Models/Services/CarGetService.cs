using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarGetService : IGetService<CarItemModel>
    {
        public CarGetService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;
        private IQueryable<CarItemModel> ItemModels => _context.Cars.Select(c => CarItemModel.FromCarEntity(c));

        public List<CarItemModel> GetAll()
        {
            return ItemModels.ToList();
        }

        public CarItemModel GetFirst()
        {
            return ItemModels.First();
        }

        public IQueryable<CarItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public CarItemModel? GetFirst(Func<CarItemModel, bool> func)
        {
            return ItemModels.FirstOrDefault(func);
        }
    }
}
