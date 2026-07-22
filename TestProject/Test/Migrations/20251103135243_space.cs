using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Migrations
{
    /// <inheritdoc />
    public partial class space : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Count",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data_Brop",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direct_Brop",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Down",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inv_From_1",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inv_From_2",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_Inv",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Num_Brop",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Num_Brop1",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place_Inv",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summray_Inv",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topic_Inv",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_Area_Inv",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Up",
                table: "TanjezOrder",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Data_Brop",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Direct_Brop",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Down",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Inv_From_1",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Inv_From_2",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Name_Inv",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Num_Brop",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Num_Brop1",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Place_Inv",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Summray_Inv",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Topic_Inv",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Type_Area_Inv",
                table: "TanjezOrder");

            migrationBuilder.DropColumn(
                name: "Up",
                table: "TanjezOrder");
        }
    }
}
