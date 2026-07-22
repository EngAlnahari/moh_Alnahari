using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "اسم المشروع")]
        public string? ProjName { get; set; }

        [Display(Name = "جهة العمل")]
        public string? ProjCompany { get; set; }

        [Display(Name = "المجال")]
        public string? ProjField { get; set; }

        [Display(Name = "من تاريخ")]
        public string? ProjFrom { get; set; }

        [Display(Name = "إلى تاريخ")]
        public string? ProjTo { get; set; }

        [Display(Name = "القيمة التقريبية")]
        public string? ProjValue { get; set; }

        [Display(Name = "الدور")]
        public string? ProjRole { get; set; }

        [Display(Name = "المتقدم")]
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }

}
