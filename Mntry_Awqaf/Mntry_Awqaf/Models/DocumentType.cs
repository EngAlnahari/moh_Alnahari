using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class DocumentType
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string DocumentTypeName { get; set; } // اسم نوع الوثيقة
    }
}
