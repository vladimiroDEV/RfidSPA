using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class addStoreToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreID",
                table: "RfidDevice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RfidDevice_StoreID",
                table: "RfidDevice",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDevice_Store_StoreID",
                table: "RfidDevice",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "StoreID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDevice_Store_StoreID",
                table: "RfidDevice");

            migrationBuilder.DropIndex(
                name: "IX_RfidDevice_StoreID",
                table: "RfidDevice");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "RfidDevice");
        }
    }
}
