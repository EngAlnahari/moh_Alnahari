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
    public class CharacteristicpersonsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public CharacteristicpersonsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/Characteristicpersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Characteristicperson>>> GetCharacteristicpeople()
        {
            return await _context.Characteristicpeople.ToListAsync();
        }

        // GET: api/Characteristicpersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Characteristicperson>> GetCharacteristicperson(int id)
        {
            var characteristicperson = await _context.Characteristicpeople.FindAsync(id);

            if (characteristicperson == null)
            {
                return NotFound();
            }

            return characteristicperson;
        }

        // PUT: api/Characteristicpersons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacteristicperson(int id, Characteristicperson characteristicperson)
        {
            if (id != characteristicperson.Id)
            {
                return BadRequest();
            }

            _context.Entry(characteristicperson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacteristicpersonExists(id))
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

        // POST: api/Characteristicpersons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Characteristicperson>> PostCharacteristicperson(Characteristicperson characteristicperson)
        {
            _context.Characteristicpeople.Add(characteristicperson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacteristicperson", new { id = characteristicperson.Id }, characteristicperson);
        }

        // DELETE: api/Characteristicpersons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacteristicperson(int id)
        {
            var characteristicperson = await _context.Characteristicpeople.FindAsync(id);
            if (characteristicperson == null)
            {
                return NotFound();
            }

            _context.Characteristicpeople.Remove(characteristicperson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacteristicpersonExists(int id)
        {
            return _context.Characteristicpeople.Any(e => e.Id == id);
        }
    }
}
