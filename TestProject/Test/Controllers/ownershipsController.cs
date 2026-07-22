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
    public class ownershipsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public ownershipsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/ownerships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ownership>>> Getownerships()
        {
            return await _context.ownerships.ToListAsync();
        }

        // GET: api/ownerships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ownership>> Getownership(int id)
        {
            var ownership = await _context.ownerships.FindAsync(id);

            if (ownership == null)
            {
                return NotFound();
            }

            return ownership;
        }

        // PUT: api/ownerships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putownership(int id, ownership ownership)
        {
            if (id != ownership.Id)
            {
                return BadRequest();
            }

            _context.Entry(ownership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ownershipExists(id))
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

        // POST: api/ownerships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ownership>> Postownership(ownership ownership)
        {
            _context.ownerships.Add(ownership);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getownership", new { id = ownership.Id }, ownership);
        }

        // DELETE: api/ownerships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteownership(int id)
        {
            var ownership = await _context.ownerships.FindAsync(id);
            if (ownership == null)
            {
                return NotFound();
            }

            _context.ownerships.Remove(ownership);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ownershipExists(int id)
        {
            return _context.ownerships.Any(e => e.Id == id);
        }
    }
}
