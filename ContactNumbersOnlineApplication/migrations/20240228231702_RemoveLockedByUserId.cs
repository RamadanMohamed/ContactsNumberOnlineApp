using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactNumbersOnlineApplication.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLockedByUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockedByUserId",
                table: "Contacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LockedByUserId",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
