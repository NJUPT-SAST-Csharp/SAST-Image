using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations
{
    /// <inheritdoc />
    public partial class DM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "domain");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "domain",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "domain",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: false),
                    roles = table.Column<byte[]>(type: "smallint[]", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "albums",
                schema: "domain",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    access_level = table.Column<byte>(type: "smallint", nullable: false),
                    author_id = table.Column<long>(type: "bigint", nullable: false),
                    removed = table.Column<bool>(type: "boolean", nullable: false),
                    update_cover_automatically = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_albums", x => x.id);
                    table.ForeignKey(
                        name: "fk_albums_users_author_id",
                        column: x => x.author_id,
                        principalSchema: "domain",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identities",
                schema: "domain",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    provider = table.Column<byte>(type: "smallint", nullable: false),
                    provider_user_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identities", x => x.id);
                    table.ForeignKey(
                        name: "fk_identities_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "domain",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                schema: "domain",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    uploader_id = table.Column<long>(type: "bigint", nullable: false),
                    album_id = table.Column<long>(type: "bigint", nullable: false),
                    _status_value = table.Column<int>(type: "integer", nullable: true),
                    _status_removed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_albums_album_id",
                        column: x => x.album_id,
                        principalSchema: "domain",
                        principalTable: "albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_images_users_uploader_id",
                        column: x => x.uploader_id,
                        principalSchema: "domain",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscribes",
                schema: "domain",
                columns: table => new
                {
                    album = table.Column<long>(type: "bigint", nullable: false),
                    user = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscribes", x => new { x.user, x.album });
                    table.ForeignKey(
                        name: "fk_subscribes_albums_album",
                        column: x => x.album,
                        principalSchema: "domain",
                        principalTable: "albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subscribes_users_user",
                        column: x => x.user,
                        principalSchema: "domain",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "likes",
                schema: "domain",
                columns: table => new
                {
                    image = table.Column<long>(type: "bigint", nullable: false),
                    user = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_likes", x => new { x.user, x.image });
                    table.ForeignKey(
                        name: "fk_likes_images_image",
                        column: x => x.image,
                        principalSchema: "domain",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_likes_users_user",
                        column: x => x.user,
                        principalSchema: "domain",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_albums_author_id",
                schema: "domain",
                table: "albums",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_identities_user_id",
                schema: "domain",
                table: "identities",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_images_album_id",
                schema: "domain",
                table: "images",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "ix_images_uploader_id",
                schema: "domain",
                table: "images",
                column: "uploader_id");

            migrationBuilder.CreateIndex(
                name: "ix_likes_image",
                schema: "domain",
                table: "likes",
                column: "image");

            migrationBuilder.CreateIndex(
                name: "ix_subscribes_album",
                schema: "domain",
                table: "subscribes",
                column: "album");

            migrationBuilder.CreateIndex(
                name: "ix_users__username",
                schema: "domain",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "identities",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "likes",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "subscribes",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "images",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "albums",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "users",
                schema: "domain");
        }
    }
}
