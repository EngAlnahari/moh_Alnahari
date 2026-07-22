using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class SpecificSpaceTemplate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? LessThan { get; set; } // اقل من
        public int? BiggerThan { get; set; }//  اكثر من 
        public int? SpaceFrom { get; set; }//  من 
        public int? SpaceTo { get; set; }//  الى 
        public int? Space { get; set; }//   المساحة 
        public int? Price { get; set; }//   السعر 
        public int? FromDate { get; set; }//   السعر 
        public int? ToDate { get; set; }//  معيار المساحة 
        public int? PriceWithDevice { get; set; }//  السعر شامل اجور الجهاز 
        public string? Notes { get; set; }

        public int? AlmjalID { get; set; }
        [ForeignKey("AlmjalID")]
        public Almjal? almjal { get; set; }
        public int? WorkWagesTapleID { get; set; }
        [ForeignKey("WorkWagesTapleID")]
        public WorkWagesTaple? workWagesTaple { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }
}
