using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhorroDigital.API.Migrations
{
    /// <inheritdoc />
    public partial class FirstTablePayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MarksAdmin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ValueP = table.Column<int>(type: "int", nullable: false),
                    ValueD = table.Column<int>(type: "int", nullable: false),
                    ValueDues = table.Column<int>(type: "int", nullable: false),
                    Interest = table.Column<double>(type: "float", nullable: false),
                    Dues = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueLoanPagado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_LoanTypes_LoanTypeId",
                        column: x => x.LoanTypeId,
                        principalTable: "LoanTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatePR = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueCapital = table.Column<int>(type: "int", nullable: false),
                    ValueInt = table.Column<int>(type: "int", nullable: false),
                    ValueIntG = table.Column<int>(type: "int", nullable: false),
                    DayArrearsM = table.Column<double>(type: "float", nullable: false),
                    ValueArrearsM = table.Column<int>(type: "int", nullable: false),
                    ValueTP = table.Column<int>(type: "int", nullable: false),
                    PendientePago = table.Column<int>(type: "int", nullable: false),
                    TotalCapital = table.Column<int>(type: "int", nullable: false),
                    TotalInterest = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pago = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPlan_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<int>(type: "int", nullable: false),
                    IdSec = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Marks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MarksAdmin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueCapital = table.Column<int>(type: "int", nullable: false),
                    ValueInt = table.Column<int>(type: "int", nullable: false),
                    DayArrears = table.Column<double>(type: "float", nullable: false),
                    ValueArrears = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ValueP = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAdminId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPaymentPlan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_UserAdminId",
                        column: x => x.UserAdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanTypeId",
                table: "Loans",
                column: "LoanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlan_LoanId",
                table: "PaymentPlan",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LoanId",
                table: "Payments",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserAdminId",
                table: "Payments",
                column: "UserAdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentPlan");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
