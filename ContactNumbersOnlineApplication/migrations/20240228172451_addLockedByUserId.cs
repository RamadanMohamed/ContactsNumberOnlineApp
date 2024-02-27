using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactNumbersOnlineApplication.Migrations
{
    /// <inheritdoc />
    public partial class addLockedByUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LockedByUserId",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockedByUserId",
                table: "Contacts");
        }
    }
}
