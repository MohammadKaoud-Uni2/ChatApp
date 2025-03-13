using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomUserCustomUser_AspNetUsers_LikedByUsersId",
                table: "CustomUserCustomUser");

            migrationBuilder.RenameColumn(
                name: "LikedByUsersId",
                table: "CustomUserCustomUser",
                newName: "LikedUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomUserCustomUser_LikedByUsersId",
                table: "CustomUserCustomUser",
                newName: "IX_CustomUserCustomUser_LikedUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomUserCustomUser_AspNetUsers_LikedUsersId",
                table: "CustomUserCustomUser",
                column: "LikedUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomUserCustomUser_AspNetUsers_LikedUsersId",
                table: "CustomUserCustomUser");

            migrationBuilder.RenameColumn(
                name: "LikedUsersId",
                table: "CustomUserCustomUser",
                newName: "LikedByUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomUserCustomUser_LikedUsersId",
                table: "CustomUserCustomUser",
                newName: "IX_CustomUserCustomUser_LikedByUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomUserCustomUser_AspNetUsers_LikedByUsersId",
                table: "CustomUserCustomUser",
                column: "LikedByUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
