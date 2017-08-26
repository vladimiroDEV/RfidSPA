using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class ChangeHistorymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeDeviceHistoryOperation",
                table: "RfidDeviceHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeDeviceHistoryOperation",
                table: "RfidDeviceHistory",
                nullable: false,
                defaultValue: 0);
        }
    }
}
