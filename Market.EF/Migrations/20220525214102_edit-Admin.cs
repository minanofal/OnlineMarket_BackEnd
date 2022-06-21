using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.EF.Migrations
{
    public partial class editAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4b04b503-e4af-47bb-80bb-fdd772271c38", "b3210a85-5ae6-4f86-872d-8d45cd27d4e8" });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "b3210a85-5ae6-4f86-872d-8d45cd27d4e8");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4b04b503-e4af-47bb-80bb-fdd772271c38", "34bc91a8-c540-4924-be0d-7980bdae525e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4b04b503-e4af-47bb-80bb-fdd772271c38", "34bc91a8-c540-4924-be0d-7980bdae525e" });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4b04b503-e4af-47bb-80bb-fdd772271c38", "b3210a85-5ae6-4f86-872d-8d45cd27d4e8" });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b3210a85-5ae6-4f86-872d-8d45cd27d4e8", 0, "dedbd746-3272-436d-b819-a28ae15aa8ce", "Admin@test.com", false, "Admin", "Admin", false, null, null, null, "A6t7STkEWW0LKLY9IRpy9aQBKidCB4yfxfSAf3V2n/g=", null, false, "OJTHDYE47EKVFIBS2BIKVWJXGC3KRQKT", false, "Admin" });
        }
    }
}
