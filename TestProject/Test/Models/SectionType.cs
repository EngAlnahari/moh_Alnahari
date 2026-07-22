namespace Test.Models
{
    public class SectionType
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string SectionTypeName { get; set; } // اسم نوع القسم
        public int? SectionId { get; set; } // ربط Link → القسم (اختياري)
        public Section Section { get; set; }
    }
}
