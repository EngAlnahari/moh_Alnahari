using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Almjal
    {
        public int ID { get; set; } // المفتاح الرئيسي
        public string AlmjalName { get; set; } // اسم المجال
        public SpecificSpaceTemplate? specificSpaceTemplate { get; set; }
        public WorkWagesTaple? workWagesTaple { get; set; }

    }
}
