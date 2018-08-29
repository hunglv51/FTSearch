using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestFileStream.Migrations
{
    public partial class map : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestDocument",
                columns: table => new
                {
                    Guid = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    file_type = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDocument", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "WordDocument",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    title = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    content = table.Column<byte[]>(nullable: false),
                    file_type = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordDocument", x => x.Guid);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__WordDocu__A2B5777DA3B7279F",
                table: "WordDocument",
                column: "Guid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestDocument");

            migrationBuilder.DropTable(
                name: "WordDocument");
        }
    }
}
