using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RfidSPA.Migrations
{
    public partial class StoreUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersStore");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Store",
                newName: "CreatorUser");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Store",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdministratorID",
                table: "Store",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoreUsers",
                columns: table => new
                {
                    StoreUsersID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StoreID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUsers", x => x.StoreUsersID);
                    table.ForeignKey(
                        name: "FK_StoreUsers_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreUsers_StoreID",
                table: "StoreUsers",
                column: "StoreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "AdministratorID",
                table: "Store");

            migrationBuilder.RenameColumn(
                name: "CreatorUser",
                table: "Store",
                newName: "Adress");

            migrationBuilder.CreateTable(
                name: "UsersStore",
                columns: table => new
                {
                    UsersStoreID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserID = table.Column<string>(nullable: true),
                    ApplicationUserRole = table.Column<string>(nullable: true),
                    StoreID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersStore", x => x.UsersStoreID);
                });
        }
    }
}
