using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class TransactionChange_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID1",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceTransaction_RfidDeviceID1",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropColumn(
                name: "RfidDeviceID1",
                table: "RfidDeviceTransaction");

            migrationBuilder.AlterColumn<long>(
                name: "RfidDeviceID",
                table: "RfidDeviceTransaction",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RfidDeviceTransaction_RfidDeviceID",
                table: "RfidDeviceTransaction",
                column: "RfidDeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID",
                table: "RfidDeviceTransaction",
                column: "RfidDeviceID",
                principalTable: "RfidDevice",
                principalColumn: "RfidDeviceID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceTransaction_RfidDeviceID",
                table: "RfidDeviceTransaction");

            migrationBuilder.AlterColumn<string>(
                name: "RfidDeviceID",
                table: "RfidDeviceTransaction",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RfidDeviceID1",
                table: "RfidDeviceTransaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RfidDeviceTransaction_RfidDeviceID1",
                table: "RfidDeviceTransaction",
                column: "RfidDeviceID1");

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID1",
                table: "RfidDeviceTransaction",
                column: "RfidDeviceID1",
                principalTable: "RfidDevice",
                principalColumn: "RfidDeviceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
