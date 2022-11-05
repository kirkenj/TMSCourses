using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IGetService<JournalItemModel> _getService;
        private readonly IUpdateService<JournalUpdateModel> _updateService;
        private readonly IDeleteService<int> _deleteService;
        private readonly ICreateService<JournalCreateModel> _createService;

        public JournalController(IGetService<JournalItemModel> getService, IUpdateService<JournalUpdateModel> updateService, IDeleteService<int> deleteService, ICreateService<JournalCreateModel> createService)
        {
            _createService = createService;
            _getService = getService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        // GET: api/Journals
        [HttpGet]
        public ActionResult<IEnumerable<JournalItemModel>> GetJournals()
        {
            return _getService.GetAll();
        }

        // GET: api/Journals/5
        [HttpGet("{id}")]
        public ActionResult<JournalItemModel> GetJournal(int id)
        {
            var journal = _getService.GetFirst(j => j.Id == id);

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
            _updateService.Update(journal);
            return Ok();
        }

        // POST: api/Journals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<JournalCreateModel> PostJournal(JournalCreateModel model)
        {
            _createService.Create(model);
            return CreatedAtAction("GetJournal", model);
        }

        // DELETE: api/Journals/5
        [HttpDelete("{id}")]
        public IActionResult DeleteJournal(int id)
        {
            _deleteService.Delete(id);
            return Ok();
        }
    }
}
