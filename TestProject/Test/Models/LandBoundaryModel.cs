using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class LandBoundaryModel
    {
        public int Id { get; set; }
        [Display(Name = "الصفة")]
        public string? AttributeName { get; set; }

        [Display(Name = "الحد الشمالي")]
        public string? NorthBoundary { get; set; }

        [Display(Name = "الحد الجنوبي")]
        public string? SouthBoundary { get; set; }

        [Display(Name = "الحد الغربي")]
        public string? WestBoundary { get; set; }

        [Display(Name = "الحد الشرقي")]
        public string? EastBoundary { get; set; }
        public int? WorkFormId { get; set; }
        [ForeignKey("WorkFormId")]
        public WorkForm? WorkForms { get; set; }
    }
}
