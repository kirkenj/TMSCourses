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
        public async Task<ActionResult<IEnumerable<JournalItemModel>>> GetJournals()
        {
            var journals = await _getService.GetAllAsync();
            return journals;
        }

        // GET: api/Journals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JournalItemModel>> GetJournal(int id)
        {
            var journal = await _getService.GetFirstAsync(j => j.Id == id);

            if (journal == null)
            {
                return NotFound();
            }

            return journal;
        }

        // PUT: api/Journals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutJournal(JournalUpdateModel journal)
        {
            await _updateService.UpdateAsync(journal);
            return Ok();
        }

        // POST: api/Journals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JournalCreateModel>> PostJournal(JournalCreateModel model)
        {
            await _createService.CreateAsync(model);
            return CreatedAtAction("GetJournal", model);
        }

        // DELETE: api/Journals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJournal(int id)
        {
            await _deleteService.DeleteAsync(id);
            return Ok();
        }
    }
}
