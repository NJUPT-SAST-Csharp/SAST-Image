using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations;

/// <inheritdoc />
public partial class DM2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameTable(
            name: "events",
            schema: "event",
            newName: "events",
            newSchema: "domain"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "event");

        migrationBuilder.RenameTable(
            name: "events",
            schema: "domain",
            newName: "events",
            newSchema: "event"
        );
    }
}
