using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IGetService<ClientItemModel> _getService;
        private readonly IUpdateService<ClientUpdateModel> _updateService;
        private readonly IDeleteService<int> _deleteService;
        private readonly ICreateService<ClientCreateModel> _createService;

        public ClientsController(IGetService<ClientItemModel> getService, IUpdateService<ClientUpdateModel> updateService, IDeleteService<int> deleteService, ICreateService<ClientCreateModel> createService)
        {
            _getService = getService;
            _createService = createService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientItemModel>>> GetClients()
        {
            return await _getService.GetAllAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientItemModel>> GetClient(int id)
        {
            var client = await _getService.GetFirstAsync(i => i.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutClient(ClientUpdateModel client)
        {
            await _updateService.UpdateAsync(client);
            return Ok();
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientCreateModel>> PostClient(ClientCreateModel client)
        {
            await _createService.CreateAsync(client);
            return CreatedAtAction("PostClient", client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _deleteService.DeleteAsync(id);
            return Ok();
        }
    }
}
