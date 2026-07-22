namespace Mntry_Awqaf.Models
{
    public class AdditionalCostWithModel
    {
        public AdditionalCostType AdditionalCost { get; set; } = new AdditionalCostType();
        public List<AdditionalCostModel> AdditionalCostModeles { get; set; } = new List<AdditionalCostModel>();
    }
}
