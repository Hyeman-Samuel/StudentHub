using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentHub.Infrastructure.Migrations
{
    public partial class Seed_Admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "ede801f4-44bb-418e-9a3c-b2bacdfde397", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", 0, "e4b40761-aefa-4f7d-b7e6-252817ba729e", "Admin@StudentHub.com", false, false, null, "ADMIN@STUDENTHUB.COM", "ADMIN@STUDENTHUB.COM", "AQAAAAEAACcQAAAAEJMaeGDYLBF1i0mtGGkRnHoFppr/vAoYnS2huUFqziG8nGjr2OHiXCg4xFePi0CL2w==", null, false, 0, "ccc6ccb8-afb4-4d51-aafd-e21f7f92c8c9", false, "Admin@StudentHub.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", "341743f0-asd2–42de-afbf-59kmkkmk72cf6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", "341743f0-asd2–42de-afbf-59kmkkmk72cf6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "341743f0-asd2–42de-afbf-59kmkkmk72cf6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6");
        }
    }
}
