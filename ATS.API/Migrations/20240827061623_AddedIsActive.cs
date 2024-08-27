using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AccessPages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleName",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AccessPages");
        }
    }
}
