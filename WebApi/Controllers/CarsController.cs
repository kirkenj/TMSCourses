using Microsoft.AspNetCore.Mvc;
using WebApiDatabase.Entities;
using WebApi.Models.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IGetService<Car, CarItemModel> _getService;
        private readonly IUpdateService<CarUpdateModel> _updateService;
        private readonly IDeleteService<int> _deleteService;
        private readonly ICreateService<CarCreateModel> _createService;

        public CarsController(IGetService<Car, CarItemModel> getService, IUpdateService<CarUpdateModel> updateService, IDeleteService<int> deleteService, ICreateService<CarCreateModel> createService)
        {
            _getService = getService;
            _createService = createService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        // GET: api/Cars1
        [HttpGet]
        public IEnumerable<CarItemModel> GetCars()
        {
            return _getService.GetAll();
        }

        // GET: api/Cars1/5
        [HttpGet("{id}")]
        public ActionResult<CarItemModel> GetCar(int id)
        {
            var car = _getService.GetFirst(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }


        // PUT: api/Cars1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult PutCar(CarUpdateModel car)
        {
            _updateService.Update(car);
            return Ok();
        }

        // POST: api/Cars1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCar(CarCreateModel car)
        {
            _createService.Create(car);
            return CreatedAtAction(nameof(PutCar), car);
        }

        // DELETE: api/Cars1/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            _deleteService.Delete(id);
            return Ok();
        }
    }
}
