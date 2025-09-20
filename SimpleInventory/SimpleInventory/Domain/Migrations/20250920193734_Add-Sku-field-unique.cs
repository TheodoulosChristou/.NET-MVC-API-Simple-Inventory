﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddSkufieldunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_Sku",
                table: "Product",
                column: "Sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Sku",
                table: "Product");
        }
    }
}
