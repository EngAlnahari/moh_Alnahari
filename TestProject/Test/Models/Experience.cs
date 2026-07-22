using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    // الخبرات العملية
    public class Experience
    {
        public int Id { get; set; }

        [Display(Name = "جهة العمل")]
        public string? Company { get; set; }

        [Display(Name = "المسمى الوظيفي")]
        public string? JobTitle { get; set; }

        [Display(Name = "المجال")]
        public string? Field { get; set; }

        [Display(Name = "من تاريخ")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "إلى تاريخ")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "ما زلت أعمل حالياً")]
        public int? StillWorking { get; set; }

        [Display(Name = "المهام الرئيسية")]
        public string? MainTasks { get; set; }

        public List<Manager>? ManagerList { get; set; }

        [Display(Name = "المتقدم")]
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }


}
