using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSystem.Data.Migrations
{
    public partial class Added_IDeleteable_To_UserSLikesBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UsersBooksLikes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UsersBooksLikes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UsersBooksLikes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UsersBooksLikes");
        }
    }
}
