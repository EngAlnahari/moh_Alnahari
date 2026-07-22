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
    public class TransportModelsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public TransportModelsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/TransportModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportModel>>> GetTransportModels()
        {
            return await _context.TransportModels.ToListAsync();
        }

        // GET: api/TransportModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportModel>> GetTransportModel(int id)
        {
            var transportModel = await _context.TransportModels.FindAsync(id);

            if (transportModel == null)
            {
                return NotFound();
            }

            return transportModel;
        }

        // PUT: api/TransportModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransportModel(int id, TransportModel transportModel)
        {
            if (id != transportModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(transportModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportModelExists(id))
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

        // POST: api/TransportModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Single")]
        public async Task<ActionResult<TransportModel>> PostTransportModel(TransportModel transportModel)
        {
            _context.TransportModels.Add(transportModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransportModel", new { id = transportModel.ID }, transportModel);
        }
        [HttpPost("Multiple")]
        public async Task<ActionResult<TransportModel>> PostTransportModel(List<TransportModel> earthBorders)
        {
            _context.TransportModels.AddRange(earthBorders);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/TransportModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransportModel(int id)
        {
            var transportModel = await _context.TransportModels.FindAsync(id);
            if (transportModel == null)
            {
                return NotFound();
            }

            _context.TransportModels.Remove(transportModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransportModelExists(int id)
        {
            return _context.TransportModels.Any(e => e.ID == id);
        }
    }
}
