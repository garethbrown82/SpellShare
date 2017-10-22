using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpellShare.Migrations
{
    public partial class RebuildAllocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentGroupAllocations",
                columns: table => new
                {
                    StudentGroupAllocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpellingGroupId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroupAllocations", x => x.StudentGroupAllocationId);
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

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupAllocations_StudentId",
                table: "StudentGroupAllocations",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentGroupAllocations");
        }
    }
}
