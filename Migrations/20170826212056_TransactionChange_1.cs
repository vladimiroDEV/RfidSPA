using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class TransactionChange_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RfidDeviceTransaction_AnagraficaID",
                table: "RfidDeviceTransaction",
                column: "AnagraficaID");

            migrationBuilder.AddForeignKey(
                name: "FK_RfidDeviceTransaction_Anagrafica_AnagraficaID",
                table: "RfidDeviceTransaction",
                column: "AnagraficaID",
                principalTable: "Anagrafica",
                principalColumn: "AnagraficaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RfidDeviceTransaction_Anagrafica_AnagraficaID",
                table: "RfidDeviceTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RfidDeviceTransaction_AnagraficaID",
                table: "RfidDeviceTransaction");
        }
    }
}
