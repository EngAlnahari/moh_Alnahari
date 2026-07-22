using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class PriceTable
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public int SectionTypeId { get; set; } // ربط Link → نوع القسم
        public int AreaStandardTemplateId { get; set; } // ربط Link → قالب معيار المساحة
        public decimal Price { get; set; } // السعر
        public int ReportFromInDay { get; set; } // عدد الأيام من التقرير
        public int ReportToInDay { get; set; } // عدد الأيام إلى التقرير
        public int ParentOfferId { get; set; } // ربط Link → عرض العمل
    }
}
