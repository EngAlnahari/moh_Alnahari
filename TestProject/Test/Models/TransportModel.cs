using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Test.Models
{
    public class TransportModel  // التنقلات والمبيت
    {
        public int ID { get; set; }
        public string? StatuseType1 { get; set; }// الحالة 
        public string? StatuseType2 { get; set; }// الحالة 
        public string? Governorate { get; set; }// المحافظة 
        public int? TransportationFees { get; set; }// اجر التنقلات 
        public int? OverNightFees { get; set; }// اجر المبيت 
        public int? AllowanceFees { get; set; }//  اجر النثريات
        public int? IsSelected { get; set; }
      
      

        public int? TransportationScheduleTemplateID { get; set; }
        [ForeignKey("TransportationScheduleTemplateID")]
        [JsonIgnore]

        public TransportationScheduleTemplate? transportationScheduleTemplate { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }
}
