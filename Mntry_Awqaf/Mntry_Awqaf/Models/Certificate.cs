using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class Certificate : IUserModel
    {
        public int Id { get; set; }

        [Display(Name = "النوع")]
        public string? Type { get; set; }

        [Display(Name = "المجال")]
        public string? Almgal { get; set; }

        [Display(Name = "القسم")]
        public string? Depart { get; set; }

      

        [Display(Name = "(اسم البرنامج/الدورة/الجهاز)")]
        public string? CertName { get; set; }

        [Display(Name = "الجهة المانحة")]
        public string? CertOrg { get; set; }

        [Display(Name = "سنة الحصول")]
        public string CertYear { get; set; }

        [Display(Name = "التصنيف")]
        public string CertType { get; set; }

        [Display(Name = "المتقدم")]
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }

}
