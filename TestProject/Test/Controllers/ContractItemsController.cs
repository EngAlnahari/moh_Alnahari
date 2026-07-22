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
    public class ContractItemsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public ContractItemsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/ContractItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractItem>>> GetcontractItems()
        {
            return await _context.contractItems.ToListAsync();
        }

        // GET: api/ContractItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractItem>> GetContractItem(int id)
        {
            var contractItem = await _context.contractItems.FindAsync(id);

            if (contractItem == null)
            {
                return NotFound();
            }

            return contractItem;
        }
        [HttpGet("GetContractItems/{contractId}")]
        public async Task<IActionResult> GetContractItems(int contractId)
        {
            var items = await _context.contractItems
                .Where(x => x.WorkContractId == contractId || x.WorkContractId == null)
                .OrderBy(x => x.Count)
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("GetContractItems")]
        public async Task<IActionResult> GetContractItems()
        {
            var items = await _context.contractItems.Where(x=> x.WorkContractId == null)
                .OrderBy(x => x.Count) // الترتيب حسب count
                .ToListAsync();

            return Ok(items);
        }

        // PUT: api/ContractItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractItem(int id, ContractItem contractItem)
        {
            if (id != contractItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(contractItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractItemExists(id))
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

        // POST: api/ContractItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractItem>> PostContractItem(ContractItem contractItem)
        {
            _context.contractItems.Add(contractItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractItem", new { id = contractItem.Id }, contractItem);
        }

        // DELETE: api/ContractItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractItem(int id)
        {
            var contractItem = await _context.contractItems.FindAsync(id);
            if (contractItem == null)
            {
                return NotFound();
            }

            _context.contractItems.Remove(contractItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractItemExists(int id)
        {
            return _context.contractItems.Any(e => e.Id == id);
        }
    }
}
