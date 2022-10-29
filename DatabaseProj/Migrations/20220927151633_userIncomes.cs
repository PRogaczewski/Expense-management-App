using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseProj.Migrations
{
    public partial class userIncomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserExpensesListId = table.Column<int>(type: "int", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserIncomes_ExpensesLists_UserExpensesListId",
                        column: x => x.UserExpensesListId,
                        principalTable: "ExpensesLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserIncomes_UserExpensesListId",
                table: "UserIncomes",
                column: "UserExpensesListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserIncomes");
        }
    }
}
