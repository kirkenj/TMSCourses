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
        public async Task<ActionResult<IEnumerable<CarTypeItemModel>>> GetCarTypes()
        {
            return await _getService.GetAllAsync();
        }

        // GET: api/CarTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarTypeItemModel?>> GetCarType(int id)
        {
            var car = await _getService.GetFirstAsync(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/CarTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCarType(CarTypeUpdateModel carType)
        {
            await _updateService.UpdateAsync(carType);
            return Ok();
        }

        // POST: api/CarTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarTypeCreateModel>> PostCarType(CarTypeCreateModel carType)
        {
            await _createService.CreateAsync(carType);
            return CreatedAtAction("GetCarType", carType);
        }

        // DELETE: api/CarTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarType(int id)
        {
            await _deleteService.DeleteAsync(id);
            return Ok();
        }
    }
}
