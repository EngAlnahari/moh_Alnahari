namespace Test.Models
{
    public class AreaStandardTemplate
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public ICollection<AreaStandardFromTo> Standards { get; set; } // جدول فرعي
    }
}
