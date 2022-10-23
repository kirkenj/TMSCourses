using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ClientCreateService : ICreateService<ClientCreateModel>
    {
        public ClientCreateService(IAutoparkDBContext context)
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
    }
}
