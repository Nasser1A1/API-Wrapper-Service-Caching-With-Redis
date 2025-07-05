using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather_API_Wrapper_Service.Data.Migrations.App
{
    /// <inheritdoc />
    public partial class ChangeDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_weatherLogs",
                table: "weatherLogs");

            migrationBuilder.RenameTable(
                name: "weatherLogs",
                newName: "WeatherLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherLogs",
                table: "WeatherLogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherLogs",
                table: "WeatherLogs");

            migrationBuilder.RenameTable(
                name: "WeatherLogs",
                newName: "weatherLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_weatherLogs",
                table: "weatherLogs",
                column: "Id");
        }
    }
}
