using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editplacetb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BestTimeForVisit",
                table: "Places",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountDisLike",
                table: "Places",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountLike",
                table: "Places",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThicketPrice",
                table: "Places",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitingHours",
                table: "Places",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "Places",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestTimeForVisit",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "CountDisLike",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "CountLike",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "ThicketPrice",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "VisitingHours",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Places");
        }
    }
}
