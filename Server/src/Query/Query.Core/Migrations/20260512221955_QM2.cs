using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Query.Migrations
{
    /// <inheritdoc />
    public partial class QM2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "query",
                table: "users",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "query",
                table: "images",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "query",
                table: "categories",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "query",
                table: "albums",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "query",
                table: "users");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "query",
                table: "images");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "query",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "query",
                table: "albums");
        }
    }
}
