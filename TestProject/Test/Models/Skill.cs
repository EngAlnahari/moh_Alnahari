using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    // المهارات التقنية
    public class Skill
    {
        public int Id { get; set; }

        [Display(Name = "أدخل المهارة")]
        public string? SkillName { get; set; }

        [Display(Name = "المجال")]
        public string? SkillField { get; set; }

        [Display(Name = "من")]
        public string? SkillFrom { get; set; }

        [Display(Name = "إلى")]
        public string? SkillTo { get; set; }

        [Display(Name = "الوصف")]
        public string? SkillDesc { get; set; }

        [Display(Name = "المتقدم")]
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }

}
