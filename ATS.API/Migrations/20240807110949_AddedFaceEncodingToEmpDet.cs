using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedFaceEncodingToEmpDet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FaceEncoding",
                table: "EmployeeDetails",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceEncoding",
                table: "EmployeeDetails");
        }
    }
}
