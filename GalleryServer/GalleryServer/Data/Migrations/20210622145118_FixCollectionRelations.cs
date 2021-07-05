using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GalleryServer.Data.Migrations
{
    public partial class FixCollectionRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Collections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Collections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Collections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Collections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserId1",
                table: "Collections",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_UserId1",
                table: "Collections",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_UserId1",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_UserId1",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Collections");
        }
    }
}
