using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[] { 1, "user1@email.com", "password 1", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[] { 2, "user2@email.com", "password 2", "Member" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[] { 3, "user3@email.com", "password 3", "Member" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Color", "CreationTime", "Description", "Name", "Status", "UserId" },
                values: new object[] { 1, "Color 1", new DateTime(2024, 1, 24, 14, 0, 10, 317, DateTimeKind.Local).AddTicks(9692), "Description 1", "Card 1", "ToDo", 1 });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Color", "CreationTime", "Description", "Name", "Status", "UserId" },
                values: new object[] { 2, "Color 2", new DateTime(2024, 1, 24, 14, 0, 10, 317, DateTimeKind.Local).AddTicks(9740), "Description 2", "Card 2", "ToDo", 2 });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Color", "CreationTime", "Description", "Name", "Status", "UserId" },
                values: new object[] { 3, "Color 3", new DateTime(2024, 1, 24, 14, 0, 10, 317, DateTimeKind.Local).AddTicks(9742), "Description 3", "Card 3", "ToDo", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
