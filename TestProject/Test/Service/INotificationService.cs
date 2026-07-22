using System;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Service
{
    public interface INotificationService
    {
        Task<Notification> CreateAsync(int? userId, string message, int? contractId = null, string? url = null);
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId, bool onlyUnread = true);
        Task MarkAsReadAsync(int notificationId);
    }

    public class NotificationService : INotificationService
    {
        private readonly DbContextFirst _db;

        public NotificationService(DbContextFirst db)
        {
            _db = db;
        }

        public async Task<Notification> CreateAsync(int? userId, string message, int? contractId = null, string? url = null)
        {
            var notif = new Notification
            {
                UserId = userId,
                ContractId = contractId,
                Message = message,
                Url = url
            };
            _db.Notifications.Add(notif);
            await _db.SaveChangesAsync();
            return notif;
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId, bool onlyUnread = true)
        {
            var query = _db.Notifications.Where(n => n.UserId == userId);
            if (onlyUnread) query = query.Where(n => !n.IsRead);
            return await query.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notif = await _db.Notifications.FindAsync(notificationId);
            if (notif != null)
            {
                notif.IsRead = true;
                await _db.SaveChangesAsync();
            }
        }
    }

}
