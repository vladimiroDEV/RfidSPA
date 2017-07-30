using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RfidSPA.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anagrafica",
                columns: table => new
                {
                    AnagraficaID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cognome = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anagrafica", x => x.AnagraficaID);
                });

            migrationBuilder.CreateTable(
                name: "RfidDevice",
                columns: table => new
                {
                    RfidDeviceID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AnagraficaID = table.Column<long>(nullable: true),
                    ApplicationUserID = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Credit = table.Column<double>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    RfidDeviceCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfidDevice", x => x.RfidDeviceID);
                });

            migrationBuilder.CreateTable(
                name: "RfidDeviceHistory",
                columns: table => new
                {
                    RfidDeviceHistoryID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AnagraficaID = table.Column<long>(nullable: true),
                    ApplicationUserID = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: true),
                    RfidDeviceCode = table.Column<string>(nullable: true),
                    RfidDeviceOperation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfidDeviceHistory", x => x.RfidDeviceHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "RfidDeviceTransaction",
                columns: table => new
                {
                    RfidDeviceTransactionID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnagraficaID = table.Column<long>(nullable: true),
                    ApplicationUserID = table.Column<string>(nullable: true),
                    Descrizione = table.Column<string>(nullable: true),
                    Importo = table.Column<double>(nullable: true),
                    PaydOff = table.Column<bool>(nullable: false),
                    PaydOffDate = table.Column<DateTime>(nullable: true),
                    RfidDeviceCode = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    TransactionOperation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfidDeviceTransaction", x => x.RfidDeviceTransactionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anagrafica");

            migrationBuilder.DropTable(
                name: "RfidDevice");

            migrationBuilder.DropTable(
                name: "RfidDeviceHistory");

            migrationBuilder.DropTable(
                name: "RfidDeviceTransaction");
        }
    }
}
