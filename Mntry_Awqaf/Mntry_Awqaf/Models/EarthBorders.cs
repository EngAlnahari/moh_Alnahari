using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class EarthBorders// حدود الأرض)
    {
        public int ID { get; set; } // المفتاح الرئيسي
        public string BorderType { get; set; } // نوع الحد
        public string? BorderDescription { get; set; } //  الحد كما في الوثيقة
        public int? IdenticalOrDifferent { get; set; } // مطابق أو مختلف؟
        public string? Difference { get; set; } // الفرق
        public string? Difference1 { get; set; } // الوصف التفصيلي للاختلاف
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
        public int? TanjezOrderID { get; set; }
        [ForeignKey("TanjezOrderID")]
        public TanjezOrder? tanjezOrder { get; set; }
    }
}
