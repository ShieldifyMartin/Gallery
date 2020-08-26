using Microsoft.EntityFrameworkCore.Migrations;

namespace GalleryServer.Data.Migrations
{
    public partial class AddTagsListToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "679058ff-bb65-4081-8f2d-8c59da9f15b5", "193da50c-0ff7-4e83-b68a-f7888490e16f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "679058ff-bb65-4081-8f2d-8c59da9f15b5");
        }
    }
}
