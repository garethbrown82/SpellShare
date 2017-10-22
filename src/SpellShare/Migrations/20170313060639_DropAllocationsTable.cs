using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpellShare.Migrations
{
    public partial class DropAllocationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentGroupAllocations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentGroupAllocations",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    SpellingGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroupAllocations", x => new { x.StudentId, x.SpellingGroupId });
                    table.ForeignKey(
                        name: "FK_StudentGroupAllocations_SpellingGroups_SpellingGroupId",
                        column: x => x.SpellingGroupId,
                        principalTable: "SpellingGroups",
                        principalColumn: "SpellingGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroupAllocations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupAllocations_SpellingGroupId",
                table: "StudentGroupAllocations",
                column: "SpellingGroupId");
        }
    }
}
