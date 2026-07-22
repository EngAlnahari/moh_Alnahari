using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Models
{
    public class DynamicFieldValue
    {
        [Key]
        public int Id { get; set; }

        public int RecordId { get; set; }

        public string ScreenType { get; set; }

        public int DynamicFieldId { get; set; }

        public string Value { get; set; }
    }
}
