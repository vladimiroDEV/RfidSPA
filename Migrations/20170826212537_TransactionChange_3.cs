using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class TransactionChange_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID",
                table: "RfidDeviceTransaction");

            migrationBuilder.AlterColumn<long>(
                name: "RfidDeviceID",
                table: "RfidDeviceTransaction",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID",
                table: "RfidDeviceTransaction",
                column: "RfidDeviceID",
                principalTable: "RfidDevice",
                principalColumn: "RfidDeviceID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID",
                table: "RfidDeviceTransaction");

            migrationBuilder.AlterColumn<long>(
                name: "RfidDeviceID",
                table: "RfidDeviceTransaction",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID",
                table: "RfidDeviceTransaction",
                column: "RfidDeviceID",
                principalTable: "RfidDevice",
                principalColumn: "RfidDeviceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
