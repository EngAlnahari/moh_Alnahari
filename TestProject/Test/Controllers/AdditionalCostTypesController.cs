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
    public class AdditionalCostTypesController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public AdditionalCostTypesController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/AdditionalCostTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalCostType>>> GetAdditionalCostType()
        {
            return await _context.AdditionalCostType.ToListAsync();
        }
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<AdditionalCostType>> GetByName(string name)
        {
            var template = await _context.AdditionalCostType
                                         .FirstOrDefaultAsync(t => t.AdditionalCostTypeName == name);
            if (template == null) return NotFound();
            return template;
        }
        [HttpGet("GetAdditionalCostTemplate/{id}")]
        public async Task<IActionResult> GetAdditionalCostTemplate(int id)
        {
            var template = await _context.AdditionalCostType
                .FirstOrDefaultAsync(t => t.Id == id);

            if (template == null)
                return NotFound();

            // جلب التنقلات المرتبطة بهذا القالب
            template.additionalCostModels = await _context.AdditionalCostModels
                .Where(m => m.AdditionalCostTypeId == id)
                .ToListAsync();

            return Ok(template);
        }

        // GET: api/AdditionalCostTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalCostType>> GetAdditionalCostType(int id)
        {
            var additionalCostType = await _context.AdditionalCostType.FindAsync(id);

            if (additionalCostType == null)
            {
                return NotFound();
            }

            return additionalCostType;
        }

        // PUT: api/AdditionalCostTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalCostType(int id, AdditionalCostType additionalCostType)
        {
            if (id != additionalCostType.Id)
            {
                return BadRequest();
            }

            _context.Entry(additionalCostType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalCostTypeExists(id))
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

        // POST: api/AdditionalCostTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdditionalCostType>> PostAdditionalCostType(AdditionalCostType additionalCostType)
        {
            _context.AdditionalCostType.Add(additionalCostType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalCostType", new { id = additionalCostType.Id }, additionalCostType);
        }

        // DELETE: api/AdditionalCostTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdditionalCostType(int id)
        {
            var additionalCostType = await _context.AdditionalCostType.FindAsync(id);
            if (additionalCostType == null)
            {
                return NotFound();
            }

            _context.AdditionalCostType.Remove(additionalCostType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdditionalCostTypeExists(int id)
        {
            return _context.AdditionalCostType.Any(e => e.Id == id);
        }
    }
}
