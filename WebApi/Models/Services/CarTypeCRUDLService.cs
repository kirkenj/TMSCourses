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

        public async Task CreateAsync(CarTypeCreateModel item)
        {
            CarType type = new()
            {
                TypeName = item.TypeName
            };

            _context.CarTypes.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var type = _context.CarTypes.Single(c => c.Id == id);
            _context.CarTypes.Remove(type);
            await _context.SaveChangesAsync();
        }

        private IQueryable<CarTypeItemModel> ItemModels => _context.CarTypes.Select(c => new CarTypeItemModel { Id = c.Id, TypeName = c.TypeName });

        public async Task<List<CarTypeItemModel>> GetAllAsync()
        {
            return await Task.Run(() => ItemModels.ToList());
        }

        public async Task<CarTypeItemModel> GetFirstAsync()
        {
            return await Task.Run(() => ItemModels.First());
        }

        public IQueryable<CarTypeItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public async Task<CarTypeItemModel?> GetFirstAsync(Func<CarTypeItemModel, bool> func)
        {
            return await Task.Run(() => ItemModels.FirstOrDefault(func));
        }

        public async Task UpdateAsync(CarTypeUpdateModel item)
        {
            var type = _context.CarTypes.Single(c => c.Id == item.Id);
            type.TypeName = item.NewTypeName;
            await _context.SaveChangesAsync();
        }
    }
}
