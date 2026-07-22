using System.ComponentModel.DataAnnotations;

namespace Mntry_Awqaf.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public int? UserId { get; set; }        // المستخدم المستلم (ClientId أو EngineerId)
        //[Required(ErrorMessage = "الاسم الكامل مطلوب")]
        [Display(Name = " المهندس")]
        public int? ReciveId { get; set; }
        [Display(Name = " النوع")]

        public string? UserType { get; set; }
        [Display(Name = " العقد")]

        public int? ContractId { get; set; }    // العقد المرتبط (اختياري)
        [Display(Name = " الطلب")]

        public string Message { get; set; } = "";
        public string? Url { get; set; }        // رابط اختياري (مثل تفاصيل العقد)
        [Display(Name = " تم المراجعة")]

        public bool IsRead { get; set; } = false;
        [Display(Name = " تاريخ الطلب")]

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
