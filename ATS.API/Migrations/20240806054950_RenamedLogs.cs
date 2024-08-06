using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATS.API.Migrations
{
    /// <inheritdoc />
    public partial class RenamedLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "AttendanceLogs",
                newName: "AttendanceLogTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttendanceLogTime",
                table: "AttendanceLogs",
                newName: "Time");
        }
    }
}
