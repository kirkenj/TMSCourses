using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingPlacesController : ControllerBase
    {
        private readonly IDeleteService<int> _deleteService;
        private readonly IUpdateService<ParkingPlaceUpdateModel> _updateService;
        private readonly IGetService<ParkingPlaceItemModel> _getService;
        private readonly ICreateService<int> _createService;

        public ParkingPlacesController(IDeleteService<int> deleteService, IUpdateService<ParkingPlaceUpdateModel> updateService, IGetService<ParkingPlaceItemModel> getService, ICreateService<int> createService)
        {
            _deleteService = deleteService;
            _updateService = updateService;
            _getService = getService;
            _createService = createService;
        }

        // GET: api/ParkingPlaces
        [HttpGet]
        public async Task<IEnumerable<ParkingPlaceItemModel>> GetParkingPlaces()
        {
            return await _getService.GetAllAsync();
        }

        // GET: api/ParkingPlaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingPlaceItemModel>> GetParkingPlace(int id)
        {
            var place = await _getService.GetFirstAsync(c => c.Id == id);
            if (place == null) 
            {
                return NotFound();
            }

            return place;
        }

        // PUT: api/ParkingPlaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutParkingPlace(ParkingPlaceUpdateModel updateModel)
        {
            await _updateService.UpdateAsync(updateModel);
            return Ok();
        }

        // POST: api/ParkingPlaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<int>> PostParkingPlace(int newParkingPlaceCarType)
        {
            await _createService.CreateAsync(newParkingPlaceCarType);
            return CreatedAtAction("GetParkingPlace", new { type = newParkingPlaceCarType}, newParkingPlaceCarType);
        }

        // DELETE: api/ParkingPlaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingPlace(int id)
        {
            await _deleteService.DeleteAsync(id);
            return Ok();
        }
    }
}
