using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }

        //public int? DepartID { get; set; }
        //[ForeignKey("DepartID")]
        //public Classification? Depart { get; set; }
    }
}
