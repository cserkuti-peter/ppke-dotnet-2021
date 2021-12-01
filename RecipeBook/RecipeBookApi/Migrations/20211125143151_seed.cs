using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBookApi.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookTimeMinutes", "Ingredients", "Method", "Name", "RatingsAvg", "Servers" },
                values: new object[] { 1, 120, "Eggs, ...", "...", "Apple pie", 0.0, 10 });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookTimeMinutes", "Ingredients", "Method", "Name", "RatingsAvg", "Servers" },
                values: new object[] { 2, 20, "Rice, ...", "...", "Sushi", 0.0, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
