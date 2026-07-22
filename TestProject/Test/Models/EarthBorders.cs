using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Test.Models
{
    public class EarthBorders// حدود الأرض)
    {
        public int ID { get; set; } // المفتاح الرئيسي
        public string BorderType { get; set; } // نوع الحد
        public string? BorderDescription { get; set; } // وصف الحد
        public int? IdenticalOrDifferent { get; set; } // مطابق أو مختلف؟
        public string? Difference { get; set; } // الوصف التفصيلي للاختلاف
        public string? Difference1 { get; set; } // حقل اضافي
        public int? Difference12 { get; set; } // حقل اضافي
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }

        public int? TanjezOrderID { get; set; }
        [ForeignKey("TanjezOrderID")]
      
        public TanjezOrder? tanjezOrder { get; set; }
    }
}
