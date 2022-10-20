using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDatabase.Interfaces;
using WebApiDatabase.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypesController : ControllerBase
    {
        private readonly IAutoparkDBContext _context;

        public CarTypesController(IAutoparkDBContext context)
        {
            _context = context;
        }

        // GET: api/CarTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarType>>> GetCarTypes()
        {
            return await _context.CarTypes.ToListAsync();
        }

        // GET: api/CarTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarType>> GetCarType(int id)
        {
            var carType = await _context.CarTypes.FindAsync(id);

            if (carType == null)
            {
                return NotFound();
            }

            return carType;
        }

        // PUT: api/CarTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType(int id, CarType carType)
        {
            if (id != carType.Id)
            {
                return BadRequest();
            }

            _context.Entry(carType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarType>> PostCarType(CarType carType)
        {
            _context.CarTypes.Add(carType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarType", new { id = carType.Id }, carType);
        }

        // DELETE: api/CarTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarType(int id)
        {
            var carType = await _context.CarTypes.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }

            _context.CarTypes.Remove(carType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarTypeExists(int id)
        {
            return _context.CarTypes.Any(e => e.Id == id);
        }
    }
}
