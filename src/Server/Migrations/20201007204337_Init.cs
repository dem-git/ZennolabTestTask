using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaptchaApp.Server.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ContainsCyrillic = table.Column<bool>(nullable: false),
                    ContainsLatin = table.Column<bool>(nullable: false),
                    ContainsDigits = table.Column<bool>(nullable: false),
                    ContainsSpecialSymbols = table.Column<bool>(nullable: false),
                    CaseSentence = table.Column<bool>(nullable: false),
                    AnswerPlacement = table.Column<int>(nullable: false),
                    ImagesZipFilename = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSets");
        }
    }
}
