using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;
using Test.Service;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ITokenCreate _tokenCreate;
        private readonly DbContextFirst _context;
      

  
        public UsersController(DbContextFirst context, ITokenCreate tokenCreate)
        {
            _context = context;
            _tokenCreate = tokenCreate;

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("GetUserOffers/{id}")]
        public async Task<ActionResult<IEnumerable<WorkOffer>>> GetUserOffers(int id)
        {
            var user = await _context.Users
                .Include(u => u.workOffers)
                .FirstOrDefaultAsync(u => u.ID == id);

            if (user == null)
                return NotFound();

            // إرجاع العروض فقط
            var offers = user.workOffers.Select(o => new
            {
                o.Id,
                o.OfferName
               
            }).ToList();

            return Ok(offers);
        }




        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // جلب المستخدم مع العروض المرتبطة به
            var user = await _context.Users
                .Include(u => u.workOffers) // افتراض أن لديك ICollection<Offer> في User
                .FirstOrDefaultAsync(u => u.ID == id);

            if (user == null)
            {
                return NotFound();
            }

            // إرجاع المستخدم مع العروض
            return Ok(user);
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //[HttpPost("Userlogin")]
        //public async Task<IActionResult> Userlogin([FromBody] UserLogin userlogin)
        //{
        //    if (string.IsNullOrEmpty(userlogin.Name) || string.IsNullOrEmpty(userlogin.Pass))
        //    {
        //        return BadRequest();
        //    }
        //    var userlog = await _context.Users.SingleOrDefaultAsync(u => u.Frist_Name == userlogin.Name && u.Pass == userlogin.Pass);
        //    if (userlog == null)
        //    {
        //        return Unauthorized();
        //    }
        //    string rol = userlog.User_type;
        //    var token = _tokenCreate.GetToken(userlogin.Name, rol);
        //    return Ok(new
        //    {
        //        State = "تم تسجيل الدخول",
        //        Token = token,

        //        UserType = userlog.User_type,
        //        role = userlog.ID,
        //        //UserType = userlog.User_type  // إرجاع نوع المستخدم


        //    });

        //}
        [HttpPost("Userlogin")]
        public async Task<IActionResult> Userlogin([FromBody] UserLogin userlogin)
        {
            if (string.IsNullOrEmpty(userlogin.Name) || string.IsNullOrEmpty(userlogin.Pass))
            {
                return BadRequest(new { Message = "الاسم أو كلمة المرور مطلوبة." });
            }

            var userlog = await _context.Users
                .SingleOrDefaultAsync(u => u.Frist_Name == userlogin.Name && u.Pass == userlogin.Pass);

            if (userlog == null)
            {
                return Unauthorized(new { Message = "اسم المستخدم أو كلمة المرور غير صحيحة." });
            }

            // 🔹 استخراج نوع المستخدم
            string userType = userlog.User_type;

            // 🔹 توليد التوكن
            var token = _tokenCreate.GetToken(userlogin.Name, userType);

            // 🔹 إرجاع البيانات المطلوبة
            return Ok(new
            {
                State = "تم تسجيل الدخول بنجاح",
                Token = token,
                UserType = userlog.User_type,  // نوع المستخدم
                UserId = userlog.ID,           // رقم المستخدم
                Name = userlog.Frist_Name      // الاسم
            });
        }
        [HttpGet("managers")]
        public async Task<IActionResult> GetManagers()
        {
            try
            {
                var managers = await _context.Users
                    .Where(u => u.User_type == "مدير")
                    .ToListAsync();

                if (managers == null || !managers.Any())
                    return NotFound("لم يتم العثور على أي مدير.");

                return Ok(managers);
            }
            catch (Exception ex)
            {
                return BadRequest($"حدث خطأ أثناء جلب المدراء: {ex.Message}");
            }
        }


        [HttpPut("{id}/AuthenticStatus")]
        public async Task<IActionResult> UpdateAuthenticStatus(int id, [FromBody] string isAuthentic)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Is_Authentic = isAuthentic;
            await _context.SaveChangesAsync();

            return Ok(user);
        }



        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
