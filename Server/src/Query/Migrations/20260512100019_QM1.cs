using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Query.Migrations;

/// <inheritdoc />
public partial class QM1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_checkpoints_timestamp",
            schema: "query",
            table: "checkpoints"
        );

        migrationBuilder.CreateIndex(
            name: "ix_checkpoints_timestamp",
            schema: "query",
            table: "checkpoints",
            column: "timestamp"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_checkpoints_timestamp",
            schema: "query",
            table: "checkpoints"
        );

        migrationBuilder.CreateIndex(
            name: "ix_checkpoints_timestamp",
            schema: "query",
            table: "checkpoints",
            column: "timestamp",
            unique: true
        );
    }
}
