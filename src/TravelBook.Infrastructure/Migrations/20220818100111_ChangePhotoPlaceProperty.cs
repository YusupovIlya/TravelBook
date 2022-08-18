using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBook.Infrastructure.Migrations
{
    public partial class ChangePhotoPlaceProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place_City",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Place_Country",
                table: "Photos",
                newName: "Place");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                column: "ConcurrencyStamp",
                value: "66bf1830-46ae-4289-8e88-a84db793bd97");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8052c609-e1f1-4e00-8d68-a496749bd774", "AQAAAAEAACcQAAAAED8wAs3XkdgN9asb75tdqHP5TLUsQCB22lTosTuBZ1aPXskldM/vKS6I4qv0/jVLeA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Place",
                table: "Photos",
                newName: "Place_Country");

            migrationBuilder.AddColumn<string>(
                name: "Place_City",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                column: "ConcurrencyStamp",
                value: "ceed2b9a-470a-4f2b-b796-eca34bdeaf42");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "441578f9-1c4f-422b-ab37-5b8c20354451", "AQAAAAEAACcQAAAAEKZOLHJT2G1zasIAS3vPvRCyL7XjcQ3rMcLwxedDqTV6i1HDohp8E5vCEbAKXPZyoA==" });
        }
    }
}
