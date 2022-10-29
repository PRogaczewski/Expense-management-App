using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseProj.Migrations
{
    public partial class AddMonthlyGoals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserExpensesGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserExpensesListId = table.Column<int>(type: "int", nullable: false),
                    MonthChosenForGoal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExpensesGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExpensesGoals_ExpensesLists_UserExpensesListId",
                        column: x => x.UserExpensesListId,
                        principalTable: "ExpensesLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserExpenseGoalId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGoals_UserExpensesGoals_UserExpenseGoalId",
                        column: x => x.UserExpenseGoalId,
                        principalTable: "UserExpensesGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExpensesGoals_UserExpensesListId",
                table: "UserExpensesGoals",
                column: "UserExpensesListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGoals_UserExpenseGoalId",
                table: "UserGoals",
                column: "UserExpenseGoalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGoals");

            migrationBuilder.DropTable(
                name: "UserExpensesGoals");
        }
    }
}
