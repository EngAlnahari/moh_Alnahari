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
    public class AdditionalCostModelsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public AdditionalCostModelsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/AdditionalCostModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalCostModel>>> GetAdditionalCostModels()
        {
            return await _context.AdditionalCostModels.ToListAsync();
        }

        // GET: api/AdditionalCostModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalCostModel>> GetAdditionalCostModel(int id)
        {
            var additionalCostModel = await _context.AdditionalCostModels.FindAsync(id);

            if (additionalCostModel == null)
            {
                return NotFound();
            }

            return additionalCostModel;
        }

        // PUT: api/AdditionalCostModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalCostModel(int id, AdditionalCostModel additionalCostModel)
        {
            if (id != additionalCostModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(additionalCostModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalCostModelExists(id))
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

        // POST: api/AdditionalCostModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Single")]
        public async Task<ActionResult<AdditionalCostModel>> PostAdditionalCostModel(AdditionalCostModel additionalCostModel)
        {
            _context.AdditionalCostModels.Add(additionalCostModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalCostModel", new { id = additionalCostModel.ID }, additionalCostModel);
        }
        [HttpPost("Multiple")]
        public async Task<ActionResult> PostWorkWagesTaples(List<AdditionalCostModel> workWagesTaples)
        {
            _context.AdditionalCostModels.AddRange(workWagesTaples);
            await _context.SaveChangesAsync();
            return Ok();
        }
        // DELETE: api/AdditionalCostModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdditionalCostModel(int id)
        {
            var additionalCostModel = await _context.AdditionalCostModels.FindAsync(id);
            if (additionalCostModel == null)
            {
                return NotFound();
            }

            _context.AdditionalCostModels.Remove(additionalCostModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdditionalCostModelExists(int id)
        {
            return _context.AdditionalCostModels.Any(e => e.ID == id);
        }
    }
}
