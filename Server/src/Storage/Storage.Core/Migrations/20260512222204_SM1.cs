using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.Migrations
{
    /// <inheritdoc />
    public partial class SM1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_checkpoints_timestamp",
                schema: "storage",
                table: "checkpoints");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "storage",
                table: "access_control_list",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateIndex(
                name: "ix_checkpoints_timestamp",
                schema: "storage",
                table: "checkpoints",
                column: "timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_checkpoints_timestamp",
                schema: "storage",
                table: "checkpoints");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "storage",
                table: "access_control_list");

            migrationBuilder.CreateIndex(
                name: "ix_checkpoints_timestamp",
                schema: "storage",
                table: "checkpoints",
                column: "timestamp",
                unique: true);
        }
    }
}
