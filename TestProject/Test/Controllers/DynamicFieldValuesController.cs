using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using TestProject.Models;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicFieldValuesController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public DynamicFieldValuesController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/DynamicFieldValu
        // GET: api/DynamicFieldValues/TanjezOrder
        [HttpGet("{screenType}")]
        public async Task<ActionResult<IEnumerable<DynamicFieldValue>>> GetAllValuesByScreen(string screenType)
        {
            return await _context.DynamicFieldValues
                .Where(v => v.ScreenType == screenType)
                .ToListAsync();
        }
        [HttpGet("{screenType}/{recordId}")]
        public async Task<ActionResult<IEnumerable<DynamicFieldValue>>> GetValues(string screenType, int recordId)
        {
            return await _context.DynamicFieldValues
                .Where(v => v.ScreenType == screenType && v.RecordId == recordId)
                .ToListAsync();
        }

        // POST: api/DynamicFieldValues/Multiple
        [HttpPost("Multiple")]
        public async Task<IActionResult> PostMultipleValues(List<DynamicFieldValue> values)
        {
            if (values == null || !values.Any())
            {
                return BadRequest("No values provided.");
            }

            _context.DynamicFieldValues.AddRange(values);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/DynamicFieldValues/MultipleUpdate
        [HttpPut("MultipleUpdate")]
        public async Task<IActionResult> UpdateMultipleValues(List<DynamicFieldValue> values)
        {
            if (values == null || !values.Any())
            {
                return BadRequest("No values provided.");
            }

            var recordId = values.First().RecordId;
            var screenType = values.First().ScreenType;

            // Delete existing
            var existingValues = await _context.DynamicFieldValues
                .Where(v => v.RecordId == recordId && v.ScreenType == screenType)
                .ToListAsync();
            _context.DynamicFieldValues.RemoveRange(existingValues);

            // Add new
            _context.DynamicFieldValues.AddRange(values);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
