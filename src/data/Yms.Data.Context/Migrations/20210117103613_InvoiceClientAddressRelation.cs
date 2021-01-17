using Microsoft.EntityFrameworkCore.Migrations;

namespace Yms.Data.Context.Migrations
{
    public partial class InvoiceClientAddressRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientAddressId",
                schema: "Order",
                table: "Invoices",
                column: "ClientAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_ClientAddresses_ClientAddressId",
                schema: "Order",
                table: "Invoices",
                column: "ClientAddressId",
                principalSchema: "Management",
                principalTable: "ClientAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_ClientAddresses_ClientAddressId",
                schema: "Order",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ClientAddressId",
                schema: "Order",
                table: "Invoices");
        }
    }
}
