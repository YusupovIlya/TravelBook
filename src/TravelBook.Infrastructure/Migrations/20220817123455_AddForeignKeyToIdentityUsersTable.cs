using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBook.Infrastructure.Migrations
{
    public partial class AddForeignKeyToIdentityUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Travels",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_Travels_UserId",
                table: "Travels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_AspNetUsers_UserId",
                table: "Travels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travels_AspNetUsers_UserId",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_UserId",
                table: "Travels");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                column: "ConcurrencyStamp",
                value: "7f66b294-c9f8-41d9-9627-c66e5407cc42");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c8990a3e-4c22-42d4-96d3-6f48378cc1b5", "AQAAAAEAACcQAAAAEA1PlQ6UdGpp67LJvxzozxY3x9y0/BiEx+IbvMUmPsW0KnUR/wvM/zZ721qZ4HCa0A==" });
        }
    }
}
