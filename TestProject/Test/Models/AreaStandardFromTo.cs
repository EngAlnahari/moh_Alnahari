namespace Test.Models
{
    public class AreaStandardFromTo
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public decimal AreaFrom { get; set; } // المساحة من
        public decimal AreaTo { get; set; } // المساحة إلى
        public decimal CostOfSurveyingDevice { get; set; } // تكلفة جهاز المساحة
        public decimal SurveyingDeviceCost { get; set; } // تكلفة الجهاز
        public decimal TotalPrice { get; set; } // السعر الإجمالي
        public int ParentTemplateId { get; set; } // ربط Link → قالب معيار المساحة
    }
}
