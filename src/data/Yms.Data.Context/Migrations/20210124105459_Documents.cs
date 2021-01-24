using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yms.Data.Context.Migrations
{
    public partial class Documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "Production",
                table: "ProductImages");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                schema: "Production",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                schema: "Production",
                table: "ProductImages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "Management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PhysicalAddress = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DocumentId",
                schema: "Production",
                table: "Products",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_DocumentId",
                schema: "Production",
                table: "ProductImages",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Documents_DocumentId",
                schema: "Production",
                table: "ProductImages",
                column: "DocumentId",
                principalSchema: "Management",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Documents_DocumentId",
                schema: "Production",
                table: "Products",
                column: "DocumentId",
                principalSchema: "Management",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Documents_DocumentId",
                schema: "Production",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Documents_DocumentId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "Management");

            migrationBuilder.DropIndex(
                name: "IX_Products_DocumentId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_DocumentId",
                schema: "Production",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                schema: "Production",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                schema: "Production",
                table: "ProductImages");

            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                schema: "Production",
                table: "Products",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "Production",
                table: "ProductImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
