using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSystem.Data.Migrations
{
    public partial class Added_Properties_ToBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Books");
        }
    }
}
