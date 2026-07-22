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
    public class WorkWagesTemplatesController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public WorkWagesTemplatesController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/WorkWagesTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkWagesTemplate>>> GetWorkWagesTemplate()
        {
            return await _context.WorkWagesTemplate.ToListAsync();
        }
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<WorkWagesTemplate>> GetByName(string name)
        {
            var template = await _context.WorkWagesTemplate
                                         .FirstOrDefaultAsync(t => t.WorkWagesName == name);
            if (template == null) return NotFound();
            return template;
        }

        // GET: api/WorkWagesTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkWagesTemplate>> GetWorkWagesTemplate(int id)
        {
            var workWagesTemplate = await _context.WorkWagesTemplate.FindAsync(id);

            if (workWagesTemplate == null)
            {
                return NotFound();
            }

            return workWagesTemplate;
        }

        // PUT: api/WorkWagesTemplates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkWagesTemplate(int id, WorkWagesTemplate workWagesTemplate)
        {
            if (id != workWagesTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(workWagesTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkWagesTemplateExists(id))
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

        [HttpGet("GetWorkWagesTemplateWithDetail/{id}")]
        public async Task<IActionResult> GetWorkWagesTemplateWithDetail(int id)
        {
            var template = await _context.WorkWagesTemplate
                .FirstOrDefaultAsync(t => t.Id == id);

            if (template == null)
                return NotFound();

            // جلب التنقلات المرتبطة بهذا القالب
            template.workWagesTaples = await _context.workWagesTaples
                .Where(m => m.WorkWagesTemplatesID == id)
                .ToListAsync();

            return Ok(template);
        }

        // POST: api/WorkWagesTemplates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkWagesTemplate>> PostWorkWagesTemplate(WorkWagesTemplate workWagesTemplate)
        {
            _context.WorkWagesTemplate.Add(workWagesTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkWagesTemplate", new { id = workWagesTemplate.Id }, workWagesTemplate);
        }

        // DELETE: api/WorkWagesTemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkWagesTemplate(int id)
        {
            var workWagesTemplate = await _context.WorkWagesTemplate.FindAsync(id);
            if (workWagesTemplate == null)
            {
                return NotFound();
            }

            _context.WorkWagesTemplate.Remove(workWagesTemplate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkWagesTemplateExists(int id)
        {
            return _context.WorkWagesTemplate.Any(e => e.Id == id);
        }
    }
}
