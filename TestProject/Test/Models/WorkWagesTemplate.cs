
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class WorkWagesTemplate
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string WorkWagesName { get; set; } // اسم أجور العمل
        public string? WorkWagesType { get; set; } // نوع أجور العمل
        public int? EachTypeSimilar { get; set; } // كل نوع مشابه؟
        public string? Notes { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
        public List<WorkOffer>? workOffers { get; set; }

        public List<WorkWagesTaple>? workWagesTaples { get; set; }
    }

}
