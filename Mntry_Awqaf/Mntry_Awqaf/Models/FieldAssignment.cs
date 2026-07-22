using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class FieldAssignment
    {
        [Key]
        public int Id { get; set; }

        // Category matching from almjal1.html
        public string MainCategory { get; set; }
        public string Specialty { get; set; }
        public string Branch { get; set; }
        public string Detail { get; set; }

        // Screen to which this field is assigned: TanjezOrder, WorkForm, WorkOffer, WorkContract
        [Required]
        public string ScreenType { get; set; }

        public string? GroupName { get; set; } // e.g. "موقع الأرض", "بيانات الوثيقة"

        public int DynamicFieldId { get; set; }

        [ForeignKey("DynamicFieldId")]
        public DynamicField DynamicField { get; set; }
    }
}
