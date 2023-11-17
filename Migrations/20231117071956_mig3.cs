using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testem.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberTestId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MemberTests",
                columns: table => new
                {
                    MemberTestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    Passed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTests", x => x.MemberTestId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_MemberTestId",
                table: "Questions",
                column: "MemberTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_MemberTests_MemberTestId",
                table: "Questions",
                column: "MemberTestId",
                principalTable: "MemberTests",
                principalColumn: "MemberTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_MemberTests_MemberTestId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "MemberTests");

            migrationBuilder.DropIndex(
                name: "IX_Questions_MemberTestId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MemberTestId",
                table: "Questions");
        }
    }
}
