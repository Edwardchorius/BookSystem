using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSystem.Data.Migrations
{
    public partial class Added_ManytoMany_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "UsersBooksLikes",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    BookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersBooksLikes", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_UsersBooksLikes_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersBooksLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersBooksLikes_BookId",
                table: "UsersBooksLikes",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersBooksLikes");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }
    }
}
