using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class User
    {
        public int ID { get; set; }
        [Display(Name = " السم الاول")]
        public string Frist_Name { get; set; }
        [Display(Name = " الاسم الثاني")]
        public string Second_Name { get; set; }
        [Display(Name = " الاسم الثالث")]
        public string Third_Name { get; set; }
        [Display(Name = " الاسم الرابع")]
        public string Fourth_Name { get; set; }
        [Display(Name = " اللقلب")]
        public string Nickname { get; set; }
        [Display(Name = "  صورة")]
        public string? Img { get; set; }
        [Display(Name = "  السيرة الذاتية")]
        public string? Cv { get; set; }
       
        public string? Is_Authentic { get; set; }
        [Display(Name = "  رقم البطاقة الشخصية")]
        public int? Number_card  { get; set; }
        [Display(Name = "  نوع المستخدم")]
        public string? User_type { get; set; } = "عميل";
        [Display(Name = "  رقم الجوال1")]
        public string Phone_number1  { get; set; }
        [Display(Name = "  رقم الجوال2")]
        public string? Phone_number2  { get; set; }
        [Display(Name = "  رقم الجوال3")]
        public string? Phone_number13 { get; set; }
        [Display(Name = "البريد الإلكتروني")]
        public string? Email { get; set; }
        //public string Email { get; set; }
        [Display(Name = "  كلمة المرور")]
        public string Pass { get; set; }

       

        [Display(Name = "الجنس")]
        public string Sex { get; set; }

        [Display(Name = "الجنسية")]
        public string Nationality { get; set; }

        [Display(Name = "الحالة الاجتماعية")]
        public string MaritalStatus { get; set; }

        [Display(Name = "تاريخ الميلاد")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "رقم جوال أحد الأقارب")]
        public string RelativePhone { get; set; }



        public List<TransportationScheduleTemplate>? transportationScheduleTemplates { get; set; }
        public List<WorkWagesTemplate>? workWagesTemplates { get; set; }
        public List<SpecificSpaceTemplate>? specificSpaceTemplates { get; set; }
        public List<OfferTypeModel>? offerTypeModels { get; set; }
       
        public List<TransportModel>? transportModels { get; set; }
        public List<WorkWagesTaple>? workWagesTaples { get; set; }
        public List<AdditionalCostType>? additionalCostTypes { get; set; }
       
       
        public List<AdditionalCostModel>?  additionalCostModels { get; set; }
        public List<WorkOffer>?  workOffers { get; set; }
        public List<EarthBorders>?  earthBorders { get; set; }
        public List<TanjezOrder>?  tanjezOrders { get; set; }

        //السيره الذاتيه
        public List<Experience>? experiences { get; set; }
        public List<Qualification>?  qualifications { get; set; }
        public List<Skill>?  skills { get; set; }
        public List<Certificate>?  certificates { get; set; }
        public List<Project>?   projects { get; set; }




        //public int? DepartID { get; set; }
        //[ForeignKey("DepartID")]
        //public Classification? Depart { get; set; }
    }
}
