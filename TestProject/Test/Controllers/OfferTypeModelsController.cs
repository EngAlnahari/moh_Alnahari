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
    public class OfferTypeModelsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public OfferTypeModelsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/OfferTypeModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfferTypeModel>>> GetOfferTypeModels()
        {
            return await _context.OfferTypeModels.ToListAsync();
        }

        // GET: api/OfferTypeModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfferTypeModel>> GetOfferTypeModel(int id)
        {
            var offerTypeModel = await _context.OfferTypeModels.FindAsync(id);

            if (offerTypeModel == null)
            {
                return NotFound();
            }

            return offerTypeModel;
        }

        // PUT: api/OfferTypeModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfferTypeModel(int id, OfferTypeModel offerTypeModel)
        {
            if (id != offerTypeModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(offerTypeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferTypeModelExists(id))
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

        // POST: api/OfferTypeModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OfferTypeModel>> PostOfferTypeModel(OfferTypeModel offerTypeModel)
        {
            _context.OfferTypeModels.Add(offerTypeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfferTypeModel", new { id = offerTypeModel.ID }, offerTypeModel);
        }

        // DELETE: api/OfferTypeModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfferTypeModel(int id)
        {
            var offerTypeModel = await _context.OfferTypeModels.FindAsync(id);
            if (offerTypeModel == null)
            {
                return NotFound();
            }

            _context.OfferTypeModels.Remove(offerTypeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfferTypeModelExists(int id)
        {
            return _context.OfferTypeModels.Any(e => e.ID == id);
        }
    }
}
