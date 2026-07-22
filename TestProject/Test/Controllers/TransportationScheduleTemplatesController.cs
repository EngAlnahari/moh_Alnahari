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
    public class TransportationScheduleTemplatesController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public TransportationScheduleTemplatesController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/TransportationScheduleTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationScheduleTemplate>>> GetTransportationScheduleTemplate()
        {
            return await _context.TransportationScheduleTemplate.ToListAsync();
        }

        [HttpGet("byname/{name}")]
        public async Task<ActionResult<TransportationScheduleTemplate>> GetByName(string name)
        {
            var template = await _context.TransportationScheduleTemplate
                                         .FirstOrDefaultAsync(t => t.NameTemplete == name);
            if (template == null) return NotFound();
            return template;
        }
        // GET: api/TransportationScheduleTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportationScheduleTemplate>> GetTransportationScheduleTemplate(int id)
        {
            var transportationScheduleTemplate = await _context.TransportationScheduleTemplate.FindAsync(id);

            if (transportationScheduleTemplate == null)
            {
                return NotFound();
            }

            return transportationScheduleTemplate;
        }


        [HttpGet("GetTransportationTemplate/{id}")]
        public async Task<IActionResult> GetTransportationTemplate(int id)
        {
            var template = await _context.TransportationScheduleTemplate
                .FirstOrDefaultAsync(t => t.Id == id);

            if (template == null)
                return NotFound();

            // جلب التنقلات المرتبطة بهذا القالب
            template.transportModels = await _context.TransportModels
                .Where(m => m.TransportationScheduleTemplateID == id)
                .ToListAsync();

            return Ok(template);
        }



        // PUT: api/TransportationScheduleTemplates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransportationScheduleTemplate(int id, TransportationScheduleTemplate transportationScheduleTemplate)
        {
            if (id != transportationScheduleTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(transportationScheduleTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportationScheduleTemplateExists(id))
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

        // POST: api/TransportationScheduleTemplates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransportationScheduleTemplate>> PostTransportationScheduleTemplate(TransportationScheduleTemplate transportationScheduleTemplate)
        {
            _context.TransportationScheduleTemplate.Add(transportationScheduleTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransportationScheduleTemplate", new { id = transportationScheduleTemplate.Id }, transportationScheduleTemplate);
        }

        // DELETE: api/TransportationScheduleTemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransportationScheduleTemplate(int id)
        {
            var transportationScheduleTemplate = await _context.TransportationScheduleTemplate.FindAsync(id);
            if (transportationScheduleTemplate == null)
            {
                return NotFound();
            }

            _context.TransportationScheduleTemplate.Remove(transportationScheduleTemplate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransportationScheduleTemplateExists(int id)
        {
            return _context.TransportationScheduleTemplate.Any(e => e.Id == id);
        }
    }
}
