using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserDetailsIdToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_UserId",
                table: "EmployeeDetails");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeDetailsId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_UserId",
                table: "EmployeeDetails",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_UserId",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeDetailsId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_UserId",
                table: "EmployeeDetails",
                column: "UserId");
        }
    }
}
