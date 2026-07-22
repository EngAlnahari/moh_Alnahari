namespace Test.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int? UserId { get; set; }        // المستخدم المستلم (ClientId أو EngineerId)
        public int? ReciveId { get; set; }        // المستخدم المستلم (ClientId أو EngineerId)
        public string? UserType { get; set; }

        public int? ContractId { get; set; }    // العقد المرتبط (اختياري)
        public string Message { get; set; } = "";
        public string? Url { get; set; }        // رابط اختياري (مثل تفاصيل العقد)
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
