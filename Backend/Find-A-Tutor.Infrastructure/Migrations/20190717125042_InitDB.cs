using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Find_A_Tutor.Infrastructure.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchoolSubjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudnetId = table.Column<Guid>(nullable: false),
                    TutorId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    TakenAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    RelevantTo = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SchoolSubjectId = table.Column<Guid>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateLessons_SchoolSubjects_SchoolSubjectId",
                        column: x => x.SchoolSubjectId,
                        principalTable: "SchoolSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateLessons_SchoolSubjectId",
                table: "PrivateLessons",
                column: "SchoolSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivateLessons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SchoolSubjects");
        }
    }
}
