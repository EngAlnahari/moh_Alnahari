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
    public class ContractAmendmentsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public ContractAmendmentsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/ContractAmendments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractAmendment>>> GetcontractAmendments()
        {
            return await _context.contractAmendments.ToListAsync();
        }

        // GET: api/ContractAmendments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractAmendment>> GetContractAmendment(int id)
        {
            var contractAmendment = await _context.contractAmendments.FindAsync(id);

            if (contractAmendment == null)
            {
                return NotFound();
            }

            return contractAmendment;
        }

        // PUT: api/ContractAmendments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractAmendment(int id, ContractAmendment contractAmendment)
        {
            if (id != contractAmendment.Id)
            {
                return BadRequest();
            }

            _context.Entry(contractAmendment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractAmendmentExists(id))
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

        // POST: api/ContractAmendments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractAmendment>> PostContractAmendment(ContractAmendment contractAmendment)
        {
            _context.contractAmendments.Add(contractAmendment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractAmendment", new { id = contractAmendment.Id }, contractAmendment);
        }

        // DELETE: api/ContractAmendments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractAmendment(int id)
        {
            var contractAmendment = await _context.contractAmendments.FindAsync(id);
            if (contractAmendment == null)
            {
                return NotFound();
            }

            _context.contractAmendments.Remove(contractAmendment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractAmendmentExists(int id)
        {
            return _context.contractAmendments.Any(e => e.Id == id);
        }
    }
}
