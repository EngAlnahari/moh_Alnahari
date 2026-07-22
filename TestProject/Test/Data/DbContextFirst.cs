using Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Test.Data
{
    public class DbContextFirst:DbContext
    {
        public DbContextFirst(DbContextOptions<DbContextFirst> options):base(options) { }

        public DbSet<Classification> Classification { get; set; }
       
        public DbSet<SectionType> sectionTypes { get; set; }
        //public DbSet<Category> Categories { get; set; }
       
        public DbSet<AdditionalCostType> AdditionalCostType { get; set; }
        public DbSet<AreaStandardFromTo> AreaStandardFromTo { get; set; }
        public DbSet<AreaStandardTemplate> AreaStandardTemplate { get; set; }
        public DbSet<PriceTable> PriceTable { get; set; }
        public DbSet<WorkWagesTemplate> WorkWagesTemplate { get; set; }
        public DbSet<TransportationScheduleTemplate> TransportationScheduleTemplate { get; set; }
        //public DbSet<Transportation> TransportationSchedule { get; set; }
        public DbSet<WorkOffer> WorkOffer { get; set; }
        public DbSet<Section> sections { get; set; }
        public DbSet<EarthBorders> EarthBorders { get; set; }
        public DbSet<WorkContract> WorkContract { get; set; }
        public DbSet<TransportModel> TransportModels { get; set; }
        public DbSet<WorkWagesTemplate> workWagesTemplates { get; set; }
        public DbSet<WorkWagesTaple> workWagesTaples { get; set; }
        public DbSet<Almjal> Almjals { get; set; }
        public DbSet<SpecificSpaceTemplate> specificSpaceTemplates { get; set; }
        public DbSet<AdditionalCostModel> AdditionalCostModels { get; set; }
        public DbSet<OfferTypeModel> OfferTypeModels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TanjezOrder> TanjezOrder { get; set; } = default!;


        public DbSet<AccessRoadType> AccessRoadType { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }

        public DbSet<PlaceType> PlaceType { get; set; }
        public DbSet<Workpaymentperiod> Workpaymentperiods { get; set; } 
        public DbSet<Characteristicperson> Characteristicpeople { get; set; } 
        public DbSet<ownership> ownerships { get; set; } 
        public DbSet<PurposeSurvey> purposeSurveys { get; set; } 
        public DbSet<Notification> Notifications { get; set; } 
        public DbSet<ContractAmendment> contractAmendments { get; set; } 
        public DbSet<ContractItem> contractItems { get; set; } 

        public DbSet<Project> Projects { get; set; } 
        public DbSet<Certificate> Certificates { get; set; } 
        public DbSet<Skill> Skills { get; set; } 
        public DbSet<Qualification> Qualifications { get; set; } 
        public DbSet<Experience> Experiences { get; set; } 
        public DbSet<Manager> Managers { get; set; } 
        public DbSet<WorkForm> WorkForms  { get; set; } 
        public DbSet<Land> Lands { get; set; } 
        public DbSet<Test.Models.LandBoundaryModel> LandBoundaryModel { get; set; } = default!;
        public DbSet<TestProject.Models.DynamicFieldValue> DynamicFieldValues { get; set; } = default!;

    }
}
