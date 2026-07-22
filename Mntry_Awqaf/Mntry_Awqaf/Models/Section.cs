using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class Section
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string SectionName { get; set; } // اسم القسم
        public int CategoryId { get; set; } // ربط Link → الفئة
        public Category Category { get; set; }
    }

}
