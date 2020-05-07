﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment_WebShop_2.Migrations
{
    public partial class ORDERADDED : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "BasketElem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketElem_OrderID",
                table: "BasketElem",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketElem_Orders_OrderID",
                table: "BasketElem",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketElem_Orders_OrderID",
                table: "BasketElem");

            migrationBuilder.DropIndex(
                name: "IX_BasketElem_OrderID",
                table: "BasketElem");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "BasketElem");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderID",
                table: "Products",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderID",
                table: "Products",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
