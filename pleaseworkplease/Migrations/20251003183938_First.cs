using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pleaseworkplease.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    keyword = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    rating = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    Genre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Novels",
                columns: table => new
                {
                    ISBN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Author = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novels", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_Novels_Genre",
                        column: x => x.Genre,
                        principalTable: "Genre",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Novels_Genre",
                table: "Novels",
                column: "Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Novels");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
