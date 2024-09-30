using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace News.Api.Data
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "websites",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_websites", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    websiteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_categories_websites_websiteId",
                        column: x => x.websiteId,
                        principalTable: "websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    websiteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_websites_websiteId",
                        column: x => x.websiteId,
                        principalTable: "websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    publishDateUtc = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    publishDateRaw = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    imgUrl = table.Column<string>(type: "text", nullable: false),
                    postUrl = table.Column<string>(type: "text", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    timezone = table.Column<string>(type: "text", nullable: false),
                    utcOffset = table.Column<string>(type: "text", nullable: false),
                    websiteId = table.Column<int>(type: "integer", nullable: false),
                    categoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_posts_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posts_websites_websiteId",
                        column: x => x.websiteId,
                        principalTable: "websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mainPageArticles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdAt = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    websiteId = table.Column<int>(type: "integer", nullable: false),
                    postId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mainPageArticles", x => x.id);
                    table.ForeignKey(
                        name: "FK_mainPageArticles_posts_postId",
                        column: x => x.postId,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mainPageArticles_websites_websiteId",
                        column: x => x.websiteId,
                        principalTable: "websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "poststags",
                columns: table => new
                {
                    postId = table.Column<int>(type: "integer", nullable: false),
                    tagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poststags", x => new { x.postId, x.tagId });
                    table.ForeignKey(
                        name: "FK_poststags_posts_postId",
                        column: x => x.postId,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_poststags_tags_tagId",
                        column: x => x.tagId,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_websiteId",
                table: "categories",
                column: "websiteId");

            migrationBuilder.CreateIndex(
                name: "IX_mainPageArticles_postId",
                table: "mainPageArticles",
                column: "postId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mainPageArticles_websiteId",
                table: "mainPageArticles",
                column: "websiteId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_categoryId",
                table: "posts",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_websiteId",
                table: "posts",
                column: "websiteId");

            migrationBuilder.CreateIndex(
                name: "IX_poststags_tagId",
                table: "poststags",
                column: "tagId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_websiteId",
                table: "tags",
                column: "websiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mainPageArticles");

            migrationBuilder.DropTable(
                name: "poststags");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "websites");
        }
    }
}
