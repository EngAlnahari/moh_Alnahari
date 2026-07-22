using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class AdditionalCostType
    {
        [Key] // أضف هذه السمة
        public int Id { get; set; }
        public string AdditionalCostTypeName { get; set; } // اسم التكلفة الإضافية
        public List<AdditionalCostModel>? additionalCostModels { get; set; }
        public List<WorkOffer>? workOffers { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }// إظهار لجميع المستخدمين
    }

}
