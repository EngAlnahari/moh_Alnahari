namespace Test.Models
{
    public class OrderViewModel
    {

        // بيانات المستخدم
        public int UserId { get; set; }
        public string? UserName { get; set; }

        public List<TransportationScheduleTemplate>? TransportationSchedules { get; set; }
        public List<WorkWagesTemplate>? WorkWagesTemplates { get; set; }
        public List<SpecificSpaceTemplate>? SpecificSpaceTemplates { get; set; }

        // بيانات أجور العمل

    }

    

}
