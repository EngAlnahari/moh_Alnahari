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
    public class LandBoundaryModelsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public LandBoundaryModelsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/LandBoundaryModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LandBoundaryModel>>> GetLandBoundaryModel()
        {
            return await _context.LandBoundaryModel.ToListAsync();
        }

        // GET: api/LandBoundaryModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LandBoundaryModel>> GetLandBoundaryModel(int id)
        {
            var landBoundaryModel = await _context.LandBoundaryModel.FindAsync(id);

            if (landBoundaryModel == null)
            {
                return NotFound();
            }

            return landBoundaryModel;
        }

        // PUT: api/LandBoundaryModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLandBoundaryModel(int id, LandBoundaryModel landBoundaryModel)
        {
            if (id != landBoundaryModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(landBoundaryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandBoundaryModelExists(id))
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

        // POST: api/LandBoundaryModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LandBoundaryModel>> PostLandBoundaryModel(LandBoundaryModel landBoundaryModel)
        {
            _context.LandBoundaryModel.Add(landBoundaryModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLandBoundaryModel", new { id = landBoundaryModel.Id }, landBoundaryModel);
        }
        [HttpPost("Multiple")]
        public async Task<ActionResult> PostLandBoundaryModel(List<LandBoundaryModel> landBoundaryModels)
        {
            _context.LandBoundaryModel.AddRange(landBoundaryModels);
            await _context.SaveChangesAsync();
            return Ok();
        }
        // DELETE: api/LandBoundaryModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLandBoundaryModel(int id)
        {
            var landBoundaryModel = await _context.LandBoundaryModel.FindAsync(id);
            if (landBoundaryModel == null)
            {
                return NotFound();
            }

            _context.LandBoundaryModel.Remove(landBoundaryModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LandBoundaryModelExists(int id)
        {
            return _context.LandBoundaryModel.Any(e => e.Id == id);
        }
    }
}
