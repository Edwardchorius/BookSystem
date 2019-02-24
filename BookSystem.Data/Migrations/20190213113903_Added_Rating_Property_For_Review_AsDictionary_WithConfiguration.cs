using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSystem.Data.Migrations
{
    public partial class Added_Rating_Property_For_Review_AsDictionary_WithConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ratings",
                table: "Reviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ratings",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
