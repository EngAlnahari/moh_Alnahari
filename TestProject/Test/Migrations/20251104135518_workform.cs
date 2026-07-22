using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Migrations
{
    /// <inheritdoc />
    public partial class workform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmissionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralSubjectNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VillageEndowmentAgents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdoulAlTaruya = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PieceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PieceUsage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PieceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PropertyIdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NorthBoundaries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EastBoundaries = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LandBoundaryModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NorthBoundary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SouthBoundary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WestBoundary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EastBoundary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkFormId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandBoundaryModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LandBoundaryModel_WorkForms_WorkFormId",
                        column: x => x.WorkFormId,
                        principalTable: "WorkForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LandBoundaryModel_WorkFormId",
                table: "LandBoundaryModel",
                column: "WorkFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandBoundaryModel");

            migrationBuilder.DropTable(
                name: "WorkForms");
        }
    }
}
