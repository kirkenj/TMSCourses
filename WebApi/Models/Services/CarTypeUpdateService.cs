using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class CarTypeUpdateService : IUpdateService<CarTypeUpdateModel>
    {
        public CarTypeUpdateService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Update(CarTypeUpdateModel item)
        {
            var type = _context.CarTypes.Single(c => c.Id == item.Id);
            type.TypeName = item.NewTypeName;
            _context.SaveChanges();
        }
    }
}
