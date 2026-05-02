using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.Migrations;

/// <inheritdoc />
public partial class SM : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "storage");

        migrationBuilder.CreateTable(
            name: "messages",
            schema: "storage",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                status = table.Column<byte>(type: "smallint", nullable: false),
                retry_count = table.Column<int>(type: "integer", nullable: false),
                retry_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                type = table.Column<string>(
                    type: "character varying(21)",
                    maxLength: 21,
                    nullable: false
                ),
                album = table.Column<long>(type: "bigint", nullable: true),
                album_cover_updated_message_file = table.Column<string>(
                    type: "text",
                    nullable: true
                ),
                file = table.Column<string>(type: "text", nullable: true),
                image_added_message_image_id = table.Column<long>(type: "bigint", nullable: true),
                image_id = table.Column<long>(type: "bigint", nullable: true),
                image = table.Column<string>(type: "text", nullable: true),
                user_id = table.Column<long>(type: "bigint", nullable: true),
                header_updated_message_image = table.Column<string>(type: "text", nullable: true),
                header_updated_message_user_id = table.Column<long>(type: "bigint", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_messages", x => x.id);
            }
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "messages", schema: "storage");
    }
}
