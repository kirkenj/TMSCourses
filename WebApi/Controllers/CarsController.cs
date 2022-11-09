using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IGetService<CarItemModel> _getService;
        private readonly IUpdateService<CarUpdateModel> _updateService;
        private readonly IDeleteService<int> _deleteService;
        private readonly ICreateService<CarCreateModel> _createService;

        public CarsController(IGetService<CarItemModel> getService, IUpdateService<CarUpdateModel> updateService, IDeleteService<int> deleteService, ICreateService<CarCreateModel> createService)
        {
            _getService = getService;
            _createService = createService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        // GET: api/Cars1
        [HttpGet]
        public async Task<IEnumerable<CarItemModel>> GetCars()
        {
            return await _getService.GetAllAsync();
        }

        // GET: api/Cars1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarItemModel>> GetCar(int id)
        {
            var car = await _getService.GetFirstAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCar(CarUpdateModel car)
        {
            await _updateService.UpdateAsync(car);
            return Ok();
        }

        // POST: api/Cars1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCar(CarCreateModel car)
        {
            await _createService.CreateAsync(car);
            return CreatedAtAction(nameof(PutCar), car);
        }

        // DELETE: api/Cars1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _deleteService.DeleteAsync(id);
            return Ok();
        }
    }
}
