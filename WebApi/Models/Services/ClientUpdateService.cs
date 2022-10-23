using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ClientUpdateService : IUpdateService<ClientUpdateModel>
    {
        public ClientUpdateService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Update(ClientUpdateModel item)
        {
            var client = _context.Clients.Single(c => c.Id == item.Id);
            client.Name = item.NewName;
            client.Surname = item.NewSurname;
            _context.SaveChanges();
        }
    }
}
