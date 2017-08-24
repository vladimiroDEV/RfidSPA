using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class StoreManage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "StoreUsers",
                newName: "ApplicationUserID");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "StoreUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Store",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RfidDeviceID",
                table: "RfidDeviceTransaction",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RfidDeviceID1",
                table: "RfidDeviceTransaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RfidDeviceID",
                table: "RfidDeviceHistory",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RfidDeviceID1",
                table: "RfidDeviceHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreUsers_ApplicationUserID",
                table: "StoreUsers",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RfidDeviceTransaction_RfidDeviceID1",
                table: "RfidDeviceTransaction",
                column: "RfidDeviceID1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID1",
                table: "RfidDeviceTransaction",
                column: "RfidDeviceID1",
                principalTable: "RfidDevice",
                principalColumn: "RfidDeviceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreUsers_AspNetUsers_ApplicationUserID",
                table: "StoreUsers",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceHistory_RfidDevice_RfidDeviceID1",
                table: "RfidDeviceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceTransaction_RfidDevice_RfidDeviceID1",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreUsers_AspNetUsers_ApplicationUserID",
                table: "StoreUsers");

            migrationBuilder.DropIndex(
                name: "IX_StoreUsers_ApplicationUserID",
                table: "StoreUsers");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceTransaction_RfidDeviceID1",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceHistory_RfidDeviceID1",
                table: "RfidDeviceHistory");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "RfidDeviceID",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropColumn(
                name: "RfidDeviceID1",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropColumn(
                name: "RfidDeviceID",
                table: "RfidDeviceHistory");

            migrationBuilder.DropColumn(
                name: "RfidDeviceID1",
                table: "RfidDeviceHistory");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "StoreUsers",
                newName: "UserID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "StoreUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
