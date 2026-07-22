using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class WorkForm
    {

            public int Id { get; set; }
            [Display(Name = "نوع الطلب")]
            public string? RequestType { get; set; }

            [Display(Name = "رقم التقديم")]
            public string? SubmissionNumber { get; set; }

            [Display(Name = "الرقم المزيد لموضوع الكلي/الشامل")]
            public string? GeneralSubjectNumber { get; set; }

            [Display(Name = "رقم التكليف")]
            public string? AssignmentNumber { get; set; }

            [Display(Name = "وكلاء أوقاف في القرى")]
            public string? VillageEndowmentAgents { get; set; }

            [Display(Name = "عدول التروية")]
            public string? AdoulAlTaruya { get; set; }

            [Display(Name = "الفئة")]
            public string? Category { get; set; }

            [Display(Name = "تصنيف الفئة")]
            public string? CategoryClassification { get; set; }

            [Display(Name = "ملاحظات")]
            public string? Notes { get; set; }

            [Display(Name = "المساحة")]
            public decimal? Area { get; set; }

            [Display(Name = "رقم القطعة")]
            public string? PieceNumber { get; set; }

            [Display(Name = "استخدام القطعة")]
            public string? PieceUsage { get; set; }

            [Display(Name = "مساحة القطعة")]
            public decimal? PieceArea { get; set; }

            [Display(Name = "رقم الهوية العقارية")]
            public string? PropertyIdentityNumber { get; set; }

            [Display(Name = "الشماليات")]
            public string? NorthBoundaries { get; set; }

            [Display(Name = "الشرقيات")]
            public string? EastBoundaries { get; set; }

            [Display(Name = "الحقول الديناميكية")]
            public string? DynamicFields { get; set; }

        public List<LandBoundaryModel>? landBoundaryModels { get; set; }


    }
}
