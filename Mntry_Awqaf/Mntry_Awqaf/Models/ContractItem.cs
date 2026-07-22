using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mntry_Awqaf.Models
{
    public class ContractItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Count { get; set; }
        public string? DescriptionTemplate { get; set; }
        public int? UserID { get; set; }
        public int? WorkContractId { get; set; }
        [ForeignKey("WorkContractId")]
        public WorkContract? workContracts { get; set; }
    }
}
