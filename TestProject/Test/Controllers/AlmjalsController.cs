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
    public class AlmjalsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public AlmjalsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/Almjals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Almjal>>> GetAlmjals()
        {
            return await _context.Almjals.ToListAsync();
        }

        // GET: api/Almjals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Almjal>> GetAlmjal(int id)
        {
            var almjal = await _context.Almjals.FindAsync(id);

            if (almjal == null)
            {
                return NotFound();
            }

            return almjal;
        }

        // PUT: api/Almjals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlmjal(int id, Almjal almjal)
        {
            if (id != almjal.ID)
            {
                return BadRequest();
            }

            _context.Entry(almjal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlmjalExists(id))
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

        // POST: api/Almjals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Almjal>> PostAlmjal(Almjal almjal)
        {
            _context.Almjals.Add(almjal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlmjal", new { id = almjal.ID }, almjal);
        }

        // DELETE: api/Almjals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlmjal(int id)
        {
            var almjal = await _context.Almjals.FindAsync(id);
            if (almjal == null)
            {
                return NotFound();
            }

            _context.Almjals.Remove(almjal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlmjalExists(int id)
        {
            return _context.Almjals.Any(e => e.ID == id);
        }
    }
}
