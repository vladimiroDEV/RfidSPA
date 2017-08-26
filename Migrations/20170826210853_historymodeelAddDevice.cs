using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class historymodeelAddDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RfidDeviceID",
                table: "RfidDeviceHistory",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RfidDeviceHistory_RfidDeviceID",
                table: "RfidDeviceHistory",
                column: "RfidDeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceHistory_RfidDevice_RfidDeviceID",
                table: "RfidDeviceHistory",
                column: "RfidDeviceID",
                principalTable: "RfidDevice",
                principalColumn: "RfidDeviceID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceHistory_RfidDevice_RfidDeviceID",
                table: "RfidDeviceHistory");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceHistory_RfidDeviceID",
                table: "RfidDeviceHistory");

            migrationBuilder.DropColumn(
                name: "RfidDeviceID",
                table: "RfidDeviceHistory");
        }
    }
}
