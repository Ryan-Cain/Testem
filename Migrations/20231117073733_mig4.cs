using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testem.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MemberTests_MemberId",
                table: "MemberTests",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberTests_Members_MemberId",
                table: "MemberTests",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberTests_Members_MemberId",
                table: "MemberTests");

            migrationBuilder.DropIndex(
                name: "IX_MemberTests_MemberId",
                table: "MemberTests");
        }
    }
}
