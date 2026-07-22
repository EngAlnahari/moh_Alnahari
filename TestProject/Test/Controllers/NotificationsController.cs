using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public NotificationsController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }
        // 🔹 دالة جديدة لجلب الإشعارات التي ليس لها عقد
        [HttpGet("GetUserNotificationsOnly")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetUserNotificationsOnly()
        {
            var userNotifications = await _context.Notifications
                .Where(n => n.ContractId == null) // فقط الإشعارات العادية (بدون عقد)
                .ToListAsync();

            return Ok(userNotifications);
        }


        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return NotFound();

            return notification;
        }
        [HttpPost("MarkAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notif = await _context.Notifications.FindAsync(id);
            if (notif == null)
                return NotFound();

            notif.IsRead = true;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/Notifications/user/5

        // جلب إشعارات مستخدم محدد، مع خيار جلب غير المقروءة فقط
        [HttpGet("GetUserNotifications/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetUserNotifications(int userId, [FromQuery] bool onlyUnread = true)
        {
            var query = _context.Notifications.Where(n => n.UserId == userId);
            if (onlyUnread)
                query = query.Where(n => !n.IsRead);

            var notifications = await query.OrderByDescending(n => n.CreatedAt).ToListAsync();
            return Ok(notifications);
        }


        [HttpGet("GetEngineerNotifications")]
        public async Task<IActionResult> GetEngineerNotifications(int engineerId)
        {
            var notifs = await _context.Notifications
                .Where(n => n.UserId == engineerId && !n.IsRead) // تصفية غير المقروءة فقط
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return Ok(notifs);
        }

        // POST: api/Notifications
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification([FromBody] NotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                ReciveId = dto.ReciveId,
                UserType = dto.UserType,
                ContractId = dto.ContractId,
                Message = dto.Message,
                Url = dto.Url,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
        }

        // POST: api/Notifications/mark-as-read/5
        //[HttpPost("mark-as-read/{id}")]
        //public async Task<IActionResult> MarkAsRead(int id)
        //{
        //    var notification = await _context.Notifications.FindAsync(id);
        //    if (notification == null)
        //        return NotFound();

        //    notification.IsRead = true;
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(int id, Notification notification)
        {
            if (id != notification.Id)
                return BadRequest();

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound();

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }

    // DTO لإرسال الإشعارات من العميل
    public class NotificationDto
    {
        public int? UserId { get; set; }        // المستلم
        public int? ReciveId { get; set; }  // ✅ تأكد أنها موجودة هنا
        public string? UserType { get; set; } // ✅ تأكد أنها موجودة هنا
        public int? ContractId { get; set; }    // العقد المرتبط
        public string Message { get; set; }     // نص الإشعار
        public string? Url { get; set; }        // رابط اختياري
    }
}
