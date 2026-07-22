namespace Mntry_Awqaf.Models
{
    public class SpecificwithAlmjal
    {
        public Almjal almjal { get; set; } = new Almjal();
        public List<SpecificSpaceTemplate> specificSpaceTemplates { get; set; } = new List<SpecificSpaceTemplate>();
    }
}
