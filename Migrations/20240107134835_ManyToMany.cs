using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibWEB.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__efmigrationshistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductVersion = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MigrationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    country_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name_genre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "print_publishing",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    age_restriction = table.Column<int>(type: "int", nullable: false),
                    year_of_publishing = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numbers = table.Column<int>(type: "int", nullable: false),
                    image_id = table.Column<string>(type: "varchar(225)", maxLength: 225, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_print_publishing", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "reader",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    surname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    patronymic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    email_address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reader", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<int>(type: "int", nullable: false),
                    surname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    patronymic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.id);
                    table.ForeignKey(
                        name: "author_ibfk_1",
                        column: x => x.country,
                        principalTable: "country",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "genre_print_publishing",
                columns: table => new
                {
                    genre = table.Column<int>(type: "int", nullable: false),
                    print_publishing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre_print_publishing", x => new { x.genre, x.print_publishing });
                    table.ForeignKey(
                        name: "genre_print_publishing_ibfk_1",
                        column: x => x.genre,
                        principalTable: "genre",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "genre_print_publishing_ibfk_2",
                        column: x => x.print_publishing,
                        principalTable: "print_publishing",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "giving",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reader = table.Column<int>(type: "int", nullable: false),
                    giving_date = table.Column<DateOnly>(type: "date", nullable: false),
                    receiving_date = table.Column<DateOnly>(type: "date", nullable: true),
                    receiving_deadline_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giving", x => x.id);
                    table.ForeignKey(
                        name: "giving_ibfk_1",
                        column: x => x.reader,
                        principalTable: "reader",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "preorder",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reader = table.Column<int>(type: "int", nullable: false),
                    giving_deadline_date = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "enum('created','done','archived')", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preorder", x => x.id);
                    table.ForeignKey(
                        name: "preorder_ibfk_1",
                        column: x => x.reader,
                        principalTable: "reader",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "author_print_publishing",
                columns: table => new
                {
                    author = table.Column<int>(type: "int", nullable: false),
                    print_publishing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_print_publishing", x => new { x.author, x.print_publishing });
                    table.ForeignKey(
                        name: "author_print_publishing_ibfk_1",
                        column: x => x.author,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "author_print_publishing_ibfk_2",
                        column: x => x.print_publishing,
                        principalTable: "print_publishing",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "giving_print_publishing",
                columns: table => new
                {
                    giving = table.Column<int>(type: "int", nullable: false),
                    print_publishing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giving_print_publishing", x => new { x.giving, x.print_publishing });
                    table.ForeignKey(
                        name: "giving_print_publishing_ibfk_1",
                        column: x => x.giving,
                        principalTable: "giving",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "giving_print_publishing_ibfk_2",
                        column: x => x.print_publishing,
                        principalTable: "print_publishing",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "preorder_print_publishing",
                columns: table => new
                {
                    preorder = table.Column<int>(type: "int", nullable: false),
                    print_publishing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preorder_print_publishing", x => new { x.preorder, x.print_publishing });
                    table.ForeignKey(
                        name: "preorder_print_publishing_ibfk_1",
                        column: x => x.preorder,
                        principalTable: "preorder",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "preorder_print_publishing_ibfk_2",
                        column: x => x.print_publishing,
                        principalTable: "print_publishing",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "country",
                table: "author",
                column: "country");

            migrationBuilder.CreateIndex(
                name: "author",
                table: "author_print_publishing",
                column: "author");

            migrationBuilder.CreateIndex(
                name: "print_publishing",
                table: "author_print_publishing",
                column: "print_publishing");

            migrationBuilder.CreateIndex(
                name: "genre",
                table: "genre_print_publishing",
                column: "genre");

            migrationBuilder.CreateIndex(
                name: "print_publishing1",
                table: "genre_print_publishing",
                column: "print_publishing");

            migrationBuilder.CreateIndex(
                name: "reader",
                table: "giving",
                column: "reader");

            migrationBuilder.CreateIndex(
                name: "giving",
                table: "giving_print_publishing",
                column: "giving");

            migrationBuilder.CreateIndex(
                name: "print_publishing2",
                table: "giving_print_publishing",
                column: "print_publishing");

            migrationBuilder.CreateIndex(
                name: "reader1",
                table: "preorder",
                column: "reader");

            migrationBuilder.CreateIndex(
                name: "preorder",
                table: "preorder_print_publishing",
                column: "preorder");

            migrationBuilder.CreateIndex(
                name: "print_publishing3",
                table: "preorder_print_publishing",
                column: "print_publishing");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__efmigrationshistory");

            migrationBuilder.DropTable(
                name: "author_print_publishing");

            migrationBuilder.DropTable(
                name: "genre_print_publishing");

            migrationBuilder.DropTable(
                name: "giving_print_publishing");

            migrationBuilder.DropTable(
                name: "preorder_print_publishing");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "giving");

            migrationBuilder.DropTable(
                name: "preorder");

            migrationBuilder.DropTable(
                name: "print_publishing");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "reader");
        }
    }
}
