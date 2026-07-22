using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Manager
    {
        public int Id { get; set; }

        [Display(Name = "الاسم")]
        public string Name { get; set; }

        [Display(Name = "الرقم")]
        public string Number { get; set; }

        [Display(Name = "الدرجة الوظيفية")]
        public string JobGrade { get; set; }

        [Display(Name = "المتقدم")]
        public int? ExperienceId { get; set; }
        [ForeignKey("ExperienceId")]
        public Experience? GetExperience { get; set; }
    }

}
