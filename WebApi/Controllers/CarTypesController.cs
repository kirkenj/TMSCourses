using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypesController : ControllerBase
    {
        private readonly IGetService<CarTypeItemModel> _getService;
        private readonly IUpdateService<CarTypeUpdateModel> _updateService;
        private readonly IDeleteService<int> _deleteService;
        private readonly ICreateService<CarTypeCreateModel> _createService;

        public CarTypesController(IGetService<CarTypeItemModel> getService, IUpdateService<CarTypeUpdateModel> updateService, IDeleteService<int> deleteService, ICreateService<CarTypeCreateModel> createService)
        {
            _getService = getService;
            _createService = createService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        // GET: api/CarTypes
        [HttpGet]
        public ActionResult<IEnumerable<CarTypeItemModel>> GetCarTypes()
        {
            return _getService.GetAll();
        }

        // GET: api/CarTypes/5
        [HttpGet("{id}")]
        public ActionResult<CarTypeItemModel?> GetCarType(int id)
        {
            var car = _getService.GetFirst(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/CarTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult PutCarType(CarTypeUpdateModel carType)
        {
            _updateService.Update(carType);
            return Ok();
        }

        // POST: api/CarTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<CarTypeCreateModel> PostCarType(CarTypeCreateModel carType)
        {
            _createService.Create(carType);
            return CreatedAtAction("GetCarType", carType);
        }

        // DELETE: api/CarTypes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCarType(int id)
        {
            _deleteService.Delete(id);
            return Ok();
        }
    }
}
