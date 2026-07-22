namespace Mntry_Awqaf.Models
{
    public class WorkContract//عقد العمل
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public int? OrderId { get; set; }   // الطلب
        public int? OfferId { get; set; }   // العرض
        public int? ClientId { get; set; }  // العميل
        public int? EngineerId { get; set; } // المهندس
        public string? FirstParty { get; set; } // اسم الطرف الااول
        public string? SecondParty { get; set; } // اسم الطرف الثاني 
        public string? WorkContractDate { get; set; } // تاريخ العقد
        public ContractStatus Status { get; set; } = ContractStatus.PendingEngineerApproval;

        public string? WorkType { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public int? Space { get; set; }
        public string? Governorate { get; set; }
        public int? Transportation { get; set; }
        public int? AdditionalCostsTotal { get; set; }
        public int? WorkCost { get; set; }
        public decimal? TotalCost { get; set; } // التكلفة الإجمالية
        public TimeSpan? WorkContractTime { get; set; } // وقت العقد

        public decimal? LatePenaltyPercentage { get; set; }
        public decimal? AdvancePaymentPercentage { get; set; }
        public decimal? ResignationAfterAdvancePercentage { get; set; }
        public decimal? OnSitePaymentPercentage { get; set; }
        public decimal? CamelResignationPenaltyPercentage { get; set; }
        public decimal? AfterDeliveryPaymentPercentage { get; set; }
       public List<ContractItem>? ContractItems { get; set; }
        public List<ContractAmendment>? contractAmendments { get; set; }


    }
    public enum ContractStatus
    {
        PendingEngineerApproval,  // بانتظار موافقة المهندس
        EngineerApproved,         // موافقة المهندس
        EngineerRejected,         // رفض المهندس
        PendingClientFinalApproval, // بانتظار موافقة العميل النهائية
        FinalApproved,            // العقد أصبح معتمد
        ClientRejected            // العميل رفض بعد المهندس
    }

}

