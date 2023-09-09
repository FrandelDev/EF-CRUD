using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba2.Migrations
{
    /// <inheritdoc />
    public partial class PersonContextCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Raza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Dogs_Persons_AmoId",
                        column: x => x.AmoId,
                        principalTable: "Persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "id", "Apellido", "Nombre" },
                values: new object[] { new Guid("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), "Corporan", "Frandel" });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "id", "Age", "AmoId", "Nombre", "Raza" },
                values: new object[] { new Guid("fe2de405-c38e-4c90-ac52-da0540dfbd11"), 5, new Guid("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), "Chubi", null });

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_AmoId",
                table: "Dogs",
                column: "AmoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogs");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
