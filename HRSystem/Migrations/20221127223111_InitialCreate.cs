using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "division",
                columns: table => new
                {
                    division_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true, defaultValueSql: "CONVERT(Char(16), getdate() ,20)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_division", x => x.division_id);

                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    division_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_id);
                    table.ForeignKey(
                        name: "employee_FK",
                        column: x => x.division_id,
                        principalTable: "division",
                        principalColumn: "division_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "transfer_history",
                columns: table => new
                {
                    transfer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_id = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    division_id = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    date_from = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    date_to = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfer_history", x => x.transfer_id);
                    table.ForeignKey(
                        name: "transfer_history_FK",
                        column: x => x.division_id,
                        principalTable: "division",
                        principalColumn: "division_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "transfer_history_FK_1",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_division_parent_id",
                table: "division",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_division_id",
                table: "employee",
                column: "division_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfer_history_division_id",
                table: "transfer_history",
                column: "division_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfer_history_employee_id",
                table: "transfer_history",
                column: "employee_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transfer_history");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "division");
        }
    }
}
