using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpellShare.Migrations
{
    public partial class AddListGroupAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListGroupAllocations",
                columns: table => new
                {
                    ListGroupAllocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpellingGroupId = table.Column<int>(nullable: false),
                    SpellingListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListGroupAllocations", x => x.ListGroupAllocationId);
                    table.ForeignKey(
                        name: "FK_ListGroupAllocations_SpellingGroups_SpellingGroupId",
                        column: x => x.SpellingGroupId,
                        principalTable: "SpellingGroups",
                        principalColumn: "SpellingGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListGroupAllocations_SpellingLists_SpellingListId",
                        column: x => x.SpellingListId,
                        principalTable: "SpellingLists",
                        principalColumn: "SpellingListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListGroupAllocations_SpellingGroupId",
                table: "ListGroupAllocations",
                column: "SpellingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ListGroupAllocations_SpellingListId",
                table: "ListGroupAllocations",
                column: "SpellingListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListGroupAllocations");
        }
    }
}
