using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class Seed : Migration
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
                values: new object[] { "f0ecad61-bbfa-447f-8533-2501f6ffad3b", 0, "db2fde33-bed6-4e25-9cf3-45e530c5e510", null, false, false, null, null, "ROSEN.ROYDEV@GMAIL.COM", "AQAAAAEAACcQAAAAEIdgHU8PKhvko2kEciAU9NOFw7dErQ1PZ38x838Ii5N3HrLAABHRmmEo6yHQCRTPhw==", null, false, "451588b6-6603-403e-9529-ba7d0c5d629b", false, "rosen.roydev@gmail.com" });

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
                    { 1, 1, new DateTime(2023, 7, 26, 22, 50, 24, 775, DateTimeKind.Local).AddTicks(2528), "Implement better styling for all public pages", "f0ecad61-bbfa-447f-8533-2501f6ffad3b", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 11, 22, 50, 24, 775, DateTimeKind.Local).AddTicks(2589), "Create Android client app for the TaskBoard RESTful API", "f0ecad61-bbfa-447f-8533-2501f6ffad3b", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 11, 22, 50, 24, 775, DateTimeKind.Local).AddTicks(2595), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "f0ecad61-bbfa-447f-8533-2501f6ffad3b", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 11, 22, 50, 24, 775, DateTimeKind.Local).AddTicks(2600), "Implement [Create Task] page for adding new tasks", "f0ecad61-bbfa-447f-8533-2501f6ffad3b", "Create Tasks" }
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
                keyValue: "f0ecad61-bbfa-447f-8533-2501f6ffad3b");
        }
    }
}
