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
    public class SpecificSpaceTemplatesController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public SpecificSpaceTemplatesController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/SpecificSpaceTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecificSpaceTemplate>>> GetspecificSpaceTemplates()
        {
            return await _context.specificSpaceTemplates.ToListAsync();
        }

        // GET: api/SpecificSpaceTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecificSpaceTemplate>> GetSpecificSpaceTemplate(int id)
        {
            var specificSpaceTemplate = await _context.specificSpaceTemplates.FindAsync(id);

            if (specificSpaceTemplate == null)
            {
                return NotFound();
            }

            return specificSpaceTemplate;
        }

        // PUT: api/SpecificSpaceTemplates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecificSpaceTemplate(int id, SpecificSpaceTemplate specificSpaceTemplate)
        {
            if (id != specificSpaceTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(specificSpaceTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecificSpaceTemplateExists(id))
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

        // POST: api/SpecificSpaceTemplates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Single")]
        public async Task<ActionResult> PostSpecificSpaceTemplates(List<SpecificSpaceTemplate> specificSpaceTemplates)
        {
            _context.specificSpaceTemplates.AddRange(specificSpaceTemplates);
            await _context.SaveChangesAsync();
            return Ok(specificSpaceTemplates);
        }
        [HttpPost("Multiple")]
        public async Task<ActionResult<SpecificSpaceTemplate>> PostSpecificSpaceTemplate(List<SpecificSpaceTemplate> earthBorders)
        {
            _context.specificSpaceTemplates.AddRange(earthBorders);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/SpecificSpaceTemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecificSpaceTemplate(int id)
        {
            var specificSpaceTemplate = await _context.specificSpaceTemplates.FindAsync(id);
            if (specificSpaceTemplate == null)
            {
                return NotFound();
            }

            _context.specificSpaceTemplates.Remove(specificSpaceTemplate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecificSpaceTemplateExists(int id)
        {
            return _context.specificSpaceTemplates.Any(e => e.Id == id);
        }
    }
}
