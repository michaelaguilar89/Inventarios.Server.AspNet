using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventarios.Server.AspNet.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "storage_historys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Storage_Batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Storage_ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Storage_Quantity_Actual = table.Column<double>(type: "float", nullable: false),
                    Storage_Quantity_Operation = table.Column<double>(type: "float", nullable: false),
                    Storage_Quantity_Final = table.Column<double>(type: "float", nullable: false),
                    Storage_userIdCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Storage_CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Storage_userIdModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Storage_ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Storage_Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Production_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storage_historys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    userIdCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Final_Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userIdModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Storage_HistoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productions_storage_historys_Storage_HistoryId",
                        column: x => x.Storage_HistoryId,
                        principalTable: "storage_historys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    userIdCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userIdModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Storage_HistoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stores_storage_historys_Storage_HistoryId",
                        column: x => x.Storage_HistoryId,
                        principalTable: "storage_historys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_productions_Storage_HistoryId",
                table: "productions",
                column: "Storage_HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_stores_Storage_HistoryId",
                table: "stores",
                column: "Storage_HistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productions");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "storage_historys");
        }
    }
}
