using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.EF.Migrations
{
    public partial class SetAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4b04b503-e4af-47bb-80bb-fdd772271c38", "60fa9d01-9d0a-4670-b52f-9d5246dfe404" });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4b04b503-e4af-47bb-80bb-fdd772271c38", "f683ab07-2020-4371-8839-07edc7e9d77a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4b04b503-e4af-47bb-80bb-fdd772271c38", "f683ab07-2020-4371-8839-07edc7e9d77a" });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4b04b503-e4af-47bb-80bb-fdd772271c38", "60fa9d01-9d0a-4670-b52f-9d5246dfe404" });
        }
    }
}
