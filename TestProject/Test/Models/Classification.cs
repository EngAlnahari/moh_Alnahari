namespace Test.Models
{
    public class Classification
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string ClassificationName { get; set; } // اسم التصنيف
        public int AlmjalId { get; set; } // ربط Link → المجال
        public Almjal Almjal { get; set; }
    }
}
