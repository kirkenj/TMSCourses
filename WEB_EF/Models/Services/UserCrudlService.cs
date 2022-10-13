using WEB_EF.Models.Classes;
using WEB_EF.Models.Interfaces;

namespace WEB_EF.Models.Services
{
    public class ClientCrudlService : ICRUDlService<Client>
    {
        public ClientCrudlService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(Client item)
        {
            _context.Clients.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Client item)
        {
            _context.Clients.Remove(item);
            _context.SaveChanges();
        }

        public List<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public Client GetFirst()
        {
            return _context.Clients.First();
        }

        public void Update(Client item)
        {
            _context.Clients.Update(item);
            _context.SaveChanges();
        }
        public IQueryable<Client> GetViaIQueriable()
        {
            return _context.Clients;
        }
    }
}
