using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBreakingLikeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomUserCustomUser",
                columns: table => new
                {
                    LikedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikedByUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomUserCustomUser", x => new { x.LikedByUserId, x.LikedByUsersId });
                    table.ForeignKey(
                        name: "FK_CustomUserCustomUser_AspNetUsers_LikedByUserId",
                        column: x => x.LikedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomUserCustomUser_AspNetUsers_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomUserCustomUser_LikedByUsersId",
                table: "CustomUserCustomUser",
                column: "LikedByUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomUserCustomUser");
        }
    }
}
