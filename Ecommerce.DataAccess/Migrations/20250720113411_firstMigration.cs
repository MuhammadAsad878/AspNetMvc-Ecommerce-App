using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Action" },
                    { 2, 2, "Adventure" },
                    { 3, 3, "Comedy" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Billy Spark", 1, "Praesent vitae sodales libero. Curabitur at pulvinar elit.", "SWD9999001", "", 90.0, 80.0, 85.0, "Fortune of Time" },
                    { 2, "Nancy Drew", 1, "Etiam auctor, magna vel malesuada tempor, augue velit aliquet sapien.", "SWD9999002", "", 120.0, 100.0, 110.0, "The Silent Observer" },
                    { 3, "James Walker", 1, "Suspendisse potenti. Nullam ac nisi non sapien pulvinar faucibus.", "SWD9999003", "", 75.0, 65.0, 70.0, "Mystic River Tales" },
                    { 4, "Sara Connor", 1, "Integer vel arcu nec augue dapibus fermentum a a ligula.", "SWD9999004", "", 150.0, 130.0, 140.0, "Digital Dreams" },
                    { 5, "Tony Stark", 1, "Morbi accumsan metus at ipsum euismod, ac porta velit volutpat.", "SWD9999005", "", 200.0, 180.0, 190.0, "Echoes of Eternity" },
                    { 6, "Bruce Wayne", 1, "Ut vehicula risus in ex blandit, ut facilisis ex luctus.", "SWD9999006", "", 110.0, 95.0, 100.0, "Code of Shadows" },
                    { 7, "Lara Craft", 1, "Phasellus at dignissim augue. In nec tellus magna.", "SWD9999007", "", 85.0, 75.0, 80.0, "Journey to Nowhere" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
