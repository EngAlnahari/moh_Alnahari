using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Land
    {
       
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ParcelNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string OwnerName { get; set; }

        public double Area { get; set; }

        // يمكنك استخدام string أو SqlGeometry حسب طريقة تخزينك
        public string Shape { get; set; } // إذا ستخزن GeoJSON كنص
        // public SqlGeometry Shape { get; set; } // إذا تريد استخدام نوع geometry SQL Server

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // إضافة حقول للتتبع (اختياري)
        [MaxLength(100)]
        public string CreatedBy { get; set; }

        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
