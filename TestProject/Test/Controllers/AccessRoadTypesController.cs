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
    public class AccessRoadTypesController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public AccessRoadTypesController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/AccessRoadTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessRoadType>>> GetAccessRoadType()
        {
            return await _context.AccessRoadType.ToListAsync();
        }

        // GET: api/AccessRoadTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessRoadType>> GetAccessRoadType(int id)
        {
            var accessRoadType = await _context.AccessRoadType.FindAsync(id);

            if (accessRoadType == null)
            {
                return NotFound();
            }

            return accessRoadType;
        }

        // PUT: api/AccessRoadTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessRoadType(int id, AccessRoadType accessRoadType)
        {
            if (id != accessRoadType.Id)
            {
                return BadRequest();
            }

            _context.Entry(accessRoadType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessRoadTypeExists(id))
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

        // POST: api/AccessRoadTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccessRoadType>> PostAccessRoadType(AccessRoadType accessRoadType)
        {
            _context.AccessRoadType.Add(accessRoadType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessRoadType", new { id = accessRoadType.Id }, accessRoadType);
        }

        // DELETE: api/AccessRoadTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessRoadType(int id)
        {
            var accessRoadType = await _context.AccessRoadType.FindAsync(id);
            if (accessRoadType == null)
            {
                return NotFound();
            }

            _context.AccessRoadType.Remove(accessRoadType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessRoadTypeExists(int id)
        {
            return _context.AccessRoadType.Any(e => e.Id == id);
        }
    }
}
