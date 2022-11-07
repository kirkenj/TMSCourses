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

        public async Task CreateAsync(ClientCreateModel item)
        {
            var client = new Client()
            {
                Name = item.Name,
                Surname = item.Surname
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = _context.Clients.Single(c => c.Id == id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        private IQueryable<ClientItemModel> ItemModels => _context.Clients.Select(c => new ClientItemModel { Id = c.Id, Surname = c.Surname, Name = c.Name });

        public async Task<List<ClientItemModel>> GetAllAsync()
        {
            return await Task.Run(() => ItemModels.ToList());
        }

        public async Task<ClientItemModel>GetFirstAsync()
        {
            return await Task.Run(() => ItemModels.First());
        }

        public IQueryable<ClientItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public async Task<ClientItemModel?> GetFirstAsync(Func<ClientItemModel, bool> func)
        {
            return await Task.Run(() => ItemModels.FirstOrDefault(func));
        }

        public async Task UpdateAsync(ClientUpdateModel item)
        {
            var client = _context.Clients.Single(c => c.Id == item.Id);
            client.Name = item.NewName;
            client.Surname = item.NewSurname;
            await _context.SaveChangesAsync();
        }
    }
}
