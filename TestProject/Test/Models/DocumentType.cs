using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class DocumentType
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string DocumentTypeName { get; set; } // اسم نوع الوثيقة
    }
}
