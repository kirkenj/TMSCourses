using WebApi.Models.Interfaces;
using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarTypeCRUDLService : ICRUDLService<CarTypeCreateModel, CarTypeUpdateModel, int, CarTypeItemModel>
    {
        public CarTypeCRUDLService(IAutoparkDBContext context)
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

        public void Delete(int id)
        {
            var type = _context.CarTypes.Single(c => c.Id == id);
            _context.CarTypes.Remove(type);
            _context.SaveChanges();
        }

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

        public void Update(CarTypeUpdateModel item)
        {
            var type = _context.CarTypes.Single(c => c.Id == item.Id);
            type.TypeName = item.NewTypeName;
            _context.SaveChanges();
        }
    }
}
