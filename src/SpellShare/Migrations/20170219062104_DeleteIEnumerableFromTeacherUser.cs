using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpellShare.Migrations
{
    public partial class DeleteIEnumerableFromTeacherUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_SpellShareUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SpellShareUserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SpellShareUserId",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpellShareUserId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SpellShareUserId",
                table: "Students",
                column: "SpellShareUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_SpellShareUserId",
                table: "Students",
                column: "SpellShareUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
