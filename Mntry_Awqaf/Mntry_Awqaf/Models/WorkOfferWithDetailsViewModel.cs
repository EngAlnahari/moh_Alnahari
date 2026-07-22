namespace Mntry_Awqaf.Models
{
    public class WorkOfferWithDetailsViewModel
    {
        public WorkWagesTemplate OfferType { get; set; } = new WorkWagesTemplate();
        public List<WorkWagesTaple> WorkWages { get; set; } = new List<WorkWagesTaple>();
    }
}
