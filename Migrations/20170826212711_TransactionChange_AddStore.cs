using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class TransactionChange_AddStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreID",
                table: "RfidDeviceTransaction",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RfidDeviceTransaction_StoreID",
                table: "RfidDeviceTransaction",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceTransaction_Store_StoreID",
                table: "RfidDeviceTransaction",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "StoreID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceTransaction_Store_StoreID",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceTransaction_StoreID",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "RfidDeviceTransaction");
        }
    }
}
