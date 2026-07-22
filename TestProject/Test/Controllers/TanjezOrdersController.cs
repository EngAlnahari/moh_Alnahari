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
    public class TanjezOrdersController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public TanjezOrdersController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/TanjezOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TanjezOrder>>> GetTanjezOrder_1()
        {
            return await _context.TanjezOrder.ToListAsync();
        }

        [HttpGet("GetBordersByOrder/{orderId}")]
        public async Task<ActionResult<IEnumerable<EarthBorders>>> GetBordersByOrder(int orderId)
        {
            // جلب جميع الحدود المرتبطة بالطلب
            var borders = await _context.EarthBorders
                                        .Where(b => b.TanjezOrderID == orderId)
                                        .ToListAsync();

            if (borders == null || borders.Count == 0)
            {
                return NotFound("لا توجد حدود مرتبطة بهذا الطلب.");
            }

            return Ok(borders);
        }


        // GET: api/TanjezOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TanjezOrder>> GetTanjezOrder(int id)
        {
            var tanjezOrder = await _context.TanjezOrder.FindAsync(id);

            if (tanjezOrder == null)
            {
                return NotFound();
            }

            return tanjezOrder;
        }

        // PUT: api/TanjezOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTanjezOrder(int id, TanjezOrder tanjezOrder)
        {
            if (id != tanjezOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(tanjezOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TanjezOrderExists(id))
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

        // POST: api/TanjezOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TanjezOrder>> PostTanjezOrder(TanjezOrder tanjezOrder)
        {
            _context.TanjezOrder.Add(tanjezOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTanjezOrder", new { id = tanjezOrder.Id }, tanjezOrder);
        }

        // DELETE: api/TanjezOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTanjezOrder(int id)
        {
            var tanjezOrder = await _context.TanjezOrder.FindAsync(id);
            if (tanjezOrder == null)
            {
                return NotFound();
            }

            _context.TanjezOrder.Remove(tanjezOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TanjezOrderExists(int id)
        {
            return _context.TanjezOrder.Any(e => e.Id == id);
        }
    }
}
