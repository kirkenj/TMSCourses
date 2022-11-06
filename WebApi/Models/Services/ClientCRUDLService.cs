using WebApi.Models.Interfaces;
using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ClientCRUDLService : ICRUDLService<ClientCreateModel, ClientUpdateModel, int, ClientItemModel>
    {
        public ClientCRUDLService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(ClientCreateModel item)
        {
            var client = new Client()
            {
                Name = item.Name,
                Surname = item.Surname
            };

            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var client = _context.Clients.Single(c => c.Id == id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

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

        public void Update(ClientUpdateModel item)
        {
            var client = _context.Clients.Single(c => c.Id == item.Id);
            client.Name = item.NewName;
            client.Surname = item.NewSurname;
            _context.SaveChanges();
        }
    }
}
