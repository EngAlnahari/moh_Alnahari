using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Test.Models
{
    public class WorkWagesTaple
    {
        public int Id { get; set; }
       
        public string? EachTypeSimilar { get; set; } // كل نوع مشابه؟
        public string? WorkType { get; set; } // كل نوع مشابه؟

        public int? Price { get; set; }//  السعر 
        public int? Price1 { get; set; }//  السعر 
        public string? SpaceStandard { get; set; }//  معيار المساحة 
        public int? FromDate { get; set; }//   من تاريخ 
        public int? ToDate { get; set; }//  الى تاريخ 
        public string? Notes { get; set; }

        public int? AlmjalID { get; set; }
        [ForeignKey("AlmjalID")]
        public Almjal? almjal { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? Users { get; set; }
        public int? WorkWagesTemplatesID { get; set; }
        [ForeignKey("WorkWagesTemplatesID")]
        [JsonIgnore]
        public WorkWagesTemplate? WorkWagesTemplates { get; set; }
        public SpecificSpaceTemplate? specificSpaceTemplate { get; set; }

    }
}
