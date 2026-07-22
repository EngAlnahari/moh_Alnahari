using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class TransportationSchedule
    {
        public int Id { get; set; } // المفتاح الرئيسي
        public string Title { get; set; } // عنوان العرض
        public DateTime EndDate { get; set; } // تاريخ انتهاء العرض
        public int WorkWagesTemplateId { get; set; } // ربط Link → قالب أجور العمل
        public int TransportationScheduleTemplateId { get; set; } // ربط Link → قالب جدول التنقلات
        public decimal FirstPayment { get; set; } // نسبة الدفع مقدماً
        public decimal WorkStartingPayment { get; set; } // نسبة بدء العمل
        public decimal WorkCompletionPayment { get; set; } // نسبة إتمام العمل
        public decimal FinesWorkLate { get; set; } // غرامات التأخير
        public decimal EngineerApologyFines { get; set; } // غرامات اعتذار المهندس
        public decimal CustomerApologyFines { get; set; } // غرامات اعتذار العميل
        public string Note { get; set; } // ملاحظة قصيرة
        public int? AmendedFromId { get; set; } // ربط Link → عرض العمل
    }
}
