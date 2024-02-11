using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class SeedDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1d521d0d-a216-4b2b-9896-9192498662bc", 0, "e800b2ba-9fc8-40bb-9083-40264253c0ea", null, false, false, null, null, "ROSEN111@ABV.BG", "AQAAAAEAACcQAAAAEASy6zo1qUwCPRmevbZgifQobbWaHFwUViT4bk88LZ7GAVw0kWu8MZUoxo/ODo8g4g==", null, false, "95a18ae6-6977-4139-a76b-28736ca9de5e", false, "rosen111@abv.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 26, 22, 39, 53, 147, DateTimeKind.Local).AddTicks(726), "Implement better styling for all public pages", "1d521d0d-a216-4b2b-9896-9192498662bc", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 11, 22, 39, 53, 147, DateTimeKind.Local).AddTicks(781), "Create Android client app for the TaskBoard RESTful API", "1d521d0d-a216-4b2b-9896-9192498662bc", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 11, 22, 39, 53, 147, DateTimeKind.Local).AddTicks(788), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "1d521d0d-a216-4b2b-9896-9192498662bc", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 11, 22, 39, 53, 147, DateTimeKind.Local).AddTicks(792), "Implement [Create Task] page for adding new tasks", "1d521d0d-a216-4b2b-9896-9192498662bc", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d521d0d-a216-4b2b-9896-9192498662bc");
        }
    }
}
