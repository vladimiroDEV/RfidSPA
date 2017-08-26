using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class ChengeHistorymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceHistory_RfidDevice_RfidDeviceID1",
                table: "RfidDeviceHistory");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceHistory_RfidDeviceID1",
                table: "RfidDeviceHistory");

            migrationBuilder.DropColumn(
                name: "RfidDeviceID1",
                table: "RfidDeviceHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RfidDeviceID1",
                table: "RfidDeviceHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RfidDeviceHistory_RfidDeviceID1",
                table: "RfidDeviceHistory",
                column: "RfidDeviceID1");

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceHistory_RfidDevice_RfidDeviceID1",
                table: "RfidDeviceHistory",
                column: "RfidDeviceID1",
                principalTable: "RfidDevice",
                principalColumn: "RfidDeviceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
