
namespace Mntry_Awqaf.Models
{
    public class LandBoundaryWithWorkForm
    {
        public WorkForm work_Form { get; set; } = new WorkForm();
        public List<LandBoundaryModel> landBoundaryModels { get; set; } = new List<LandBoundaryModel>();
    }
}
