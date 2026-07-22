using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mntry_Awqaf.Models
{
    public class WorkOffer
    {

        public int Id { get; set; }
        // معلومات العرض الرئيسية
        [Display(Name = "  اسم العرض")]
        public string? OfferName { get; set; }//  اسم العرض
        [Display(Name = "  تاريخ العرض")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "قالب اجور العمل")]
        public string? WagesTemplate { get; set; }
        [Display(Name = "التنقالات والمبيت")]
        public string? TransportTemplate { get; set; }
        [Display(Name = "اجر العمل")]
        public string? WorkWagesType { get; set; }
        [Display(Name = " شامل لكل الانواع")]
        public int? IsAllTypesIncluded { get; set; }

        [Display(Name = "نوع العمل")]
        public int? WorkWagesTemplateId { get; set; }
        [ForeignKey("WorkWagesTemplateId")]
        public WorkWagesTemplate? workWagesTemplate { get; set; }

        // التنقلات والمبيت
        public int? TransportationScheduleTemplateId { get; set; }
        [ForeignKey("TransportationScheduleTemplateId")]
        public TransportationScheduleTemplate? transportationScheduleTemplate { get; set; }

        //// التكاليف الإضافية
        [Display(Name = " التكاليف الإضافية")]
        public int? AdditionalCostTypeId { get; set; }
        [ForeignKey("AdditionalCostTypeId")]
        public AdditionalCostType? additionalCostType { get; set; }


        // الغرامات والمدفوعات
        [Display(Name = "نسبة الغرامة على التأخير")]
        public decimal? LatePenaltyPercentage { get; set; }

        [Display(Name = "نسبة الدفعة المقدمة")]
        public decimal? AdvancePaymentPercentage { get; set; }

        [Display(Name = "نسبة الاستقالة بعد الدفعة المقدمة")]
        public decimal? ResignationAfterAdvancePercentage { get; set; }

        [Display(Name = "نسبة الدفع في الموقع")]
        public decimal? OnSitePaymentPercentage { get; set; }

        [Display(Name = "نسبة غرامة استقالة الجمل")]
        public decimal? CamelResignationPenaltyPercentage { get; set; }

        [Display(Name = "نسبة الدفع بعد التسليم")]
        public decimal? AfterDeliveryPaymentPercentage { get; set; }
        //public decimal? LatePenaltyPercentage { get; set; }
        //public decimal? AdvancePaymentPercentage { get; set; }
        //public decimal? ResignationAfterAdvancePercentage { get; set; }
        //public decimal? OnSitePaymentPercentage { get; set; }
        //public decimal? CamelResignationPenaltyPercentage { get; set; }
        //public decimal? AfterDeliveryPaymentPercentage { get; set; }

        // الحقول الديناميكية للعرض
        public string? DynamicFields { get; set; }

        // ملاحظات
        public string? Notes { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        [JsonIgnore]
        public User? Users { get; set; }
    }
    //public class OfferTypeModel 
    //{
    //    public int Id { get; set; }
    //    public string? TypeName { get; set; }
    //    public bool?  IsSelected { get; set; }
    //}

    //public class TransportModel
    //{
    //    public int Id { get; set; }
    //    public string? Governorate { get; set; }
    //    public decimal? TransportationFees { get; set; }
    //    public decimal? AccommodationFees { get; set; }
    //    public decimal? AllowanceFees { get; set; }
    //    public bool? IsSelected { get; set; }
    //}

    //public class AdditionalCostModel
    //{
    //    [Key] // أضف هذه السمة
    //    public int Id { get; set; }
    //    public string? CostType { get; set; }
    //    public decimal? Price { get; set; }
    //    public bool? IsSelected { get; set; }
    //}
}
