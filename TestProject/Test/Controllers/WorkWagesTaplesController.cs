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
    public class WorkWagesTaplesController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public WorkWagesTaplesController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/WorkWagesTaples
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkWagesTaple>>> GetworkWagesTaples()
        {
            return await _context.workWagesTaples.ToListAsync();
        }

        // GET: api/WorkWagesTaples/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkWagesTaple>> GetWorkWagesTaple(int id)
        {
            var workWagesTaple = await _context.workWagesTaples.FindAsync(id);

            if (workWagesTaple == null)
            {
                return NotFound();
            }

            return workWagesTaple;
        }

        // PUT: api/WorkWagesTaples/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkWagesTaple(int id, WorkWagesTaple workWagesTaple)
        {
            if (id != workWagesTaple.Id)
            {
                return BadRequest();
            }

            _context.Entry(workWagesTaple).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkWagesTapleExists(id))
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

        // POST: api/WorkWagesTaples
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Single")]
        public async Task<ActionResult<WorkWagesTaple>> PostWorkWagesTaple(WorkWagesTaple workWagesTaple)
        {
            _context.workWagesTaples.Add(workWagesTaple);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkWagesTaple", new { id = workWagesTaple.Id }, workWagesTaple);
        }
        [HttpPost("Multiple")]
        public async Task<ActionResult> PostWorkWagesTaples(List<WorkWagesTaple> workWagesTaples)
        {
            _context.workWagesTaples.AddRange(workWagesTaples);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/WorkWagesTaples/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkWagesTaple(int id)
        {
            var workWagesTaple = await _context.workWagesTaples.FindAsync(id);
            if (workWagesTaple == null)
            {
                return NotFound();
            }

            _context.workWagesTaples.Remove(workWagesTaple);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("DeleteAllWages")]
        public async Task<IActionResult> DeleteAllWages()
        {
            try
            {
                var all = _context.workWagesTaples.ToList();
                _context.workWagesTaples.RemoveRange(all);
                await _context.SaveChangesAsync();

                return Ok("✅ تم حذف جميع البيانات من جدول workWagesTaples بنجاح");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ حدث خطأ أثناء الحذف: {ex.Message}");
            }
        }

        private bool WorkWagesTapleExists(int id)
        {
            return _context.workWagesTaples.Any(e => e.Id == id);
        }
    }
}
