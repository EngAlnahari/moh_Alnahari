namespace Mntry_Awqaf.Models
{
    public class TransportModelWithDetails
    {
        public TransportationScheduleTemplate scheduleTemplate { get; set; } = new TransportationScheduleTemplate();
        public List<TransportModel> transportModels { get; set; } = new List<TransportModel>();
    }
}
