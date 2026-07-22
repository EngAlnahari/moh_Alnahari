namespace Mntry_Awqaf.Models
{
    public class TanjezOrderWithEarthBordersViewModel
    {
        public TanjezOrder tanjezOrder { get; set; } = new TanjezOrder();
        public List<EarthBorders> earthBorders { get; set; } = new List<EarthBorders>();
    }
}
