using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Test.Models
{
    public class FamilyMember
    {
            public int Id { get; set; }
            public string Name { get; set; }

            public int? ParentId { get; set; }

            [JsonIgnore]
            public FamilyMember? Parent { get; set; }

            public ICollection<FamilyMember>? Children { get; set; }
        
    }
}
