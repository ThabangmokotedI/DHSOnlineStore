using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHSOnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class CartDetailsICollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Stocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Stocks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
