using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarCoffee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePostCodeToPostalCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "CustomerAddresses",
                newName: "PostalCode");

            migrationBuilder.AddColumn<int>(
                name: "QuantityOnHand",
                table: "ProductInventorySnapshots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "ProductInventories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityOnHand",
                table: "ProductInventorySnapshots");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "ProductInventories");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "CustomerAddresses",
                newName: "PostCode");
        }
    }
}
