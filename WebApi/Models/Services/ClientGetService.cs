using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ClientGetService : IGetService<ClientItemModel>
    {
        public ClientGetService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        private IQueryable<ClientItemModel> ItemModels => _context.Clients.Select(c => new ClientItemModel { Id = c.Id, Surname = c.Surname, Name = c.Name });

        public List<ClientItemModel> GetAll()
        {
            return ItemModels.ToList();
        }

        public ClientItemModel GetFirst()
        {
            return ItemModels.First();
        }

        public IQueryable<ClientItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public ClientItemModel? GetFirst(Func<ClientItemModel, bool> func)
        {
            return ItemModels.FirstOrDefault(func);
        }
    }
}
