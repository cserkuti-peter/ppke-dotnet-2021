using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBookApi.Migrations
{
    public partial class recipesnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipesNumber",
                table: "RecipeBook",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RecipeBook",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecipesNumber",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipesNumber",
                table: "RecipeBook");
        }
    }
}
