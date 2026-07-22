using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Test.Models
{
    public class AdditionalCostModel  // التكاليف الإضافية
    {
        public int ID { get; set; }
        public string? CostType { get; set; }
        public string? Img { get; set; }
        public string? Ducom { get; set; }
        public int? Price { get; set; }
        public int? IsSelected { get; set; }
        public int? AdditionalCostTypeId { get; set; }
        [ForeignKey("AdditionalCostTypeId")]
        [JsonIgnore]
        public AdditionalCostType? additionalCostType { get; set; }// إظهار لجميع المستخدمين
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }
}
