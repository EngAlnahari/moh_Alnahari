using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceTypesController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public PlaceTypesController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/PlaceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceType>>> GetPlaceType()
        {
            return await _context.PlaceType.ToListAsync();
        }

        // GET: api/PlaceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceType>> GetPlaceType(int id)
        {
            var placeType = await _context.PlaceType.FindAsync(id);

            if (placeType == null)
            {
                return NotFound();
            }

            return placeType;
        }

        // PUT: api/PlaceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceType(int id, PlaceType placeType)
        {
            if (id != placeType.Id)
            {
                return BadRequest();
            }

            _context.Entry(placeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceTypeExists(id))
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

        // POST: api/PlaceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlaceType>> PostPlaceType(PlaceType placeType)
        {
            _context.PlaceType.Add(placeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaceType", new { id = placeType.Id }, placeType);
        }

        // DELETE: api/PlaceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaceType(int id)
        {
            var placeType = await _context.PlaceType.FindAsync(id);
            if (placeType == null)
            {
                return NotFound();
            }

            _context.PlaceType.Remove(placeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaceTypeExists(int id)
        {
            return _context.PlaceType.Any(e => e.Id == id);
        }
    }
}
