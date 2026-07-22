using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class TanjezOrder//Tanjez Order (طلب تنجيز)
    {
        public int Id { get; set; } 
        [Display(Name = "اسم منشاء الطلب")]
        public string? CreateOrderName { get; set; } 
        [Display(Name = "  صفة منشا الطلب")]
        public string? DescripPerson { get; set; } 
        [Display(Name = "  الغرض من الطلب")]
        public string? PurposeOrder { get; set; } 
        [Display(Name = "  نوع القطعه")]
        public string TypePiece { get; set; } 
        [Display(Name = "  الدولة")]
        //   بيانات الموقع
        public string? Country { get; set; } 
        [Display(Name = "  المديىة")]
        public string City { get; set; } 
        [Display(Name = "  العزلة")]
        public string? Zone { get; set; } 
        [Display(Name = "  القرية")]
        public string? Village { get; set; } 
        [Display(Name = "  احداثيات X")]

        //   بيانات الوثيقه
        public string? CoordinatesX { get; set; } 
        [Display(Name = "  احداثيات Y")]
        public string? CoordinatesY { get; set; } 
        [Display(Name = "  اسم الموضع")]
        public string? PlaceName { get; set; } 
        [Display(Name = "  نوع الموضع")]
        public string? PlaceType { get; set; } 
        public string? DocumentType { get; set; } 
        [Display(Name = "  المساحة قصبة")]
        public int? AcountPiese { get; set; } 
        [Display(Name = "  هل المساحة مختلفة؟")]
        public int? AreaDifferent { get; set; } 
        [Display(Name = "  المساحة كما في الوثيقة")]
        public int Space { get; set; } 
        [Display(Name = "  صورة الوثيقة")]
        public string? DocumentImage { get; set; }
        [Display(Name = "   المديرية")]
        public string? Directorate { get; set; }
        [Display(Name = "   وحدة القياس")]
        public string? Unit { get; set; }
        //// حدود الأرض
        //public List<EarthBorder>? EarthBorders { get; set; } = new List<EarthBorder>(); // حدود الأرض

        // الحائز
        [Display(Name = "  الحائز")]
        public string? Alhiaza { get; set; } 
        [Display(Name = "  نوع طريق الوصول")]
        public string? AccessRoadType { get; set; } 
        [Display(Name = "  نوع الفترة")]
        public string?   PeriodType { get; set; } 
        [Display(Name = "  أقصى مدة")]
        public DateTime? MaximumDuration { get; set; } 
        [Display(Name = "  المساحة")]
        public int?   Width { get; set; } 
        [Display(Name = "  التنقلات")]
        public string? Transportation { get; set; }
        [Display(Name = "  المبيت")]
        public string? OverNight { get; set; }
        [Display(Name = "  النثريات")]
        public string? Allowance { get; set; }
        // التكاليف
        public int? WorkOfferId { get; set; } // ربط Link → عرض العمل
        public decimal?  WorkCost { get; set; } // تكلفة العمل
        public decimal? TransportationCost { get; set; } // تكلفة النقل
        public decimal? AdditionalCost { get; set; } // التكاليف الإضافية
        public decimal? TotalCost { get; set; } // التكلفة الإجمالية
        [Display(Name = "  من اعلا")]
        public string? Up { get; set; }
        [Display(Name = "  من اسفل")]
        public string? Down { get; set; }

        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //طلب تقديم شكوى نزاع
        [Display(Name = "رقم القضية")]
        public string? Num_Brop { get; set; }
        [Display(Name = "رقم الشكوى")]
        public string? Num_Brop1 { get; set; }
        [Display(Name = "تاريخ التقديم")]
        public string? Data_Brop { get; set; }
        [Display(Name = "الجهة الشاكية")]
        public string? Direct_Brop { get; set; }
        [Display(Name = "العدد")]
        public string? Count { get; set; }
        [Display(Name = "الاسماء")]
        public string? Name_Inv { get; set; }
        [Display(Name = "موضوع الشكوى")]
        public string? Topic_Inv { get; set; }
        [Display(Name = "ملخص الشكوى")]
        public string? Summray_Inv { get; set; }

        [Display(Name = "موضع الخلاف")]
        public string? Place_Inv { get; set; }
        [Display(Name = "طبيعة منطقية الخلاف ")]
        public string? Type_Area_Inv { get; set; }
        [Display(Name = "الادعاء من وجة نضر الطرف الاول")]
        public string? Inv_From_1 { get; set; }
        [Display(Name = "رد المدعي علية")]
        public string? Inv_From_2 { get; set; }
        // بيانات إضافية
        public DateTime? CreatedAt { get; set; } = DateTime.Now; // تاريخ الإنشاء
        public string? Status { get; set; } = "جديد"; // حالة الطلب
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
        public List<EarthBorders>? earthBorders { get; set; }

    }

    public class TanjezOrderDto
    {
        public int Id { get; set; }
        [Display(Name = "اسم منشاء الطلب")]
        public string CreateOrderName { get; set; }
        [Display(Name = "  اسم المكان")]
        public string PlaceName { get; set; }
        [Display(Name = "  المساحة")]
        public int? Space { get; set; }
        [Display(Name = "  نوع المكان")]
        public string TypePiece { get; set; }
        [Display(Name = "  المدينة")]
        public string City { get; set; }
        [Display(Name = "  المستخدم")]
        public int? UserID { get; set; }
        [Display(Name = "  الحيازة")]
        public string Alhiaza { get; set; }
        public List<EarthBorderDto> EarthBorders { get; set; }
    }

    public class EarthBorderDto
    {
        public int ID { get; set; }
        public string BorderType { get; set; }
        public string BorderDescription { get; set; }
        public int? IdenticalOrDifferent { get; set; }
        public int? Difference { get; set; }
    }

}
