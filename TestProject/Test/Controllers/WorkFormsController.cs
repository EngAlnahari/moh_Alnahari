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
    public class WorkFormsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public WorkFormsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/WorkForms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkForm>>> GetWorkForms()
        {
            return await _context.WorkForms.ToListAsync();
        }

        // GET: api/WorkForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkForm>> GetWorkForm(int id)
        {
            var workForm = await _context.WorkForms.FindAsync(id);

            if (workForm == null)
            {
                return NotFound();
            }

            return workForm;
        }

        // PUT: api/WorkForms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkForm(int id, WorkForm workForm)
        {
            if (id != workForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(workForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkFormExists(id))
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

        // POST: api/WorkForms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkForm>> PostWorkForm(WorkForm workForm)
        {
            _context.WorkForms.Add(workForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkForm", new { id = workForm.Id }, workForm);
        }

        // DELETE: api/WorkForms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkForm(int id)
        {
            var workForm = await _context.WorkForms.FindAsync(id);
            if (workForm == null)
            {
                return NotFound();
            }

            _context.WorkForms.Remove(workForm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkFormExists(int id)
        {
            return _context.WorkForms.Any(e => e.Id == id);
        }
    }
}
