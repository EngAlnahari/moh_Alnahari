using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class WorkWagesTaple
    {
        public int Id { get; set; }
       
        public string? EachTypeSimilar { get; set; } // كل نوع مشابه؟
        public string? WorkType { get; set; } 
        public int? Price { get; set; }//  السعر 
        public string? SpaceStandard { get; set; }//  معيار المساحة 
        public int? FromDate { get; set; }//   
        public int? ToDate { get; set; }//   
        public string? Notes { get; set; }
        public int? AlmjalID { get; set; }
        public int? WorkWagesTemplatesID { get; set; }
        [ForeignKey("AlmjalID")]
        public Almjal? almjal { get; set; }
        
        
        [ForeignKey("WorkWagesTemplatesID")]
        public WorkWagesTemplate? WorkWagesTemplates { get; set; }
         public SpecificSpaceTemplate? specificSpaceTemplate { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
    }
}
