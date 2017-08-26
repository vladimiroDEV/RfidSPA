using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RfidSPA.Migrations
{
    public partial class ChangeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "RfidDeviceHistory");

            migrationBuilder.DropColumn(
                name: "AnagraficaID",
                table: "RfidDeviceHistory");

            migrationBuilder.DropColumn(
                name: "RfidDeviceCode",
                table: "RfidDeviceHistory");

            migrationBuilder.RenameColumn(
                name: "RfidDeviceOperation",
                table: "RfidDeviceHistory",
                newName: "TypeOperation");

            migrationBuilder.AddColumn<DateTime>(
                name: "OperationDate",
                table: "RfidDeviceHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedDate",
                table: "RfidDevice",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "Anagrafica",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_RfidDevice_AnagraficaID",
                table: "RfidDevice",
                column: "AnagraficaID");

            migrationBuilder.CreateIndex(
                name: "IX_Anagrafica_ApplicationUserID",
                table: "Anagrafica",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Anagrafica_AspNetUsers_ApplicationUserID",
                table: "Anagrafica",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDevice_Anagrafica_AnagraficaID",
                table: "RfidDevice",
                column: "AnagraficaID",
                principalTable: "Anagrafica",
                principalColumn: "AnagraficaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceHistory_TypeDeviceHistoryOperations_TypeOperation",
                table: "RfidDeviceHistory",
                column: "TypeOperation",
                principalTable: "TypeDeviceHistoryOperations",
                principalColumn: "TypeRfidDeviceOperationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anagrafica_AspNetUsers_ApplicationUserID",
                table: "Anagrafica");

            migrationBuilder.DropForeignKey(
                name: "FK_RfidDevice_Anagrafica_AnagraficaID",
                table: "RfidDevice");

            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceHistory_TypeDeviceHistoryOperations_TypeOperation",
                table: "RfidDeviceHistory");

            migrationBuilder.DropTable(
                name: "TypeDeviceHistoryOperations");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceHistory_TypeOperation",
                table: "RfidDeviceHistory");

            migrationBuilder.DropIndex(
                name: "IX_RfidDevice_AnagraficaID",
                table: "RfidDevice");

            migrationBuilder.DropIndex(
                name: "IX_Anagrafica_ApplicationUserID",
                table: "Anagrafica");

            migrationBuilder.DropColumn(
                name: "OperationDate",
                table: "RfidDeviceHistory");

            migrationBuilder.DropColumn(
                name: "JoinedDate",
                table: "RfidDevice");

            migrationBuilder.RenameColumn(
                name: "TypeOperation",
                table: "RfidDeviceHistory",
                newName: "RfidDeviceOperation");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "RfidDeviceHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "AnagraficaID",
                table: "RfidDeviceHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RfidDeviceCode",
                table: "RfidDeviceHistory",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "Anagrafica",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
