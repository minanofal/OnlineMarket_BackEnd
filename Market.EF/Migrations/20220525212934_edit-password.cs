using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.EF.Migrations
{
    public partial class editpassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "b3210a85-5ae6-4f86-872d-8d45cd27d4e8",
                column: "PasswordHash",
                value: "A6t7STkEWW0LKLY9IRpy9aQBKidCB4yfxfSAf3V2n/g=");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "b3210a85-5ae6-4f86-872d-8d45cd27d4e8",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEMx0tQdok3cRuZSaTb9Rks0KpLvx7xIsLp++Yo2fFTQQ0ahMaM0WHXITXFrAxFTilA==");
        }
    }
}
