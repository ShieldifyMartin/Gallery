using Microsoft.EntityFrameworkCore.Migrations;

namespace GalleryServer.Data.Migrations
{
    public partial class AddCollectionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Collection_CollectionId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collection",
                table: "Collection");

            migrationBuilder.RenameTable(
                name: "Collection",
                newName: "Collections");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Collections_CollectionId",
                table: "Posts",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Collections_CollectionId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Collections");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "Collection");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collection",
                table: "Collection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Collection_CollectionId",
                table: "Posts",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
