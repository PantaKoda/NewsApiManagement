using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Api.Data
{
    /// <inheritdoc />
    public partial class AddIndexColumnsPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_posts_websiteId",
                table: "posts");

            migrationBuilder.CreateIndex(
                name: "IX_posts_websiteId_publishDateUtc",
                table: "posts",
                columns: new[] { "websiteId", "publishDateUtc" },
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_posts_websiteId_publishDateUtc",
                table: "posts");

            migrationBuilder.CreateIndex(
                name: "IX_posts_websiteId",
                table: "posts",
                column: "websiteId");
        }
    }
}
