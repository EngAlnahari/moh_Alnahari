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
    public class PurposeSurveysController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public PurposeSurveysController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/PurposeSurveys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurposeSurvey>>> GetpurposeSurveys()
        {
            return await _context.purposeSurveys.ToListAsync();
        }

        // GET: api/PurposeSurveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurposeSurvey>> GetPurposeSurvey(int id)
        {
            var purposeSurvey = await _context.purposeSurveys.FindAsync(id);

            if (purposeSurvey == null)
            {
                return NotFound();
            }

            return purposeSurvey;
        }

        // PUT: api/PurposeSurveys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurposeSurvey(int id, PurposeSurvey purposeSurvey)
        {
            if (id != purposeSurvey.Id)
            {
                return BadRequest();
            }

            _context.Entry(purposeSurvey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurposeSurveyExists(id))
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

        // POST: api/PurposeSurveys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurposeSurvey>> PostPurposeSurvey(PurposeSurvey purposeSurvey)
        {
            _context.purposeSurveys.Add(purposeSurvey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurposeSurvey", new { id = purposeSurvey.Id }, purposeSurvey);
        }

        // DELETE: api/PurposeSurveys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurposeSurvey(int id)
        {
            var purposeSurvey = await _context.purposeSurveys.FindAsync(id);
            if (purposeSurvey == null)
            {
                return NotFound();
            }

            _context.purposeSurveys.Remove(purposeSurvey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurposeSurveyExists(int id)
        {
            return _context.purposeSurveys.Any(e => e.Id == id);
        }
    }
}
