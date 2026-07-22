using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Test.Models
{
    public class ContractAmendment
    {
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public int? ClientId { get; set; }
        public int? EngineerId { get; set; }
        public string AmendmentType { get; set; } // Objection, Modification, Addition
        public string? FieldName { get; set; }    // اسم البند (للتعديل فقط)
        public string? NewValue { get; set; }     // القيمة الجديدة
        public string Description { get; set; }   // تفاصيل الاعتراض أو البند الجديد
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? WorkContractId { get; set; }
        [ForeignKey("WorkContractId")]
        [JsonIgnore]
        public WorkContract? workContracts { get; set; }
    }

}
