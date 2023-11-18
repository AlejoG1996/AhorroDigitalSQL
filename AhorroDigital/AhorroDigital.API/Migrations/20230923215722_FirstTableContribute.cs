using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhorroDigital.API.Migrations
{
    /// <inheritdoc />
    public partial class FirstTableContribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SavingId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Marks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MarksAdmin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ValueAvail = table.Column<int>(type: "int", nullable: false),
                    ValueSlop = table.Column<int>(type: "int", nullable: false),
                    ValueRetreat = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAdminId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributes_AspNetUsers_UserAdminId",
                        column: x => x.UserAdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contributes_Savings_SavingId",
                        column: x => x.SavingId,
                        principalTable: "Savings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contributes_SavingId",
                table: "Contributes",
                column: "SavingId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributes_UserAdminId",
                table: "Contributes",
                column: "UserAdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contributes");
        }
    }
}
