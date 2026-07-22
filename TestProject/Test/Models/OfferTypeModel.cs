using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class OfferTypeModel  // الأنواع (نوع العمل)
    {
        public int ID { get; set; }
        public string? TypeName { get; set; }
        public int? IsSelected { get; set; }

        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }
}
