using System;
using Domain;
using Domain.Event;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class DM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "event");

            migrationBuilder.EnsureSchema(
                name: "domain");

            migrationBuilder.CreateTable(
                name: "events",
                schema: "event",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    grain_id = table.Column<long>(type: "bigint", nullable: false),
                    e_tag = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<DomainEventBase>(type: "jsonb", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => x.event_id);
                });

            migrationBuilder.CreateTable(
                name: "snapshots",
                schema: "domain",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    e_tag = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<DomainStateBase>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_snapshots", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_events_grain_id_e_tag",
                schema: "event",
                table: "events",
                columns: new[] { "grain_id", "e_tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_events_timestamp",
                schema: "event",
                table: "events",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "ix_snapshots_e_tag",
                schema: "domain",
                table: "snapshots",
                column: "e_tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events",
                schema: "event");

            migrationBuilder.DropTable(
                name: "snapshots",
                schema: "domain");
        }
    }
}
