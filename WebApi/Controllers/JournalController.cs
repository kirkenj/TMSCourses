using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly ICRUDLService<JournalCreateModel, JournalUpdateModel, int, JournalItemModel> _service;

        public JournalController(ICRUDLService<JournalCreateModel, JournalUpdateModel, int, JournalItemModel> context)
        {
            _service = context;
        }

        // GET: api/Journals
        [HttpGet]
        public ActionResult<IEnumerable<JournalItemModel>> GetJournals()
        {
            return _service.GetAll();
        }

        // GET: api/Journals/5
        [HttpGet("{id}")]
        public ActionResult<JournalItemModel> GetJournal(int id)
        {
            var journal = _service.GetFirst(j => j.Id == id);

            if (journal == null)
            {
                return NotFound();
            }

            return journal;
        }

        // PUT: api/Journals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult PutJournal(JournalUpdateModel journal)
        {
            _service.Update(journal);
            return Ok();
        }

        // POST: api/Journals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<JournalCreateModel> PostJournal(JournalCreateModel model)
        {
            _service.Create(model);
            return CreatedAtAction("GetJournal", model);
        }

        // DELETE: api/Journals/5
        [HttpDelete("{id}")]
        public IActionResult DeleteJournal(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
