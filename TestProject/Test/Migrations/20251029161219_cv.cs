using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Migrations
{
    /// <inheritdoc />
    public partial class cv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRoadType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessRoad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRoadType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Almjals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlmjalName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almjals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AreaStandardTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaStandardTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characteristicpeople",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristicpeople", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ReciveId = table.Column<int>(type: "int", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ownerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ownerships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionTypeId = table.Column<int>(type: "int", nullable: false),
                    AreaStandardTemplateId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReportFromInDay = table.Column<int>(type: "int", nullable: false),
                    ReportToInDay = table.Column<int>(type: "int", nullable: false),
                    ParentOfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "purposeSurveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purposeSurveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frist_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Second_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Third_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fourth_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Authentic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number_card = table.Column<int>(type: "int", nullable: true),
                    User_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_number1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_number2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_number13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    EngineerId = table.Column<int>(type: "int", nullable: true),
                    FirstParty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondParty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkContractDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WorkType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Space = table.Column<int>(type: "int", nullable: true),
                    Governorate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transportation = table.Column<int>(type: "int", nullable: true),
                    AdditionalCostsTotal = table.Column<int>(type: "int", nullable: true),
                    WorkCost = table.Column<int>(type: "int", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WorkContractTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    LatePenaltyPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdvancePaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResignationAfterAdvancePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnSitePaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CamelResignationPenaltyPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AfterDeliveryPaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workpaymentperiods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workpaymentperiods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassificationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlmjalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classification_Almjals_AlmjalId",
                        column: x => x.AlmjalId,
                        principalTable: "Almjals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreaStandardFromTo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AreaTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostOfSurveyingDevice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SurveyingDeviceCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParentTemplateId = table.Column<int>(type: "int", nullable: false),
                    AreaStandardTemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaStandardFromTo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaStandardFromTo_AreaStandardTemplate_AreaStandardTemplateId",
                        column: x => x.AreaStandardTemplateId,
                        principalTable: "AreaStandardTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "sectionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sectionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sectionTypes_sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "sections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdditionalCostType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalCostTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalCostType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalCostType_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Almgal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Depart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertOrg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StillWorking = table.Column<bool>(type: "bit", nullable: false),
                    MainTasks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OfferTypeModels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSelected = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTypeModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OfferTypeModels_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EduDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EduCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EduUniv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EduMajor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EduDept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EduYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EduGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TanjezOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateOrderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurposeOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypePiece = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Village = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordinatesX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordinatesY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcountPiese = table.Column<int>(type: "int", nullable: true),
                    AreaDifferent = table.Column<int>(type: "int", nullable: true),
                    Space = table.Column<int>(type: "int", nullable: false),
                    DocumentImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Directorate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alhiaza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessRoadType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumDuration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Transportation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverNight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allowance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOfferId = table.Column<int>(type: "int", nullable: true),
                    WorkCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransportationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdditionalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TanjezOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TanjezOrder_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TransportationScheduleTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTemplete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatuseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationScheduleTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationScheduleTemplate_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WorkWagesTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkWagesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkWagesType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EachTypeSimilar = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkWagesTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkWagesTemplate_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "contractAmendments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    EngineerId = table.Column<int>(type: "int", nullable: true),
                    AmendmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkContractId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractAmendments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contractAmendments_WorkContract_WorkContractId",
                        column: x => x.WorkContractId,
                        principalTable: "WorkContract",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "contractItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: true),
                    DescriptionTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    WorkContractId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contractItems_WorkContract_WorkContractId",
                        column: x => x.WorkContractId,
                        principalTable: "WorkContract",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdditionalCostModels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ducom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    IsSelected = table.Column<int>(type: "int", nullable: true),
                    AdditionalCostTypeId = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalCostModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdditionalCostModels_AdditionalCostType_AdditionalCostTypeId",
                        column: x => x.AdditionalCostTypeId,
                        principalTable: "AdditionalCostType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdditionalCostModels_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EarthBorders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorderDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdenticalOrDifferent = table.Column<int>(type: "int", nullable: true),
                    Difference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Difference1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Difference12 = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    TanjezOrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarthBorders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EarthBorders_TanjezOrder_TanjezOrderID",
                        column: x => x.TanjezOrderID,
                        principalTable: "TanjezOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EarthBorders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TransportModels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatuseType1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatuseType2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Governorate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportationFees = table.Column<int>(type: "int", nullable: true),
                    OverNightFees = table.Column<int>(type: "int", nullable: true),
                    AllowanceFees = table.Column<int>(type: "int", nullable: true),
                    IsSelected = table.Column<int>(type: "int", nullable: true),
                    TransportationScheduleTemplateID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransportModels_TransportationScheduleTemplate_TransportationScheduleTemplateID",
                        column: x => x.TransportationScheduleTemplateID,
                        principalTable: "TransportationScheduleTemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportModels_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WorkOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WagesTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkWagesType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAllTypesIncluded = table.Column<int>(type: "int", nullable: true),
                    WorkWagesTemplateId = table.Column<int>(type: "int", nullable: true),
                    TransportationScheduleTemplateId = table.Column<int>(type: "int", nullable: true),
                    AdditionalCostTypeId = table.Column<int>(type: "int", nullable: true),
                    LatePenaltyPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdvancePaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResignationAfterAdvancePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OnSitePaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CamelResignationPenaltyPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AfterDeliveryPaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOffer_AdditionalCostType_AdditionalCostTypeId",
                        column: x => x.AdditionalCostTypeId,
                        principalTable: "AdditionalCostType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOffer_TransportationScheduleTemplate_TransportationScheduleTemplateId",
                        column: x => x.TransportationScheduleTemplateId,
                        principalTable: "TransportationScheduleTemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOffer_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WorkOffer_WorkWagesTemplate_WorkWagesTemplateId",
                        column: x => x.WorkWagesTemplateId,
                        principalTable: "WorkWagesTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "workWagesTaples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EachTypeSimilar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Price1 = table.Column<int>(type: "int", nullable: true),
                    SpaceStandard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<int>(type: "int", nullable: true),
                    ToDate = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlmjalID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    WorkWagesTemplatesID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workWagesTaples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workWagesTaples_Almjals_AlmjalID",
                        column: x => x.AlmjalID,
                        principalTable: "Almjals",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_workWagesTaples_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_workWagesTaples_WorkWagesTemplate_WorkWagesTemplatesID",
                        column: x => x.WorkWagesTemplatesID,
                        principalTable: "WorkWagesTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "specificSpaceTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessThan = table.Column<int>(type: "int", nullable: true),
                    BiggerThan = table.Column<int>(type: "int", nullable: true),
                    SpaceFrom = table.Column<int>(type: "int", nullable: true),
                    SpaceTo = table.Column<int>(type: "int", nullable: true),
                    Space = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    FromDate = table.Column<int>(type: "int", nullable: true),
                    ToDate = table.Column<int>(type: "int", nullable: true),
                    PriceWithDevice = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlmjalID = table.Column<int>(type: "int", nullable: true),
                    WorkWagesTapleID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specificSpaceTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_specificSpaceTemplates_Almjals_AlmjalID",
                        column: x => x.AlmjalID,
                        principalTable: "Almjals",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_specificSpaceTemplates_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_specificSpaceTemplates_workWagesTaples_WorkWagesTapleID",
                        column: x => x.WorkWagesTapleID,
                        principalTable: "workWagesTaples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalCostModels_AdditionalCostTypeId",
                table: "AdditionalCostModels",
                column: "AdditionalCostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalCostModels_UserID",
                table: "AdditionalCostModels",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalCostType_UserID",
                table: "AdditionalCostType",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AreaStandardFromTo_AreaStandardTemplateId",
                table: "AreaStandardFromTo",
                column: "AreaStandardTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_UserID",
                table: "Certificates",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Classification_AlmjalId",
                table: "Classification",
                column: "AlmjalId");

            migrationBuilder.CreateIndex(
                name: "IX_contractAmendments_WorkContractId",
                table: "contractAmendments",
                column: "WorkContractId");

            migrationBuilder.CreateIndex(
                name: "IX_contractItems_WorkContractId",
                table: "contractItems",
                column: "WorkContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EarthBorders_TanjezOrderID",
                table: "EarthBorders",
                column: "TanjezOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_EarthBorders_UserID",
                table: "EarthBorders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserID",
                table: "Experiences",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ExperienceId",
                table: "Managers",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferTypeModels_UserID",
                table: "OfferTypeModels",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserID",
                table: "Projects",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_UserID",
                table: "Qualifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_sectionTypes_SectionId",
                table: "sectionTypes",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UserID",
                table: "Skills",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_specificSpaceTemplates_AlmjalID",
                table: "specificSpaceTemplates",
                column: "AlmjalID",
                unique: true,
                filter: "[AlmjalID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_specificSpaceTemplates_UserID",
                table: "specificSpaceTemplates",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_specificSpaceTemplates_WorkWagesTapleID",
                table: "specificSpaceTemplates",
                column: "WorkWagesTapleID",
                unique: true,
                filter: "[WorkWagesTapleID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TanjezOrder_UserID",
                table: "TanjezOrder",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationScheduleTemplate_UserID",
                table: "TransportationScheduleTemplate",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransportModels_TransportationScheduleTemplateID",
                table: "TransportModels",
                column: "TransportationScheduleTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_TransportModels_UserID",
                table: "TransportModels",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOffer_AdditionalCostTypeId",
                table: "WorkOffer",
                column: "AdditionalCostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOffer_TransportationScheduleTemplateId",
                table: "WorkOffer",
                column: "TransportationScheduleTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOffer_UserID",
                table: "WorkOffer",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOffer_WorkWagesTemplateId",
                table: "WorkOffer",
                column: "WorkWagesTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_workWagesTaples_AlmjalID",
                table: "workWagesTaples",
                column: "AlmjalID",
                unique: true,
                filter: "[AlmjalID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_workWagesTaples_UserID",
                table: "workWagesTaples",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_workWagesTaples_WorkWagesTemplatesID",
                table: "workWagesTaples",
                column: "WorkWagesTemplatesID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkWagesTemplate_UserID",
                table: "WorkWagesTemplate",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRoadType");

            migrationBuilder.DropTable(
                name: "AdditionalCostModels");

            migrationBuilder.DropTable(
                name: "AreaStandardFromTo");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Characteristicpeople");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "contractAmendments");

            migrationBuilder.DropTable(
                name: "contractItems");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "EarthBorders");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OfferTypeModels");

            migrationBuilder.DropTable(
                name: "ownerships");

            migrationBuilder.DropTable(
                name: "PlaceType");

            migrationBuilder.DropTable(
                name: "PriceTable");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "purposeSurveys");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "sectionTypes");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "specificSpaceTemplates");

            migrationBuilder.DropTable(
                name: "TransportModels");

            migrationBuilder.DropTable(
                name: "WorkOffer");

            migrationBuilder.DropTable(
                name: "Workpaymentperiods");

            migrationBuilder.DropTable(
                name: "AreaStandardTemplate");

            migrationBuilder.DropTable(
                name: "WorkContract");

            migrationBuilder.DropTable(
                name: "TanjezOrder");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "sections");

            migrationBuilder.DropTable(
                name: "workWagesTaples");

            migrationBuilder.DropTable(
                name: "AdditionalCostType");

            migrationBuilder.DropTable(
                name: "TransportationScheduleTemplate");

            migrationBuilder.DropTable(
                name: "Almjals");

            migrationBuilder.DropTable(
                name: "WorkWagesTemplate");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
