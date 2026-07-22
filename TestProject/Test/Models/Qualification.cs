using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    // المؤهلات العلمية
    public class Qualification
    {
       
            public int Id { get; set; }

            [Display(Name = "المؤهل")]
            public string EduDegree { get; set; }       // المؤهل

            [Display(Name = "البلد")]
            public string EduCountry { get; set; }      // البلد

            [Display(Name = "الجامعة / المعهد")]
            public string EduUniv { get; set; }         // الجامعة / المعهد

            [Display(Name = "التخصص")]
            public string EduMajor { get; set; }        // التخصص

            [Display(Name = "القسم")]
            public string EduDept { get; set; }         // القسم

            [Display(Name = "سنة التخرج")]
            public string EduYear { get; set; }         // سنة التخرج

            [Display(Name = "المعدل / التقدير")]
            public string EduGrade { get; set; }        // المعدل / التقدير

            // العلاقة مع المستخدم
            [Display(Name = "المتقدم")]
            public int? UserID { get; set; }

            [ForeignKey("UserID")]
            public User? Users { get; set; }
        }

    }
