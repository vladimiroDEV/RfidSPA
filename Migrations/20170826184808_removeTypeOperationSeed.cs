using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RfidSPA.Migrations
{
    public partial class removeTypeOperationSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceHistory_TypeDeviceHistoryOperations_TypeOperation",
                table: "RfidDeviceHistory");

            migrationBuilder.DropTable(
                name: "TypeDeviceHistoryOperations");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceHistory_TypeOperation",
                table: "RfidDeviceHistory");

            migrationBuilder.AddColumn<int>(
                name: "TypeDeviceHistoryOperation",
                table: "RfidDeviceHistory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeDeviceHistoryOperation",
                table: "RfidDeviceHistory");

            migrationBuilder.CreateTable(
                name: "TypeDeviceHistoryOperations",
                columns: table => new
                {
                    TypeRfidDeviceOperationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeDeviceHistoryOperations", x => x.TypeRfidDeviceOperationID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RfidDeviceHistory_TypeOperation",
                table: "RfidDeviceHistory",
                column: "TypeOperation");

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceHistory_TypeDeviceHistoryOperations_TypeOperation",
                table: "RfidDeviceHistory",
                column: "TypeOperation",
                principalTable: "TypeDeviceHistoryOperations",
                principalColumn: "TypeRfidDeviceOperationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
