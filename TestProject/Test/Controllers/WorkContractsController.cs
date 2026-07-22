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
    public class WorkContractsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public WorkContractsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/WorkContracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkContract>>> GetWorkContract()
        {
            return await _context.WorkContract.ToListAsync();
        }

        // GET: api/WorkContracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkContract>> GetWorkContract(int id)
        {
            var workContract = await _context.WorkContract
                .Include(w => w.contractAmendments) // تحميل التعديلات المرتبطة
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workContract == null)
            {
                return NotFound();
            }

            return workContract;
        }


        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateContractStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var contract = await _context.WorkContract.FindAsync(id);
            if (contract == null)
                return NotFound(new { success = false, message = "العقد غير موجود." });

            contract.Status = dto.Status;
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "تم تحديث الحالة بنجاح." });
        }




        //[HttpPut("UpdateStatus/{id}")]
        //public async Task<IActionResult> UpdateContractStatus(int id, [FromBody] ContractStatus status)
        //{
        //    var contract = await _context.WorkContract.FindAsync(id);
        //    if (contract == null) return NotFound();

        //    contract.Status = status;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        // PUT: api/WorkContracts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkContract(int id, WorkContract workContract)
        {
            if (id != workContract.Id)
            {
                return BadRequest();
            }

            _context.Entry(workContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkContractExists(id))
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

        // POST: api/WorkContracts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkContract>> PostWorkContract(WorkContract workContract)
        {
            _context.WorkContract.Add(workContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkContract", new { id = workContract.Id }, workContract);
        }

        // DELETE: api/WorkContracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkContract(int id)
        {
            var workContract = await _context.WorkContract.FindAsync(id);
            if (workContract == null)
            {
                return NotFound();
            }

            _context.WorkContract.Remove(workContract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkContractExists(int id)
        {
            return _context.WorkContract.Any(e => e.Id == id);
        }
    }
}
