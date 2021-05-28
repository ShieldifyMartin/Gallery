using Microsoft.EntityFrameworkCore.Migrations;

namespace GalleryServer.Data.Migrations
{
    public partial class AddCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CollectionId",
                table: "Posts",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Collection_CollectionId",
                table: "Posts",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Collection_CollectionId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CollectionId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Posts");
        }
    }
}
