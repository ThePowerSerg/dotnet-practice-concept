using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserName",
                keyValue: "sergferreira81",
                column: "Country",
                value: "United States");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserName",
                keyValue: "sergiof810",
                column: "Country",
                value: "United States");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserProfiles");
        }
    }
}
