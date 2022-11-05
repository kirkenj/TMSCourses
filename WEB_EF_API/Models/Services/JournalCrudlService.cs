using WEB_EF.Models.Interfaces;
using WEB_EF.Models.Entities;

namespace WEB_EF.Models.Services
{
    public class JournalCrudlService : ICRUDlService<Journal>
    {
        public JournalCrudlService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public void Create(Journal item)
        {
            _context.Journals.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Journal item)
        {
            _context.Journals.Remove(item);
            _context.SaveChanges();
        }

        public List<Journal> GetAll()
        {
            return _context.Journals.ToList();
        }

        public Journal GetFirst()
        {
            return _context.Journals.First();
        }

        public void Update(Journal item)
        {
            _context.Journals.Update(item);
            _context.SaveChanges();
        }
        public IQueryable<Journal> GetViaIQueriable()
        {
            return _context.Journals;
        }
    }
}