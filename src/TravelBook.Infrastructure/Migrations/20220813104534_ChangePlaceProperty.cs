using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBook.Infrastructure.Migrations
{
    public partial class ChangePlaceProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place_Street",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "Place_Street",
                table: "Photos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                column: "ConcurrencyStamp",
                value: "6e2aae7b-148c-4de8-addd-35041b6593ad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "39732592-5e8c-416c-b3f8-0cb53cf3c8a0", "AQAAAAEAACcQAAAAEMMfN8cZu4NiTvX/U60oCFRcQgfAcWW3PwLM68qxSMHRyrbcff7mz5DNKE7Gtt7+lg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Place_Street",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place_Street",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                column: "ConcurrencyStamp",
                value: "06bc27b5-ffc8-477e-92fc-b9a8dd7439b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bb0498ea-ee5c-422e-a4e8-f3eedb4e0e15", "AQAAAAEAACcQAAAAELh0xR+rmymrBv3CUOV6d2LKr3KKCWg1MoxQGbBQAlH1W5lq+hUJ/xcvS2EvJi8lnA==" });
        }
    }
}
