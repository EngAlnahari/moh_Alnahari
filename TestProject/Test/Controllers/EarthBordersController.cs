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
    public class EarthBordersController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public EarthBordersController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/EarthBorders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EarthBorders>>> GetEarthBorders()
        {
            return await _context.EarthBorders.ToListAsync();
        }

        // GET: api/EarthBorders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EarthBorders>> GetEarthBorders(int id)
        {
            var earthBorders = await _context.EarthBorders.FindAsync(id);

            if (earthBorders == null)
            {
                return NotFound();
            }

            return earthBorders;
        }

        // PUT: api/EarthBorders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEarthBorders(int id, EarthBorders earthBorders)
        {
            if (id != earthBorders.ID)
            {
                return BadRequest();
            }

            _context.Entry(earthBorders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EarthBordersExists(id))
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

        // POST: api/EarthBorders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Single")]
        public async Task<ActionResult<EarthBorders>> PostEarthBorders(EarthBorders earthBorders)
        {
            _context.EarthBorders.Add(earthBorders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEarthBorders", new { id = earthBorders.ID }, earthBorders);
        }
        [HttpPost("Multiple")]
        public async Task<ActionResult> PostEarthBorders(List<EarthBorders> earthBorders)
        {
            _context.EarthBorders.AddRange(earthBorders);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/EarthBorders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEarthBorders(int id)
        {
            var earthBorders = await _context.EarthBorders.FindAsync(id);
            if (earthBorders == null)
            {
                return NotFound();
            }

            _context.EarthBorders.Remove(earthBorders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EarthBordersExists(int id)
        {
            return _context.EarthBorders.Any(e => e.ID == id);
        }

        [HttpGet("ByOrder/{orderId}")]
        public async Task<IActionResult> GetByOrder(int orderId)
        {
            var borders = await _context.EarthBorders
                .Where(e => e.TanjezOrderID == orderId)
                .ToListAsync();

            if (!borders.Any())
                return NotFound();

            return Ok(borders);
        }
        [HttpPut("MultipleUpdate")]
        public async Task<IActionResult> MultipleUpdate([FromBody] List<EarthBorders> borders)
        {
            if (borders == null || !borders.Any())
                return BadRequest("لم يتم إرسال أي بيانات للتحديث.");

            foreach (var border in borders)
            {
                var existing = await _context.EarthBorders
                    .FirstOrDefaultAsync(e => e.ID == border.ID);

                if (existing != null)
                {
                    // تحديث الحقول المطلوبة فقط
                    existing.BorderType = border.BorderType;
                    existing.BorderDescription = border.BorderDescription;
                    existing.IdenticalOrDifferent = border.IdenticalOrDifferent;
                    existing.Difference = border.Difference;

                    // لا يتم تحديث أي حقول أخرى
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "تم تحديث حدود الأرض بنجاح ✅" });
        }

    }
}
