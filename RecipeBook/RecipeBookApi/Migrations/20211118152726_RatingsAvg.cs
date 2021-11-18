using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBookApi.Migrations
{
    public partial class RatingsAvg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RatingsAvg",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingsAvg",
                table: "Recipes");
        }
    }
}
