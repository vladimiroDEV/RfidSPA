using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RfidSPA.Migrations
{
    public partial class Addstoretoanagrafica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreID",
                table: "Anagrafica",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Anagrafica_StoreID",
                table: "Anagrafica",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Anagrafica_Store_StoreID",
                table: "Anagrafica",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "StoreID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anagrafica_Store_StoreID",
                table: "Anagrafica");

            migrationBuilder.DropIndex(
                name: "IX_Anagrafica_StoreID",
                table: "Anagrafica");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Anagrafica");
        }
    }
}
