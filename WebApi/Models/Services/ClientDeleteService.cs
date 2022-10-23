using WebApi.Models.Interfaces;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class ClientDeleteService : IDeleteService<int>
    {
        public ClientDeleteService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;
        public void Delete(int id)
        {
            var client = _context.Clients.Single(c => c.Id == id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }
    }
}
