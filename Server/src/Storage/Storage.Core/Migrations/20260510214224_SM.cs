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
            name: "access_control_list",
            schema: "storage",
            columns: table => new
            {
                resource_id = table.Column<long>(type: "bigint", nullable: false),
                users = table.Column<long[]>(type: "bigint[]", nullable: false),
                level = table.Column<byte>(type: "smallint", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_access_control_list", x => x.resource_id);
            }
        );

        migrationBuilder.CreateTable(
            name: "checkpoints",
            schema: "storage",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                timestamp = table.Column<DateTime>(
                    type: "timestamp with time zone",
                    nullable: false
                ),
                grain_id = table.Column<long>(type: "bigint", nullable: true),
                status = table.Column<byte>(type: "smallint", nullable: false),
                xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_checkpoints", x => x.id);
            }
        );

        migrationBuilder.CreateIndex(
            name: "ix_checkpoints_grain_id",
            schema: "storage",
            table: "checkpoints",
            column: "grain_id",
            unique: true
        );

        migrationBuilder.CreateIndex(
            name: "ix_checkpoints_timestamp",
            schema: "storage",
            table: "checkpoints",
            column: "timestamp",
            unique: true
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "access_control_list", schema: "storage");

        migrationBuilder.DropTable(name: "checkpoints", schema: "storage");
    }
}
