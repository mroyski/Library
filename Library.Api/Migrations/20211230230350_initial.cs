using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    LibIssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.LibIssueId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccOpenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxBooksAllowed = table.Column<int>(type: "int", nullable: true),
                    PenaltyAmount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Reserves",
                columns: table => new
                {
                    ReserveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    ReserveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReserveStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves", x => x.ReserveId);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "BookName", "Category", "Cost" },
                values: new object[] { 1, "Mike Royski", "Hello World!", "Technology", 10.0 });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "LibIssueId", "BookId", "IssueDate", "MemberId", "ReturnDate" },
                values: new object[] { 1, 1, new DateTime(2021, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "AccOpenDate", "MaxBooksAllowed", "MemberName", "PenaltyAmount" },
                values: new object[] { 1, new DateTime(2021, 12, 30, 18, 3, 49, 444, DateTimeKind.Local).AddTicks(1993), 5, "Cam Schaefer", 0 });

            migrationBuilder.InsertData(
                table: "Reserves",
                columns: new[] { "ReserveId", "BookId", "MemberId", "ReserveDate", "ReserveStatus" },
                values: new object[] { 1, 1, 1, new DateTime(2021, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "pending" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Reserves");
        }
    }
}
