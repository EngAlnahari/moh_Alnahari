using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class Category
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string CategoryName { get; set; } // اسم الفئة
        public int ClassificationId { get; set; } // ربط Link → التصنيف
        [ForeignKey("ClassificationId")]
        public Classification? Depart { get; set; }
    }
  
}
