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
    public class ParkingPlacesController : ControllerBase
    {
        private readonly IAutoparkDBContext _context;

        public ParkingPlacesController(IAutoparkDBContext context)
        {
            _context = context;
        }

        // GET: api/ParkingPlaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingPlace>>> GetParkingPlaces()
        {
            return await _context.ParkingPlaces.ToListAsync();
        }

        // GET: api/ParkingPlaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingPlace>> GetParkingPlace(int id)
        {
            var parkingPlace = await _context.ParkingPlaces.FindAsync(id);

            if (parkingPlace == null)
            {
                return NotFound();
            }

            return parkingPlace;
        }

        // PUT: api/ParkingPlaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingPlace(int id, ParkingPlace parkingPlace)
        {
            if (id != parkingPlace.Id)
            {
                return BadRequest();
            }

            _context.Entry(parkingPlace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingPlaceExists(id))
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

        // POST: api/ParkingPlaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParkingPlace>> PostParkingPlace(ParkingPlace parkingPlace)
        {
            _context.ParkingPlaces.Add(parkingPlace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParkingPlace", new { id = parkingPlace.Id }, parkingPlace);
        }

        // DELETE: api/ParkingPlaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingPlace(int id)
        {
            var parkingPlace = await _context.ParkingPlaces.FindAsync(id);
            if (parkingPlace == null)
            {
                return NotFound();
            }

            _context.ParkingPlaces.Remove(parkingPlace);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingPlaceExists(int id)
        {
            return _context.ParkingPlaces.Any(e => e.Id == id);
        }
    }
}
