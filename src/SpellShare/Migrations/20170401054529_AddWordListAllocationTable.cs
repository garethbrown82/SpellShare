using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpellShare.Migrations
{
    public partial class AddWordListAllocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordListAllocations",
                columns: table => new
                {
                    WordListAllocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpellingListId = table.Column<int>(nullable: false),
                    SpellingWordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordListAllocations", x => x.WordListAllocationId);
                    table.ForeignKey(
                        name: "FK_WordListAllocations_SpellingLists_SpellingListId",
                        column: x => x.SpellingListId,
                        principalTable: "SpellingLists",
                        principalColumn: "SpellingListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordListAllocations_SpellingWords_SpellingWordId",
                        column: x => x.SpellingWordId,
                        principalTable: "SpellingWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordListAllocations_SpellingListId",
                table: "WordListAllocations",
                column: "SpellingListId");

            migrationBuilder.CreateIndex(
                name: "IX_WordListAllocations_SpellingWordId",
                table: "WordListAllocations",
                column: "SpellingWordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordListAllocations");
        }
    }
}
