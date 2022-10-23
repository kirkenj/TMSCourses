using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingPlacesController : ControllerBase
    {
        readonly IDeleteService<int> _deleteService;
        readonly IUpdateService<ParkingPlaceUpdateModel> _updateService;
        readonly IGetService<ParkingPlaceItemModel> _getService;
        readonly ICreateService<int> _createService;

        public ParkingPlacesController(IDeleteService<int> deleteService, IUpdateService<ParkingPlaceUpdateModel> updateService, IGetService<ParkingPlaceItemModel> getService, ICreateService<int> createService)
        {
            _deleteService = deleteService;
            _updateService = updateService;
            _getService = getService;
            _createService = createService;
        }

        // GET: api/ParkingPlaces
        [HttpGet]
        public IEnumerable<ParkingPlaceItemModel> GetParkingPlaces()
        {
            return _getService.GetAll();
        }

        // GET: api/ParkingPlaces/5
        [HttpGet("{id}")]
        public ActionResult<ParkingPlaceItemModel> GetParkingPlace(int id)
        {
            var place = _getService.GetFirst(c => c.Id == id);
            if (place == null) 
            {
                return NotFound();
            }

            return new ParkingPlaceItemModel { Id = place.Id, CarType = place.CarType };
        }

        // PUT: api/ParkingPlaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult PutParkingPlace(ParkingPlaceUpdateModel updateModel)
        {
            _updateService.Update(updateModel);
            return Ok();
        }

        // POST: api/ParkingPlaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<int> PostParkingPlace(int newParkingPlaceCarType)
        {
            _createService.Create(newParkingPlaceCarType);
            return CreatedAtAction("GetParkingPlace", new { type = newParkingPlaceCarType}, newParkingPlaceCarType);
        }

        // DELETE: api/ParkingPlaces/5
        [HttpDelete("{id}")]
        public IActionResult DeleteParkingPlace(int id)
        {
            _deleteService.Delete(id);
            return Ok();
        }
    }
}
