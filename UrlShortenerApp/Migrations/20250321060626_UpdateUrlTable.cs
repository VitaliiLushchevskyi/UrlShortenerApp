using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UrlShortenerApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrlTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3c8c56f-5e5a-41b0-946d-c3b628bd2c4d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d8a4723d-1bff-49f2-9029-fd2069f0f255", "f1aab8d3-8772-41a9-9481-4cd59734ad29" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8a4723d-1bff-49f2-9029-fd2069f0f255");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1aab8d3-8772-41a9-9481-4cd59734ad29");

            migrationBuilder.DropColumn(
                name: "ClicksCount",
                table: "Urls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClicksCount",
                table: "Urls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b3c8c56f-5e5a-41b0-946d-c3b628bd2c4d", null, "User", "USER" },
                    { "d8a4723d-1bff-49f2-9029-fd2069f0f255", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f1aab8d3-8772-41a9-9481-4cd59734ad29", 0, "d38586f0-def2-4c3b-bbdd-ddebb232df25", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMK4Ke+eUqMy6Lhp5X6+e/yFre063T0erOUSE7GgUpyW1m/rBU/jcpAPyto4yMGecw==", null, false, "76c8cfe5-6b28-45d7-9c39-27dd97d7d9f3", false, "admin@example.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d8a4723d-1bff-49f2-9029-fd2069f0f255", "f1aab8d3-8772-41a9-9481-4cd59734ad29" });
        }
    }
}
