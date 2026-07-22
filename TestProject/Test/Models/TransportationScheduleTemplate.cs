using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class TransportationScheduleTemplate
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string? NameTemplete { get; set; } // المفتاح الرئيسي
        public string? StatuseType { get; set; } // المفتاح الرئيسي
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
        public List<WorkOffer>? workOffers { get; set; }

        public List<TransportModel>? transportModels { get; set; }
        //public ICollection<TransportationSchedule> Schedules { get; set; } // جدول فرعي
    }
}
