using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhorroDigital.API.Migrations
{
    /// <inheritdoc />
    public partial class AllBd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Retreats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavingId = table.Column<int>(type: "int", nullable: false),
                    DateM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Marks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MarksAdmin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAdmin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retreats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retreats_Savings_SavingId",
                        column: x => x.SavingId,
                        principalTable: "Savings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Retreats_SavingId",
                table: "Retreats",
                column: "SavingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Retreats");
        }
    }
}
