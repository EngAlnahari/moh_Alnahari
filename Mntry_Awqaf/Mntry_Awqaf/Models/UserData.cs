using System.Text.Json.Serialization;

namespace Mntry_Awqaf.Models
{
    public class WorkOfferDataDto
    {
        public int Id { get; set; }
        public string OfferName { get; set; }

        // النسب المختلفة
        public int? userid { get; set; }
        public int? workofferid { get; set; }
        public decimal? OfferName1 { get; set; }
        public decimal? OfferName2 { get; set; }
        public decimal? OfferName3 { get; set; }
        public decimal? OfferName4 { get; set; }
        public decimal? OfferName5 { get; set; }
        public decimal? OfferName6 { get; set; }
        public List<ContractItemDto> ContractItems { get; set; }

        // القوائم
        public List<ApiTransportationSchedule> TransportationSchedules { get; set; } = new();
        public List<ApiWorkWagesTemplate> WorkWagesTemplates { get; set; } = new();
        public List<ApiSpecificSpaceTemplate> SpecificSpaceTemplates { get; set; } = new();
        public List<ApiAdditionalCost> AdditionalCosts { get; set; } = new();

        // المجموعات
        public decimal AdditionalCostsTotal { get; set; }
        public decimal? SelectedPrice { get; set; }
        public decimal? TotalTransportationCost { get; set; }
        public decimal? ToDate { get; set; }
        public decimal? FromDate { get; set; }

        // بيانات المستخدم
        public string UserName { get; set; }

        // الطلب (TanjezOrder)
        public ApiTanjezOrder TanjezOrder { get; set; }
    }
    public class ContractItemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("descriptionTemplate")]
        public string? DescriptionTemplate { get; set; }

        [JsonPropertyName("userID")]
        public int? UserID { get; set; }
    }
    // --------- Transportation -------------
    public class ApiTransportationSchedule
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string StatuseType { get; set; }

        public List<ApiTransportModel> TransportModels { get; set; } = new();

        public decimal? TotalTransportationFees { get; set; }
        public decimal? TotalOverNightFees { get; set; }
        public decimal? TotalAllowanceFees { get; set; }
        public decimal? TotalCost { get; set; }
    }

    public class ApiTransportModel
    {
        public int Id { get; set; }
        public string StatuseType1 { get; set; }
        public string StatuseType2 { get; set; }
        public string Governorate { get; set; }
        public decimal? TransportationFees { get; set; }
        public decimal? OverNightFees { get; set; }
        public decimal? AllowanceFees { get; set; }
        public bool IsSelected { get; set; }
    }

    // --------- WorkWages -------------
    public class ApiWorkWagesTemplate
    {
        public int Id { get; set; }
        public string WorkWagesName { get; set; }
        public string WorkWagesType { get; set; }
        public string Notes { get; set; }

        public List<ApiWorkWagesTable> WorkWagesTaples { get; set; } = new();
    }
    public class ApiTanjezOrder
    {
        public int Id { get; set; }
        public string CreateOrderName { get; set; }
        public string DescripPerson { get; set; }
        public string PurposeOrder { get; set; }
        public string TypePiece { get; set; }
        public string? PlaceName { get; set; } // اسم الموضع

        public string Country { get; set; }
        public string City { get; set; }
        public string Zone { get; set; }
        public string Village { get; set; }
        public string CoordinatesX { get; set; }
        public string CoordinatesY { get; set; }
       
        public string PlaceType { get; set; }
        public string DocumentType { get; set; }
        public int? AcountPiese { get; set; }
        public decimal? AreaDifferent { get; set; }
        public decimal? Space { get; set; }
        public string DocumentImage { get; set; }
        public string Alhiaza { get; set; }
        public string AccessRoadType { get; set; }
        public string PeriodType { get; set; }
        public string MaximumDuration { get; set; }
        public decimal? Width { get; set; }
        public decimal? Transportation { get; set; }
        public decimal? OverNight { get; set; }
        public decimal? Allowance { get; set; }
        public int? WorkOfferId { get; set; }
        public decimal? WorkCost { get; set; }
        public decimal? TransportationCost { get; set; }
        public decimal? AdditionalCost { get; set; }
        public decimal? TotalCost { get; set; }
        public string Status { get; set; }
        public int? UserID { get; set; }

        public List<ApiEarthBorder> EarthBorders { get; set; } = new();
    }

    public class ApiWorkWagesTable
    {
        public int Id { get; set; }
        public string EachTypeSimilar { get; set; }
        public string WorkType { get; set; }
        public decimal? Price { get; set; }
        public decimal? Price1 { get; set; }
        public string SpaceStandard { get; set; }
       
        public int? AlmjalID { get; set; }
        public string Notes { get; set; }
    }

    // --------- SpecificSpace -------------
    public class ApiSpecificSpaceTemplate
    {
        public int Id { get; set; }
        public int? Space { get; set; }
        public int? LessThan { get; set; }
        public int? BiggerThan { get; set; }
        public int? SpaceFrom { get; set; }
        public int? SpaceTo { get; set; }
        public decimal? Price { get; set; }
      
        public decimal? PriceWithDevice { get; set; }
        public string Notes { get; set; }
        public int? AlmjalID { get; set; }
    }

    // --------- AdditionalCost -------------
    public class ApiAdditionalCost
    {
        public int Id { get; set; }
        public string CostTypeName { get; set; }
        public decimal? Price { get; set; }
        public bool IsSelected { get; set; }
    }

    // --------- TanjezOrder + EarthBorders -------------
   
    public class ApiEarthBorder
    {
        public int Id { get; set; }
        public string BorderType { get; set; }
        public string BorderDescription { get; set; }
        public int? IdenticalOrDifferent { get; set; }
        public string Difference { get; set; }
    }
}
